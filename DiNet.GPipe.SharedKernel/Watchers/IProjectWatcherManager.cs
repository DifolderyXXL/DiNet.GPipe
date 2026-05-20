using System.Collections.Immutable;
using DiNet.GPipe.SharedKernel.Models;

namespace DiNet.GPipe.SharedKernel.Watchers;

public interface IProjectWatcherManager
{
    public Task<int> CreateOrUpdateWatcherAsync(WatcherParameters request, CancellationToken ct);
    public Task DeleteWatcherAsync(int id, CancellationToken ct);
    public Task<Watcher?> GetWatcherAsync(int id, CancellationToken ct);
    public IEnumerable<Watcher> EnumerateAllWatchers();
    public Task UpdateIntervalAsync(int id, TimeSpan interval, CancellationToken ct);
    public Task UpdateGitUrlAsync(int id, string gitUrl, CancellationToken ct);
    public Task UpdateBranches(int id, List<BranchConfig> branches, CancellationToken ct);
}


public interface IWatcherWorker
{
    public Task ExecuteAsync(CancellationToken ct);
}

public enum WatcherStatus
{
    Created = 0,
    Stopped = 1,
    Working = 1 << 1,
    Error = 1 << 2
}

public record Watcher(
    int ProjectId,
    ProjectWatcherConfig Config,
    WatcherStatus Status
);

public record WatcherRequest(
    ProjectWatcherConfig Config,
    bool FastStart
);