using DiNet.GPipe.SharedKernel.Watchers;
using System.Collections.Concurrent;

namespace DiNet.GPipe.Infrastructure.Workers;

public record WatcherEntry(
    CancellationTokenSource CancellationTokenSource, 
    Watcher Watcher,
    BranchCheckWorker Instance);
public class WatcherManager(IServiceProvider services) : IWatcherManager
{
    private readonly ConcurrentDictionary<Guid, WatcherEntry> _watchers = [];

    public async Task<Guid> CreateWatcherAsync(WatcherRequest request, CancellationToken ct)
    {
        
        var cts = new CancellationTokenSource();

        var watcher = new Watcher(Guid.NewGuid(), request.ProjectName, request.GitUrl, branch, WatcherStatus.Created);

        var entry = new WatcherEntry(cts, watcher, new BranchCheckWorker());

        _watchers.TryAdd(Guid.NewGuid(),)
    }

    public Task DeleteWatcherAsync(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Watcher> EnumerateAllWatchers()
    {
        throw new NotImplementedException();
    }

    public Task UpdateIntervalAsync(string branchName, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}