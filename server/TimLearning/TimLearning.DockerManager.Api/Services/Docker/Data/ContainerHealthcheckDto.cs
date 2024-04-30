namespace TimLearning.DockerManager.Api.Services.Docker.Data;

public record ContainerHealthcheckDto(
    List<string> HealthcheckTest,
    int RetriesCount,
    TimeSpan Interval,
    TimeSpan Timeout
);
