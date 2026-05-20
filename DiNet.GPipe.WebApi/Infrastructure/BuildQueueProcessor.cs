using DiNet.GPipe.BuildingApplication.Handlers;
using DiNet.GPipe.Infrastructure.Building.V2;
using DiNet.GPipe.WebApi.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace DiNet.GPipe.WebApi.Infrastructure;

public class BuildQueueProcessor(
    IBuildQueue buildQueue,
    IServiceScopeFactory scopeFactory,
    IActiveBuildTracker tracker,
    IHubContext<BuildLogHub> logHub) : BackgroundService
{
    public const int MaxBuildUnits = 2;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var semaphore = new SemaphoreSlim(MaxBuildUnits);

        await foreach (var request in buildQueue.ReaderAllAsync(stoppingToken))
        {
            await semaphore.WaitAsync(stoppingToken);

            _ = Task.Run(async () =>
            {
                var buildId = Guid.NewGuid().ToString("N");
                var logger = new SignalRForwardingLogger(new ProcessLogger(), logHub, buildId);

                tracker.TrackActive(buildId, request.ProjectId, request.ProjectName, logger);

                try
                {
                    using var scope = scopeFactory.CreateScope();
                    var builderUnit = scope.ServiceProvider.GetRequiredService<IBuilderUnit>();

                    tracker.UpdateState(buildId, BuilderState.Running);

                    var result = await builderUnit.RunAsync(request, logger, stoppingToken);

                    if (result.IsError)
                    {
                        tracker.UpdateState(buildId, BuilderState.Failed);
                    }
                    else
                    {
                        tracker.UpdateState(buildId, BuilderState.Completed);

                        //await storage.Store(result.Value!, request.ProjectName, request.CommitHash, stoppingToken);
                    }
                }
                catch (Exception ex)
                {
                    logger.Log($"Unexpected processing engine failure: {ex.Message}");
                    tracker.UpdateState(buildId, BuilderState.Failed);
                }
                finally
                {
                    semaphore.Release();
                }
            });
        }
    }
}