using System.Text;
using Docker.DotNet.Models;
using TimLearning.DockerManager.Api.Services.Data;
using TimLearning.DockerManager.Api.Services.Docker.Client;
using TimLearning.DockerManager.Api.Services.Docker.Consts;
using TimLearning.DockerManager.Api.Services.Docker.Data;
using TimLearning.Shared.Extensions;

namespace TimLearning.DockerManager.Api.Services.Docker.Services;

public class ContainerService : IContainerService
{
    private readonly IDockerClientFactory _dockerClientFactory;
    private readonly IImageService _imageService;

    public ContainerService(IDockerClientFactory dockerClientFactory, IImageService imageService)
    {
        _dockerClientFactory = dockerClientFactory;
        _imageService = imageService;
    }

    public async Task<OperationResult<ContainerStartingResult>> CreateAndStart(
        ContainerDto containerData,
        CancellationToken ct = default
    )
    {
        using var client = _dockerClientFactory.Create();
        var imageWithTag = _imageService.Concat(containerData.FromImage, containerData.ImageTag);

        var healthcheckData = containerData.Healthcheck;
        var containerParameters = new CreateContainerParameters
        {
            Image = imageWithTag,
            Name = Guid.NewGuid().ToString(),
            Hostname = containerData.Hostname,
            Env = containerData.Envs?.Select(e => $"{e.Name}={e.Value}").ToList(),
            NetworkingConfig = new NetworkingConfig
            {
                EndpointsConfig = new Dictionary<string, EndpointSettings>
                {
                    { containerData.NetworkNameOrId, new EndpointSettings() }
                }
            },
            Healthcheck = healthcheckData is not null
                ? new HealthConfig
                {
                    Test = healthcheckData.HealthcheckTest,
                    Retries = healthcheckData.RetriesCount,
                    Interval = healthcheckData.Interval,
                    Timeout = healthcheckData.Timeout
                }
                : null,
            // TODO: for test
            // HostConfig = new HostConfig { PublishAllPorts = true },
        };
        var newContainer = await client.Containers.CreateContainerAsync(containerParameters, ct);

        var result = await client.Containers.StartContainerAsync(
            newContainer.ID,
            new ContainerStartParameters(),
            ct
        );
        if (result is false)
        {
            return OperationResult<ContainerStartingResult>.Error("Container launch error.");
        }

        return OperationResult<ContainerStartingResult>.Success(
            new ContainerStartingResult(newContainer.ID)
        );
    }

    public async Task Delete(string idOrNameContainer, bool force, CancellationToken ct = default)
    {
        using var client = _dockerClientFactory.Create();

        await client.Containers.RemoveContainerAsync(
            idOrNameContainer,
            new ContainerRemoveParameters { Force = force },
            ct
        );
    }

    public async Task<OperationResult> WaitHealthy(
        string containerId,
        TimeSpan retryTime,
        CancellationToken ct = default
    )
    {
        using var client = _dockerClientFactory.Create();

        while (true)
        {
            var containerInspectionInfo = await client.Containers.InspectContainerAsync(
                containerId,
                ct
            );

            var image = containerInspectionInfo.Config.Image;
            var containerHealth = containerInspectionInfo.State.Health;
            switch (containerHealth?.Status)
            {
                case null:
                case ContainerHealthStatus.None:
                    return OperationResult.Error(
                        $"A health check was specified for the container with the image: {image},"
                            + $" but there was no check after the container was launched."
                    );
                case ContainerHealthStatus.Healthy:
                    return OperationResult.Success();
                case ContainerHealthStatus.Starting:
                    await Task.Delay(retryTime, ct);
                    continue;
                case ContainerHealthStatus.Unhealthy:
                {
                    var errorMessage = new StringBuilder();
                    errorMessage.AppendLine($"Service unhealthy. Image: {image}.");
                    foreach (var healthcheckResult in containerHealth.Log)
                    {
                        errorMessage.AppendLine("Result of a health check attempt.");
                        errorMessage.AppendLine($"Exit code: {healthcheckResult.ExitCode}");
                        errorMessage.AppendLine($"Message: {healthcheckResult.Output}");
                    }

                    return OperationResult.Error(errorMessage.ToString());
                }
                default:
                    throw new InvalidOperationException(
                        $"Unknown container health status: {containerHealth.Status}."
                    );
            }
        }
    }

    public async Task<OperationResult> WaitForSuccessfulStop(
        string containerId,
        TimeSpan timout,
        CancellationToken ct = default
    )
    {
        using var client = _dockerClientFactory.Create();

        ContainerWaitResponse executingContainerResult;
        try
        {
            var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(ct);
            cancellationTokenSource.CancelAfter(timout);
            executingContainerResult = await client.Containers.WaitContainerAsync(
                containerId,
                cancellationTokenSource.Token
            );
        }
        catch (OperationCanceledException)
        {
            return OperationResult.Error("The testing of the application took too long.");
        }

        if (
            executingContainerResult.StatusCode != 0
            || executingContainerResult.Error?.Message?.HasText() is true
        )
        {
            var containerLogs = await client.Containers.GetContainerLogsAsync(
                containerId,
                false,
                new ContainerLogsParameters { ShowStdout = true },
                ct
            );
            var (output, _) = await containerLogs.ReadOutputToEndAsync(ct);

            return OperationResult.Error(
                $"Container execution error.{Environment.NewLine}" + output
            );
        }

        return OperationResult.Success();
    }

    public async Task Prune(int beforeMin, CancellationToken ct = default)
    {
        using var client = _dockerClientFactory.Create();

        await client.Containers.PruneContainersAsync(
            new ContainersPruneParameters
            {
                Filters = new Dictionary<string, IDictionary<string, bool>>
                {
                    {
                        "until",
                        new Dictionary<string, bool> { { $"{beforeMin}m", true } }
                    }
                }
            },
            ct
        );
    }
}
