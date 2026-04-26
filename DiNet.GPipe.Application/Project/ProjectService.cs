using DiNet.GPipe.Domain;
using DiNet.GPipe.SharedKernel.Interfaces;
using DiNet.GPipe.SharedKernel.Watchers;
using Microsoft.Extensions.DependencyInjection;

namespace DiNet.GPipe.Application.Project;

public class ProjectService(IServiceScopeFactory scopeFactory) : IProjectService
{
    public async Task<ProjectModel> GetOrCreateProject(WatcherRequest request)
    {
        using var initScope = scopeFactory.CreateScope();
        var projectsRepository = initScope.ServiceProvider.GetRequiredService<IProjectsRepository>();

        var project = await projectsRepository.GetByGitUrl(request.GitUrl);
        if (project == null)
        {
            project = new() { Name = request.ProjectName, GitUrl = request.GitUrl };
            await projectsRepository.Add(project);
            await projectsRepository.SaveAsync();
        }

        return project;
    }
}