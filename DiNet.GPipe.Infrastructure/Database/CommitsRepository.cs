using DiNet.GPipe.Domain;
using DiNet.GPipe.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiNet.GPipe.Infrastructure.Database;

public class CommitsRepository(AppDbContext context) : ICommitsRepository
{
    public async Task Add(CommitEntry commitEntry)
    {
        await context.CommitEntries.AddAsync(commitEntry);
        await context.SaveChangesAsync();
    }

    private IQueryable<CommitEntry> QueryActivity()
        => context.CommitEntries
            .Include(x => x.SuccessfullBuilds)
            .Include(x => x.TestEntries)
            .Include(x => x.FailedBuilds)
            .AsNoTracking();

    public async Task<List<CommitEntry>> QueryWithActivity()
    {
        return await QueryActivity()
            .ToListAsync();
    }

    public async Task<List<CommitEntry>> QueryWithActivity(int projectId)
    {
        return await QueryActivity()
            .Where(x => x.ProjectId == projectId)
            .ToListAsync();
    }

    public async Task<List<CommitEntry>> QueryWithoutActivity()
    {
        return await context.CommitEntries
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<CommitEntry>> QueryWithoutActivity(int projectId)
    {
        return await context.CommitEntries
           .AsNoTracking()
           .Where(x => x.ProjectId == projectId)
           .ToListAsync();
    }
}

