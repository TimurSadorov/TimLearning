using System.Net;
using Docker.DotNet;
using Docker.DotNet.Models;
using TimLearning.DockerManager.Api.Services.Data;
using TimLearning.DockerManager.Api.Services.Docker.Client;
using TimLearning.DockerManager.Api.Services.Docker.Consts;
using TimLearning.DockerManager.Api.Services.Docker.Data;

namespace TimLearning.DockerManager.Api.Services.Docker.Services;

public class ImageService : IImageService
{
    private readonly IDockerClientFactory _dockerClientFactory;

    public ImageService(IDockerClientFactory dockerClientFactory)
    {
        _dockerClientFactory = dockerClientFactory;
    }

    public async Task<OperationResult> PullImage(
        string image,
        string tag,
        CancellationToken ct = default
    )
    {
        using var client = _dockerClientFactory.Create();

        var imageWithTag = Concat(image, tag);
        var progressProcessor = new ImageCreationProgressProcessor();
        try
        {
            await client.Images.CreateImageAsync(
                new ImagesCreateParameters { FromImage = imageWithTag },
                new AuthConfig(),
                progressProcessor,
                ct
            );
        }
        catch (DockerApiException e) when (e.StatusCode is HttpStatusCode.NotFound)
        {
            return OperationResult.Error($"Image not found: {imageWithTag}");
        }

        return progressProcessor.IsSuccessOperation
            ? OperationResult.Success()
            : OperationResult.Error(progressProcessor.Messages.ToString());
    }

    public async Task<OperationResult<BuildImageResult>> BuildImage(
        Stream archive,
        string pathToDockerfile,
        CancellationToken ct = default
    )
    {
        var client = _dockerClientFactory.Create();

        var appImage = $"app-{Guid.NewGuid()}";
        var progressProcessor = new ImageCreationProgressProcessor();
        try
        {
            await client.Images.BuildImageFromDockerfileAsync(
                new ImageBuildParameters
                {
                    Dockerfile = pathToDockerfile,
                    Tags = new List<string> { appImage },
                    ForceRemove = true
                },
                archive,
                new AuthConfig[] { },
                new Dictionary<string, string>(),
                progressProcessor,
                ct
            );
        }
        catch (DockerApiException e) when (e.StatusCode is HttpStatusCode.InternalServerError)
        {
            return OperationResult<BuildImageResult>.Error($"Docker host error: {e.ResponseBody}");
        }

        return progressProcessor.IsSuccessOperation
            ? OperationResult<BuildImageResult>.Success(
                new BuildImageResult(appImage, DockerImage.DefaultTag)
            )
            : OperationResult<BuildImageResult>.Error(progressProcessor.Messages.ToString());
    }

    public async Task Delete(string nameOrId, bool force, CancellationToken ct = default)
    {
        var client = _dockerClientFactory.Create();

        await client.Images.DeleteImageAsync(
            nameOrId,
            new ImageDeleteParameters { Force = force },
            ct
        );
    }

    public string Concat(string imageName, string tag) => $"{imageName}:{tag}";
}
