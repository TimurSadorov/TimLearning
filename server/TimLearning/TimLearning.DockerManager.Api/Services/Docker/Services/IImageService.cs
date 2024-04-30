using TimLearning.DockerManager.Api.Services.Data;
using TimLearning.DockerManager.Api.Services.Docker.Data;

namespace TimLearning.DockerManager.Api.Services.Docker.Services;

public interface IImageService
{
    Task<OperationResult> PullImage(string image, string tag, CancellationToken ct = default);

    Task<OperationResult<BuildImageResult>> BuildImage(
        Stream archive,
        string pathToDockerfile,
        CancellationToken ct = default
    );

    Task Delete(string nameOrId, bool force, CancellationToken ct = default);

    string Concat(string imageName, string tag);
}
