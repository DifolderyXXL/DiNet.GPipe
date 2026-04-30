using DiNet.GPipe.Application.Project;
using DiNet.GPipe.Domain;
using DiNet.GPipe.SharedKernel.Interfaces;
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
    public Task<Result<int>> CreateAndStartWatcher(WatcherRequest request, CancellationToken ct);
}

public class WatcherOrchestrator(
    IProjectService projectService,
    IProjectWatcherManager watcherManager,
    IProjectsRepository projectsRepository
) : IWatcherOrchestrator
{
    public async Task InitializeAsync(CancellationToken ct)
    {
        var projects = projectsRepository.EnumerateAllReadonly();
        foreach (var project in projects)
        {
            if(project.WatcherSettings.IsActive)
                await StartProjectWatcherInternal(project, ct);
        }
    }

    public async Task<Result<int>> CreateAndStartWatcher(WatcherRequest request, CancellationToken ct)
    {
        var projectResult = await projectService.CreateProject(request, ct);
        if (projectResult.IsError) return projectResult.Error!;

        return await StartProjectWatcherInternal(projectResult.Value!, ct);
    }

    private async Task<int> StartProjectWatcherInternal(ProjectModel project, CancellationToken ct)
    {

        var parameters = new WatcherParameters(
            Project: project,
            Branches: [.. project.BranchConfigs.Select(x=>new BranchConfig(x.BranchName, x.VersionType))],
            Period: project.WatcherSettings.PollInterval
        );

        return await watcherManager.CreateWatcherAsync(parameters, ct);
    }
}