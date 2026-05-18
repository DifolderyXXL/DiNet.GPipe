using DiNet.GPipe.Application.Project;
using DiNet.GPipe.Domain;
using DiNet.GPipe.SharedKernel.Extensions;
using DiNet.GPipe.SharedKernel.Interfaces;
using DiNet.GPipe.SharedKernel.Results;
using DiNet.GPipe.SharedKernel.Watchers;
using Microsoft.Extensions.DependencyInjection;

namespace DiNet.GPipe.Infrastructure.Project;

public class ProjectService(IServiceScopeFactory scopeFactory) : IProjectService
{
    public async Task<Result<ProjectModel>> CreateProject(WatcherRequest request, CancellationToken ct)
    {
        using var initScope = scopeFactory.CreateScope();
        var projectsRepository = initScope.ServiceProvider.GetRequiredService<IProjectsRepository>();

        var project = await projectsRepository.GetByGitUrl(request.Config.GitUrl);

        if (project == null)
        {
            project = new()
            {
                Name = request.Config.ProjectName,
                GitUrl = request.Config.GitUrl,
                WatcherSettings = new()
                {
                    PollInterval = request.Config.PollInterval,
                    IsActive = request.FastStart
                },
                BranchConfigs = request.Config.Branches.Select(
                    x => new BranchWatcherConfig() { BranchName = x.BranchName, VersionType = x.VersionType }).ToList()
            };
            await projectsRepository.Add(project);
            await projectsRepository.SaveAsync();

            return project;
        }

        return ProjectErrors.ProjectAlreadyExists(request.Config.GitUrl);
    }


    public async Task<Result> DeleteProject(int id, CancellationToken ct)
    {
        using var initScope = scopeFactory.CreateScope();
        var projectsRepository = initScope.ServiceProvider.GetRequiredService<IProjectsRepository>();

        if (await projectsRepository.Delete(id))
            return Result.Success();

        return ProjectErrors.ProjectConflict().AsResult();
    }

    public async Task<Result<ProjectModel>> GetProjectById(int id, CancellationToken ct)
    {
        using var initScope = scopeFactory.CreateScope();
        var projectsRepository = initScope.ServiceProvider.GetRequiredService<IProjectsRepository>();

        var project = await projectsRepository.Get(id);
        return project;
    }

    public async Task<Result<ProjectModel>> GetProjectByUrl(string gitUrl, CancellationToken ct)
    {
        using var initScope = scopeFactory.CreateScope();
        var projectsRepository = initScope.ServiceProvider.GetRequiredService<IProjectsRepository>();

        var project = await projectsRepository.GetByGitUrl(gitUrl);
        return project;
    }

    public async Task<Result> UpdateProjectName(int id, string newName, CancellationToken ct)
    {
        using var initScope = scopeFactory.CreateScope();
        var projectsRepository = initScope.ServiceProvider.GetRequiredService<IProjectsRepository>();

        var project = await projectsRepository.Get(id);
        if (project == null)
            return ProjectErrors.ProjectNotFound().AsResult();

        project.Name = newName;
        await projectsRepository.SaveAsync();

        return Result.Success();
    }

    public async Task<Result> UpdateGitUrl(int id, string newGitUrl, CancellationToken ct)
    {
        using var initScope = scopeFactory.CreateScope();
        var projectsRepository = initScope.ServiceProvider.GetRequiredService<IProjectsRepository>();

        var existingProject = await projectsRepository.GetByGitUrl(newGitUrl);
        if (existingProject != null && existingProject.Id != id)
            return ProjectErrors.ProjectAlreadyExists(newGitUrl);

        var project = await projectsRepository.Get(id);
        if (project == null)
            return ProjectErrors.ProjectNotFound().AsResult();

        project.GitUrl = newGitUrl;
        await projectsRepository.SaveAsync();

        return Result.Success();
    }
}