using System.Formats.Tar;
using System.IO.Compression;
using System.Net.Mime;
using Docker.DotNet;
using Docker.DotNet.Models;
using FluentValidation;
using TimLearning.Application.ToDo.Dto;
using TimLearning.Application.ToDo.Mappers;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Application.ToDoServices;

public class ExerciseService
{
    private readonly IAppDbContext _db;

    public ExerciseService(IAppDbContext db)
    {
        _db = db;
    }

    public async Task<Guid> CreateAsync(ExerciseCreateDto dto)
    {
        if (
            dto.NewApp.Source.ContentType
            is not MediaTypeNames.Application.Zip
                and not "application/x-zip-compressed"
        )
        {
            throw new ValidationException("Invalid type archived app.");
        }

        var temp = Directory.CreateTempSubdirectory();
        await using (var file = dto.NewApp.Source.OpenReadStream())
        {
            ZipFile.ExtractToDirectory(file, temp.FullName, overwriteFiles: true);
        }

        await File.WriteAllTextAsync(Path.Join(temp.FullName, dto.PathToRewriteFile), dto.Code);

        var pathToNewApp = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

        await TarFile.CreateFromDirectoryAsync(
            temp.FullName,
            pathToNewApp,
            includeBaseDirectory: false
        );
        var newApp = File.Open(pathToNewApp, FileMode.Open);

        using var client = new DockerClientConfiguration().CreateClient();

        var networkName = Guid.NewGuid().ToString();
        await client.Networks.CreateNetworkAsync(
            new NetworksCreateParameters { Name = networkName, Driver = "bridge" }
        );

        if (dto.Images is not null)
        {
            foreach (var image in dto.Images)
            {
                await client.Images.CreateImageAsync(
                    new ImagesCreateParameters
                    {
                        FromImage = image.ImageNameWithoutTag,
                        Tag = image.Tag
                    },
                    new AuthConfig(),
                    new Progress<JSONMessage>(message =>
                    {
                        // TODO: надо что то делать на ошибку
                        if (string.IsNullOrEmpty(message.ErrorMessage) == false)
                        {
                            throw new InvalidOperationException("Ошибка при создании имеджа");
                        }
                    })
                );

                var containerParam = new CreateContainerParameters
                {
                    Image = $"{image.ImageNameWithoutTag}:{image.Tag}",
                    Name = Guid.NewGuid().ToString(),
                    Hostname = image.ContainerSettings.Hostname,
                    Env = image.ContainerSettings.Envs?.Select(e => e.ToDockerEnv()).ToList(),
                    NetworkingConfig = new NetworkingConfig
                    {
                        EndpointsConfig = new Dictionary<string, EndpointSettings>
                        {
                            { networkName, new EndpointSettings() }
                        }
                    },
                    // TODO: for test
                    HostConfig = new HostConfig { PublishAllPorts = true },
                };

                if (
                    image.ContainerSettings.HealthcheckTest is not null
                    && image.ContainerSettings.HealthcheckTest.Any()
                )
                {
                    containerParam.Healthcheck = new HealthConfig()
                    {
                        Test = image.ContainerSettings.HealthcheckTest,
                        Interval = TimeSpan.FromSeconds(5),
                        Timeout = TimeSpan.FromSeconds(3),
                        Retries = 5
                    };
                }

                var container = await client.Containers.CreateContainerAsync(containerParam);

                var result = await client.Containers.StartContainerAsync(
                    container.ID,
                    new ContainerStartParameters()
                );
                if (result == false)
                {
                    throw new InvalidOperationException(
                        "Ошибка при старте контейнера вспомогательного приложения"
                    );
                }

                if (
                    image.ContainerSettings.HealthcheckTest is not null
                    && image.ContainerSettings.HealthcheckTest.Any()
                )
                {
                    var retryCount = 0;
                    while (retryCount < 10)
                    {
                        var insp = await client.Containers.InspectContainerAsync(container.ID);
                        if (insp.State.Health is null)
                        {
                            throw new Exception("Нет хелс чека");
                        }
                        if (insp.State.Health?.Status is "none" or "healthy")
                        {
                            break;
                        }
                        if (insp.State.Health?.Status == "starting")
                        {
                            retryCount++;
                            await Task.Delay(TimeSpan.FromSeconds(2));
                            continue;
                        }
                        if (insp.State.Health?.Status == "unhealthy")
                        {
                            throw new InvalidOperationException("Не работает имедж");
                        }

                        throw new Exception("Not supported status.");
                    }
                }
            }
        }

        var appImage = $"app-{Guid.NewGuid()}";
        await client.Images.BuildImageFromDockerfileAsync(
            new ImageBuildParameters
            {
                Dockerfile = dto.NewApp.PathToDockerfile,
                Tags = new List<string> { appImage },
            },
            newApp,
            new AuthConfig[] { },
            new Dictionary<string, string>(),
            new Progress<JSONMessage>(message =>
            {
                // TODO: надо что то делать на ошибку
                if (string.IsNullOrEmpty(message.ErrorMessage) == false)
                {
                    throw new InvalidOperationException("Ошибка при создании имеджа приложения");
                }
            })
        );

        var appContainer = await client.Containers.CreateContainerAsync(
            new CreateContainerParameters
            {
                Name = Guid.NewGuid().ToString(),
                Image = appImage,
                Hostname = dto.NewApp.AppContainerSettings.Hostname,
                Env = dto.NewApp.AppContainerSettings.Envs?.Select(e => e.ToDockerEnv()).ToList(),
                NetworkingConfig = new NetworkingConfig
                {
                    EndpointsConfig = new Dictionary<string, EndpointSettings>
                    {
                        { networkName, new EndpointSettings() }
                    }
                }
            }
        );

        var appStartResult = await client.Containers.StartContainerAsync(
            appContainer.ID,
            new ContainerStartParameters()
        );

        if (appStartResult == false)
        {
            throw new InvalidOperationException("Ошибка при старте контейнера приложения");
        }

        return Guid.NewGuid();
    }
}
