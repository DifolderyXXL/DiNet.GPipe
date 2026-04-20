using DiNet.GPipe.BackgroundWorker.Build;
using DiNet.GPipe.BackgroundWorker.Common;

namespace DiNet.GPipe.BackgroundWorker.Branches;

public interface IDataRepository<T>
{
    public void Save(T value);
    public T? Get();
}

public interface ILocalBranch
{
    public string Name { get; }
    public string? GetTopCommitHash();
    public string? GetNextCommitHash(string current);
}



public class BranchData
{
    public string? LastProcessedCommitHash { get; set; }
}

public class BranchCheckWorker(TimeSpan period,
                               BranchVersion versionType,
                               IBuildService buildService,
                               ILocalBranch branch,
                               IDataRepository<BranchData> repository) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var timer = new PeriodicTimer(period);

        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            var topCommit = branch.GetTopCommitHash();
            var branchData = repository.Get();
            var processedCommit = branchData?.LastProcessedCommitHash;

            if (topCommit != processedCommit)
            {
                var nextCommit = processedCommit == null ? topCommit : branch.GetNextCommitHash(processedCommit);

                await ProcessAllCommitsFromBeginning(branchData??new(), nextCommit, stoppingToken);
            }
        }
    }

    private async Task ProcessAllCommitsFromBeginning(BranchData data, string? nextCommit, CancellationToken stoppingToken)
    {
        while (nextCommit != null)
        {
            await buildService.Build(branch.Name, nextCommit, versionType, stoppingToken);

            data.LastProcessedCommitHash = nextCommit;

            repository.Save(data);

            nextCommit = branch.GetNextCommitHash(nextCommit);
        }
    }
}



