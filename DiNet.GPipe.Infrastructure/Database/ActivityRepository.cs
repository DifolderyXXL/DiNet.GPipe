using DiNet.GPipe.Domain;

namespace DiNet.GPipe.Infrastructure.Database;

public class ActivityRepository(AppDbContext context) : IActivityRepository
{
    public async Task AddBuild(Build build)
    {
        await context.Builds.AddAsync(build);
    }

    public async Task AddTest(TestEntry test)
    {
        await context.TestEntries.AddAsync(test);
    }
}