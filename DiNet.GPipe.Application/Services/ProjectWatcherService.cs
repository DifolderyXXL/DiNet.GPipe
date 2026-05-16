using DiNet.GPipe.Domain;
using DiNet.GPipe.SharedKernel.Watchers;

namespace DiNet.GPipe.Application.Service;

public class ProjectWatcherService(IProjectWatcherManager manager)
{
    public async Task SyncBranchesAsync(ProjectModel project)
    {
        await manager.UpdateBranches(
            project.Id,
            [.. project.BranchConfigs.Select(x => new BranchConfig(x.BranchName, x.VersionType))],
            default);
    }
}
