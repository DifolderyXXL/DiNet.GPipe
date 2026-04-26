namespace DiNet.GPipe.SharedKernel.Watchers;

public interface IWatcherManager
{
    public Task<Guid> CreateWatcherAsync(WatcherRequest request, CancellationToken ct);
    public Task DeleteWatcherAsync(Guid id, CancellationToken ct);
    public Task<Watcher?> GetWatcherAsync(Guid id, CancellationToken ct);
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
    Guid Id, 
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
