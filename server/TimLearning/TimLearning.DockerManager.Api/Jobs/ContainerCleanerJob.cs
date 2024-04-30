using Quartz;
using TimLearning.DockerManager.Api.Services.Docker.Services;

namespace TimLearning.DockerManager.Api.Jobs;

public class ContainerCleanerJob : IJob
{
    private readonly IContainerService _dockerContainerService;
    private readonly ILogger<ContainerCleanerJob> _logger;

    public ContainerCleanerJob(
        IContainerService dockerContainerService,
        ILogger<ContainerCleanerJob> logger
    )
    {
        _dockerContainerService = dockerContainerService;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Job is started.");

        try
        {
            await _dockerContainerService.Prune(beforeMin: 5);

            _logger.LogInformation("Job was completed successfully.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Job was completed with an error.");
        }
    }
}
