using DiNet.GPipe.Application.Project;
using DiNet.GPipe.Domain;
using DiNet.GPipe.SharedKernel.Interfaces;
using DiNet.GPipe.SharedKernel.Watchers;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace DiNet.GPipe.Application.Workers;

public record WatcherEntry(
    CancellationTokenSource CancellationTokenSource,
    Watcher Watcher,
    IServiceScope Scope,
    BranchCheckWorker Instance);
public class WatcherManager(IProjectService projectService, IWorkerFactory workerFactory) : IWatcherManager
{
    private readonly ConcurrentDictionary<Guid, WatcherEntry> _watchers = [];

    public async Task<Guid> CreateWatcherAsync(WatcherRequest request, CancellationToken ct)
    {
        var project = await projectService.GetOrCreateProject(request);

        var entry = workerFactory.Create(project, request);

        _watchers.TryAdd(entry.Watcher.Id, entry);

        entry.Instance.Start(entry.CancellationTokenSource.Token);

        return entry.Watcher.Id;
    }

    public async Task DeleteWatcherAsync(Guid id, CancellationToken ct)
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

    public async Task UpdateIntervalAsync(string branchName, CancellationToken ct)
    {

    }
}

