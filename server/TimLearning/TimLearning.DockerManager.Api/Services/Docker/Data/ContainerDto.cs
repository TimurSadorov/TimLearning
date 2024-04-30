namespace TimLearning.DockerManager.Api.Services.Docker.Data;

public record ContainerDto(
    string FromImage,
    string ImageTag,
    string NetworkNameOrId,
    string? Hostname,
    ContainerHealthcheckDto? Healthcheck,
    List<ContainerEnvDto>? Envs
);
