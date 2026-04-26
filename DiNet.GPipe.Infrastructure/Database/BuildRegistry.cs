using DiNet.GPipe.Domain;
using DiNet.GPipe.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiNet.GPipe.Infrastructure.Database;

public class BuildRegistryRepository(AppDbContext context) : IBuildRegistryRepository
{
    public async Task Add(BuildRegistry build)
    {
        await context.AddAsync(build);
    }

    public async Task<bool> Contains(string hash)
    {
        return await context.BuildRegistries.FindAsync(hash) != null;
    }

    public Task CreateFailedRecord(BuildVersion? version, string commitHash, string error)
    {
        throw new NotImplementedException();
    }

    public async Task<BuildRegistry?> Get(string hash)
    {
        return await context.BuildRegistries.FindAsync(hash);
    }

    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }

    public async Task UpdateStatus(string commitHash, BuildStatus status)
    {
        var build = await Get(commitHash);

        if (build == null) return;

        build.Status = status;

        await SaveAsync();
    }
}

public class ProjectsRepository(AppDbContext context) : IProjectsRepository
{
    public async Task Add(ProjectModel project)
    {
        context.Projects.Add(project);
    }

    public async Task<bool> Contains(int id)
    {
        return await context.Projects.FindAsync(id) != null;
    }

    public async Task<ProjectModel?> Get(int id)
    {
        return await context.Projects.FindAsync(id);
    }

    public async Task<ProjectModel?> GetByGitUrl(string gitUrl)
    {
        return await context.Projects.FirstOrDefaultAsync(x => x.GitUrl == gitUrl);    }

    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }
}