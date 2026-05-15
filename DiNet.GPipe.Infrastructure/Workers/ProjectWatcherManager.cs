using DiNet.GPipe.SharedKernel.Models;
using DiNet.GPipe.SharedKernel.Watchers;
using System.Collections.Concurrent;
using DiNet.GPipe.Application.Workers;

namespace DiNet.GPipe.Infrastructure.Workers;
public class ProjectWatcherManager(IWorkerFactory workerFactory) : IProjectWatcherManager
{
    private readonly ConcurrentDictionary<int, WatcherEntry> _watchers = [];

    public async Task<int> CreateWatcherAsync(WatcherParameters request, CancellationToken ct)
    {
        var entry = workerFactory.Create(request.Project, request);

        _watchers.TryAdd(entry.Watcher.ProjectId, entry);

        entry.Instance.Start(entry.CancellationTokenSource.Token);

        return entry.Watcher.ProjectId;
    }

    public async Task DeleteWatcherAsync(int id, CancellationToken ct)
    {
        if (_watchers.TryRemove(id, out var entry))
        {
            await entry.CancellationTokenSource.CancelAsync();

            entry.Scope.Dispose();
        }
    }

    public IEnumerable<Watcher> EnumerateAllWatchers()
    {
        return _watchers.Values.Select(x => x.Watcher);
    }

    public async Task<Watcher?> GetWatcherAsync(int id, CancellationToken ct)
    {
        if (_watchers.TryGetValue(id, out var entry))
            return entry.Watcher;

        return null;
    }

    public async Task UpdateBranches(int id, List<BranchConfig> branches, CancellationToken ct)
    {
        if(_watchers.TryGetValue(id, out var entry))
        {
            entry.Watcher.Branches.Clear();
            entry.Watcher.Branches.AddRange(branches);
        }
    }

    public async Task UpdateIntervalAsync(string branchName, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
