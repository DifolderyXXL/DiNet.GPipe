using DiNet.GPipe.BackgroundWorker.Branches;
using DiNet.GPipe.SharedKernel.Models;
using DiNet.GPipe.SharedKernel.Results;
using DiNet.GPipe.SharedKernel.Watchers;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace DiNet.GPipe.Application.Workers;

public record WatcherEntry(
    CancellationTokenSource CancellationTokenSource,
    Watcher Watcher,
    IServiceScope Scope,
    BranchCheckWorker Instance);
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

    public async Task UpdateIntervalAsync(string branchName, CancellationToken ct)
    {

    }
}


public interface IWatcherOrchestrator
{
    public Task InitializeAsync(CancellationToken ct);
    public Task<Result> ChangeActiveAsync(int projectId, bool active, CancellationToken ct);
    public Task<Result<int>> CreateAndStartWatcher(WatcherRequest request, CancellationToken ct);
}
