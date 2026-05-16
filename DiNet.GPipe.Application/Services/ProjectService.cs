using DiNet.GPipe.Application.Project;
using DiNet.GPipe.Domain;
using DiNet.GPipe.SharedKernel.Interfaces;
using DiNet.GPipe.SharedKernel.Results;
using DiNet.GPipe.SharedKernel.Watchers;

namespace DiNet.GPipe.Application.Service;

public class BranchManagementService(IProjectsRepository projectsRepository, ProjectWatcherService projectWatcherService)
{
    public async Task<Result<BranchWatcherConfig>> AddBranch(int projectId, BranchConfig branch)
    {
        var project = await projectsRepository.Get(projectId);

        if (project == null)
            return ProjectErrors.ProjectNotFound();

        if (project.BranchConfigs.Any(x => x.BranchName == branch.BranchName))
            return BranchErrors.BranchNotFound();

        var branchEntity = new BranchWatcherConfig()
        {
            BranchName = branch.BranchName,
            VersionType = branch.VersionType,
        };
        project.BranchConfigs.Add(branchEntity);

        await projectsRepository.SaveAsync();

        await projectWatcherService.SyncBranchesAsync(project);

        return branchEntity;
    }


    public async Task<Result<bool>> RemoveBranch(int projectId, string branchName)
    {
        var project = await projectsRepository.Get(projectId);

        if (project == null)
            return ProjectErrors.ProjectNotFound();

        int cnt = project.BranchConfigs.RemoveAll(x=>x.BranchName.Equals(branchName));

        if (cnt > 0)
        {
            await projectsRepository.SaveAsync();

            await projectWatcherService.SyncBranchesAsync(project);     
        }


        return true;
    }

    public async Task<Result> UpdateBranch(int projectId, string branchName, BranchConfig newValue)
    {
        var project = await projectsRepository.Get(projectId);

        if (project == null)
            return ProjectErrors.ProjectNotFound();

        var branch = project.BranchConfigs.FirstOrDefault(x=>x.BranchName == branchName);
        if (branch == null)
            return BranchErrors.BranchNotFound();

        branch.BranchName = newValue.BranchName;
        branch.VersionType = newValue.VersionType;

        await projectsRepository.SaveAsync();

        await projectWatcherService.SyncBranchesAsync(project);

        return Result.Success();
    }
}