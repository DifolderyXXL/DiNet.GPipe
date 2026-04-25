using DiNet.GPipe.SharedKernel;
using DiNet.GPipe.SharedKernel.Interfaces.Messaging;
using DiNet.GPipe.SharedKernel.Watchers;

namespace DiNet.GPipe.BackgroundWorker.Branches;

public class BranchCheckWorker(TimeSpan period,
                               ICommitSource commitSource,
                               IDataRepository<BranchData> branchRepository,
                               IEventBus eventBus,
                               ILogger<BranchCheckWorker> logger) : IWatcherWorker
{
    public async Task ExecuteAsync(CancellationToken ct)
    {
        var timer = new PeriodicTimer(period);

        while (await timer.WaitForNextTickAsync(ct))
        {
            try
            {
                await CheckForNewCommits(ct);
            }
            catch(Exception e)
            {
                logger.LogError(e, "Error while checking commits for {Branch}", commitSource.BranchName);
            }
        }
    }

    private async Task CheckForNewCommits(CancellationToken ct)
    {
        var branchData = branchRepository.Get() ?? new();
        var topCommit = commitSource.GetTopCommitHash();
        var lastProcessed = branchData.LastProcessedCommitHash;

        if (topCommit == null || topCommit == lastProcessed) return;

        var nextCommit = 
            lastProcessed == null 
            ? topCommit 
            : commitSource.GetNextCommitHash(lastProcessed);

        while (nextCommit != null)
        {
            var command = new CommitDetected(
                commitSource.ProjectName,
                commitSource.BranchName,
                nextCommit,
                commitSource.TargetVersionType,
                Guid.NewGuid()
                );

            await eventBus.PublisthAsync(command, ct);

            logger.LogInformation("Event published: {Commit} for {Type}", nextCommit, commitSource.TargetVersionType);

            branchData.LastProcessedCommitHash = nextCommit;

            branchRepository.Save(branchData);

            nextCommit = commitSource.GetNextCommitHash(nextCommit);
        }
    }
}



