using TimLearning.DockerManager.Api.Services.Data;
using TimLearning.DockerManager.Api.Services.Docker.Data;
using TimLearning.DockerManager.Api.Services.Docker.Services;
using TimLearning.DockerManager.Api.Services.Mappers;
using TimLearning.DockerManager.ApiClient.V1.Request;
using TimLearning.DockerManager.ApiClient.V1.Response;

namespace TimLearning.DockerManager.Api.Services;

public class AppTester : IAppTester
{
    private readonly ILogger<AppTester> _logger;
    private readonly INetworkService _dockerNetworkService;
    private readonly IContainerService _dockerContainerService;
    private readonly IImageService _dockerImageService;

    public AppTester(
        ILogger<AppTester> logger,
        INetworkService dockerNetworkService,
        IContainerService dockerContainerService,
        IImageService dockerImageService
    )
    {
        _logger = logger;
        _dockerNetworkService = dockerNetworkService;
        _dockerContainerService = dockerContainerService;
        _dockerImageService = dockerImageService;
    }

    public async Task<V1AppTestResponse> Test(
        V1AppTestRequest request,
        CancellationToken ct = default
    )
    {
        string? networkId = null;
        var serviceContainerIds = new List<string>();
        try
        {
            networkId = await _dockerNetworkService.Create(NetworkType.Bridge, ct);

            foreach (var serviceApp in request.ServiceApps)
            {
                var statingResult = await StartServiceApp(
                    serviceApp,
                    networkId,
                    serviceContainerIds,
                    ct
                );
                if (statingResult.IsSuccess is false)
                {
                    return new V1AppTestResponse(
                        V1AppTestingStatus.ErrorStartingServiceApp,
                        statingResult.ErrorMessage
                    );
                }
            }

            var executionResult = await ExecuteMainApp(request, networkId, ct);
            return executionResult.IsSuccess
                ? new V1AppTestResponse(V1AppTestingStatus.Ok, null)
                : new V1AppTestResponse(
                    V1AppTestingStatus.ErrorExecutingMainApp,
                    executionResult.ErrorMessage
                );
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error working with docker while testing the app.");
            throw;
        }
        finally
        {
            await DeleteNetworkAndServiceAppResources(networkId, serviceContainerIds);
        }
    }

    private async Task<OperationResult> ExecuteMainApp(
        V1AppTestRequest request,
        string networkId,
        CancellationToken ct
    )
    {
        string? mainAppImageName = null;
        string? mainAppContainerId = null;
        try
        {
            await using var appArchive = request.App.OpenReadStream();
            var buildResult = await _dockerImageService.BuildImage(
                appArchive,
                request.RelativePathToDockerfile,
                ct
            );
            if (buildResult.IsSuccess is false)
            {
                return OperationResult.Error(buildResult.ErrorMessage);
            }
            mainAppImageName = buildResult.Result!.ImageName;

            var mainAppContainerSettings = request.AppContainer;
            var mainAppStartingResult = await _dockerContainerService.CreateAndStart(
                new ContainerDto(
                    buildResult.Result!.ImageName,
                    buildResult.Result.ImageTag,
                    networkId,
                    mainAppContainerSettings.Hostname,
                    null,
                    mainAppContainerSettings.Envs?.Select(e => e.ToDto()).ToList()
                ),
                ct
            );
            if (mainAppStartingResult.IsSuccess is false)
            {
                return OperationResult.Error(mainAppStartingResult.ErrorMessage);
            }
            mainAppContainerId = mainAppStartingResult.Result!.ContainerId;

            var waitingResult = await _dockerContainerService.WaitForSuccessfulStop(
                mainAppStartingResult.Result!.ContainerId,
                TimeSpan.FromMinutes(3),
                ct
            );
            if (waitingResult.IsSuccess is false)
            {
                return OperationResult.Error(waitingResult.ErrorMessage);
            }

            return OperationResult.Success();
        }
        finally
        {
            await DeleteUnnecessaryMainAppResources(mainAppContainerId, mainAppImageName);
        }
    }

    private async Task<OperationResult> StartServiceApp(
        V1ServiceContainerImageRequest serviceApp,
        string networkId,
        List<string> serviceContainerIds,
        CancellationToken ct
    )
    {
        var pullResult = await _dockerImageService.PullImage(serviceApp.Name, serviceApp.Tag, ct);
        if (pullResult.IsSuccess is false)
        {
            return OperationResult.Error(pullResult.ErrorMessage);
        }

        var serviceContainer = serviceApp.Container;
        var hasHealthcheck =
            serviceContainer.HealthcheckTest is not null
            && serviceContainer.HealthcheckTest.Count != 0;
        var startingResult = await _dockerContainerService.CreateAndStart(
            new ContainerDto(
                serviceApp.Name,
                serviceApp.Tag,
                networkId,
                serviceContainer.Hostname,
                hasHealthcheck
                    ? new ContainerHealthcheckDto(
                        serviceContainer.HealthcheckTest!,
                        5,
                        TimeSpan.FromSeconds(5),
                        TimeSpan.FromSeconds(3)
                    )
                    : null,
                serviceContainer.Envs?.Select(e => e.ToDto()).ToList()
            ),
            ct
        );
        if (startingResult.IsSuccess is false)
        {
            return OperationResult.Error(startingResult.ErrorMessage);
        }
        serviceContainerIds.Add(startingResult.Result!.ContainerId);

        if (hasHealthcheck)
        {
            var waitResult = await _dockerContainerService.WaitHealthy(
                startingResult.Result.ContainerId,
                TimeSpan.FromSeconds(8),
                ct
            );
            if (waitResult.IsSuccess is false)
            {
                return OperationResult.Error(waitResult.ErrorMessage);
            }
        }

        return OperationResult.Success();
    }

    private async Task DeleteUnnecessaryMainAppResources(
        string? mainAppContainerId,
        string? mainAppImageName
    )
    {
        if (mainAppContainerId is not null)
        {
            await _dockerContainerService.Delete(mainAppContainerId, force: true);
        }

        if (mainAppImageName is not null)
        {
            await _dockerImageService.Delete(mainAppImageName, force: true);
        }
    }

    private async Task DeleteNetworkAndServiceAppResources(
        string? networkId,
        List<string> serviceContainerIds
    )
    {
        foreach (var serviceContainerId in serviceContainerIds)
        {
            await _dockerContainerService.Delete(serviceContainerId, force: true);
        }

        if (networkId is not null)
        {
            await _dockerNetworkService.Delete(networkId);
        }
    }
}
