using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TimLearning.Shared.BackgroundTask;

public abstract class BackgroundPeriodicalTask<TTask> : BackgroundService
    where TTask : BackgroundPeriodicalTask<TTask>
{
    private readonly TimeSpan _defaultPeriod = TimeSpan.FromMinutes(1);
    private readonly IServiceProvider _serviceProvider;

    protected readonly ILogger<TTask> Logger;
    protected virtual TimeSpan Period => _defaultPeriod;

    protected BackgroundPeriodicalTask(ILogger<TTask> logger, IServiceProvider serviceProvider)
    {
        Logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await StartTaskAsync(stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            await ExecuteOnceAsync(stoppingToken);

            await Task.Delay(Period, stoppingToken);
        }
    }

    protected virtual Task StartTaskCoreAsync(
        IServiceProvider scopedServiceProvider,
        CancellationToken cancellationToken
    )
    {
        return Task.CompletedTask;
    }

    protected virtual Task OnExceptionForExecuteOnceAsync(Exception e)
    {
        return Task.CompletedTask;
    }

    protected abstract Task ExecuteOnceCoreAsync(
        IServiceProvider scopedServiceProvider,
        CancellationToken cancellationToken
    );

    private async Task StartTaskAsync(CancellationToken stoppingToken)
    {
        try
        {
            await using var scope = _serviceProvider.CreateAsyncScope();
            await StartTaskCoreAsync(scope.ServiceProvider, stoppingToken);
        }
        catch (Exception e) when (e is not OperationCanceledException)
        {
            Logger.LogError(e, "Background periodical task start with error.");
            throw;
        }
    }

    private async Task ExecuteOnceAsync(CancellationToken stoppingToken)
    {
        try
        {
            await using var scope = _serviceProvider.CreateAsyncScope();
            await ExecuteOnceCoreAsync(scope.ServiceProvider, stoppingToken);
        }
        catch (Exception e) when (e is not OperationCanceledException)
        {
            await OnExceptionForExecuteOnceAsync(e);
            Logger.LogError(e, "Background periodical task processing with error.");
        }
    }
}
