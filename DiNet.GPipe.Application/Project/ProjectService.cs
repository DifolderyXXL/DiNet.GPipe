using DiNet.GPipe.Domain;
using DiNet.GPipe.SharedKernel.Extensions;
using DiNet.GPipe.SharedKernel.Interfaces;
using DiNet.GPipe.SharedKernel.Results;
using DiNet.GPipe.SharedKernel.Watchers;
using Microsoft.Extensions.DependencyInjection;

namespace DiNet.GPipe.Application.Project;

public class ProjectService(IServiceScopeFactory scopeFactory) : IProjectService
{
    public async Task<Result<ProjectModel>> CreateProject(WatcherRequest request, CancellationToken ct)
    {
        using var initScope = scopeFactory.CreateScope();
        var projectsRepository = initScope.ServiceProvider.GetRequiredService<IProjectsRepository>();

        var project = await projectsRepository.GetByGitUrl(request.GitUrl);

        if(project == null)
        {
            project = new() 
            { 
                Name = request.ProjectName, 
                GitUrl = request.GitUrl,
                WatcherSettings = new()
                {
                    PollInterval = request.PollInterval,
                    IsActive = request.FastStart
                },
                BranchConfigs = request.Branches.Select(
                    x=>new BranchWatcherConfig() { BranchName = x.BranchName, VersionType = x.VersionType }).ToList()
            };
            await projectsRepository.Add(project);
            await projectsRepository.SaveAsync();

            return project;
        }

        return ProjectErrors.ProjectAlreadyExists(request.GitUrl);
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
}