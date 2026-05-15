using DiNet.GPipe.Domain;
using DiNet.GPipe.SharedKernel.Interfaces;
using DiNet.GPipe.SharedKernel.Watchers;

namespace DiNet.GPipe.Application.Service;

public class ProjectService(IProjectsRepository projectsRepository, IProjectWatcherManager manager)
{
    private async Task UpdateBranches(ProjectModel project)
    {
        await manager.UpdateBranches(
            project.Id,
            project.BranchConfigs.Select(x => new BranchConfig(x.BranchName, x.VersionType)).ToList(),
            default);
    }

    public async Task<bool> AddBranch(int projectId, BranchConfig branch)
    {
        var project = await projectsRepository.Get(projectId);

        if (project == null)
            return false;

        if (project.BranchConfigs.Any(x => x.BranchName == branch.BranchName))
            return false;

        project.BranchConfigs.Add(new()
        {
            BranchName = branch.BranchName,
            VersionType = branch.VersionType,
        });

        await projectsRepository.SaveAsync();

        await UpdateBranches(project);

        return true;
    }

    public async Task<bool> RemoveBranch(int projectId, string branchName)
    {
        var project = await projectsRepository.Get(projectId);

        if (project == null)
            return false;

        int cnt = project.BranchConfigs.RemoveAll(x=>x.BranchName.Equals(branchName));

        if (cnt > 0)
        {
            await projectsRepository.SaveAsync();

            await UpdateBranches(project);
        }


        return true;
    }

    public async Task<bool> UpdateBranch(int projectId, string branchName, BranchConfig newValue)
    {
        var project = await projectsRepository.Get(projectId);

        if (project == null)
            return false;

        var branch = project.BranchConfigs.FirstOrDefault(x=>x.BranchName == branchName);
        if (branch == null)
            return false;

        branch.BranchName = newValue.BranchName;
        branch.VersionType = newValue.VersionType;

        await projectsRepository.SaveAsync();

        await UpdateBranches(project);

        return true;
    }
}