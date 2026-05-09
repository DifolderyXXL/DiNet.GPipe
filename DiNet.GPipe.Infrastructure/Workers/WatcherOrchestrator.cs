using DiNet.GPipe.Domain;
using DiNet.GPipe.SharedKernel.Interfaces;
using DiNet.GPipe.SharedKernel.Models;
using DiNet.GPipe.SharedKernel.Results;
using DiNet.GPipe.SharedKernel.Watchers;
using DiNet.GPipe.Application.Workers;

namespace DiNet.GPipe.Infrastructure.Workers;

public class WatcherOrchestrator(
    IProjectService projectService,
    IProjectsRepository projectRepository,
    IProjectWatcherManager watcherManager
) : IWatcherOrchestrator
{
    public async Task InitializeAsync(CancellationToken ct)
    {
        var projects = await projectRepository.QueryAll();
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

    public async Task<Result> ChangeActiveAsync(int projectId, bool active, CancellationToken ct)
    {
        var project = await projectRepository.Get(projectId);

        if (project == null)
            return Result.Failure(null);

        project.WatcherSettings.IsActive = active;
        await projectRepository.SaveAsync();

        await watcherManager.DeleteWatcherAsync(projectId, ct);

        return Result.Success();
    }
}
