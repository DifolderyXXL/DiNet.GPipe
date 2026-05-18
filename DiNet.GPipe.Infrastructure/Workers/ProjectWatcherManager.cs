using DiNet.GPipe.SharedKernel.Models;
using DiNet.GPipe.SharedKernel.Watchers;
using System.Collections.Concurrent;
using DiNet.GPipe.Application.Workers;

namespace DiNet.GPipe.Infrastructure.Workers;

public class ProjectWatcherManager(IWorkerFactory workerFactory) : IProjectWatcherManager
{
    private readonly ConcurrentDictionary<int, WatcherEntry> _watchers = [];

    public async Task<int> CreateOrUpdateWatcherAsync(WatcherParameters request, CancellationToken ct)
    {
        if (_watchers.TryGetValue(request.ProjectId, out var entry))
        {
            await entry.CancellationTokenSource.CancelAsync();
            entry.Scope.Dispose();
        }

        var newEntry = workerFactory.Create(request);

        if (entry != null)
            _watchers.TryUpdate(newEntry.Watcher.ProjectId, newEntry, entry);
        else
            _watchers.TryAdd(newEntry.Watcher.ProjectId, newEntry);

        newEntry.Instance.Start(newEntry.CancellationTokenSource.Token);

        return newEntry.Watcher.ProjectId;
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

    public async Task UpdateBranches(
        int id, List<BranchConfig> branches, CancellationToken ct)
    {
        if (!_watchers.TryGetValue(id, out var entry))
            throw new InvalidOperationException("Watcher not found");

        var newParams = new WatcherParameters(
            entry.Watcher.ProjectId,
            entry.Watcher.Config with { Branches = branches }
        );

        await CreateOrUpdateWatcherAsync(newParams, ct);
    }

    public async Task UpdateIntervalAsync(int id, TimeSpan interval, CancellationToken ct)
    {
        if (!_watchers.TryGetValue(id, out var entry))
            throw new InvalidOperationException("Watcher not found");

        var newParams = new WatcherParameters(
            entry.Watcher.ProjectId,
            entry.Watcher.Config with { PollInterval = interval }
        );
        await CreateOrUpdateWatcherAsync(newParams, ct);
    }
}
