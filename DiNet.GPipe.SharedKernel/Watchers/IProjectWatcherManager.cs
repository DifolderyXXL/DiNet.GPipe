using DiNet.GPipe.SharedKernel.Models;

namespace DiNet.GPipe.SharedKernel.Watchers;

public interface IProjectWatcherManager
{
    public Task<int> CreateWatcherAsync(WatcherParameters request, CancellationToken ct);
    public Task DeleteWatcherAsync(int id, CancellationToken ct);
    public Task<Watcher?> GetWatcherAsync(int id, CancellationToken ct);
    public IEnumerable<Watcher> EnumerateAllWatchers();
    public Task UpdateIntervalAsync(string branchName, CancellationToken ct);
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
    string ProjectName,
    string GitUrl,
    List<BranchConfig> Branches,
    WatcherStatus Status
    );
public record WatcherRequest(
    string ProjectName,
    string GitUrl,
    List<BranchConfig> Branches, 
    TimeSpan PollInterval
    );
