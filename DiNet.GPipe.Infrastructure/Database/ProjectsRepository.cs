using DiNet.GPipe.Domain;
using DiNet.GPipe.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DiNet.GPipe.Infrastructure.Database;

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

    public async Task<bool> Delete(int id)
    {
        var entity = await context.Projects.FindAsync(id);

        if (entity == null) return false;

        context.Projects.Remove(entity);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<List<ProjectModel>> QueryAll()
    {
        return await context.Projects
            .Include(x => x.WatcherSettings)
            .Include(x => x.BranchConfigs)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ProjectModel?> Get(int id)
    {
        return await context.Projects
            .Include(x => x.BranchConfigs)
            .Include(x => x.WatcherSettings)
            .Include(x => x.Commits)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<ProjectModel?> GetByGitUrl(string gitUrl)
    {
        return await context.Projects.FirstOrDefaultAsync(x => x.GitUrl == gitUrl);
    }

    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }
}
