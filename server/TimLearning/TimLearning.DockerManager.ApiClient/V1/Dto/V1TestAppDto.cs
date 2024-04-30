using TimLearning.DockerManager.ApiClient.V1.Request;

namespace TimLearning.DockerManager.ApiClient.V1.Dto;

public record V1TestAppDto(
    FileInfo AppArchive,
    V1MainAppContainerRequest AppContainer,
    string RelativePathToDockerfile,
    IReadOnlyList<V1ServiceContainerImageRequest> ServiceApps
);
