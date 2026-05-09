using DiNet.GPipe.Domain;
using DiNet.GPipe.Infrastructure.Database.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DiNet.GPipe.Infrastructure.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<BuildRegistry> BuildRegistries => Set<BuildRegistry>();
    public DbSet<ProjectModel> Projects => Set<ProjectModel>();
    public DbSet<WatcherSettings> WatcherSettings => Set<WatcherSettings>();
    public DbSet<BranchWatcherConfig> BranchWatcherConfigs => Set<BranchWatcherConfig>();

    public DbSet<SuccessfullBuild> SuccessfullBuilds => Set<SuccessfullBuild>();
    public DbSet<FailedBuild> FailedBuilds => Set<FailedBuild>();
    public DbSet<Build> Builds => Set<Build>();

    public DbSet<TestEntry> TestEntries => Set<TestEntry>();
    public DbSet<CommitEntry> CommitEntries => Set<CommitEntry>();


    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.ApplyConfiguration(new ProjectModelTypeConfiguration());
        mb.ApplyConfiguration(new BuildRegistryTypeConfiguration());

        mb.ApplyConfiguration(new CommitEntryTypeConfiguration());

        mb.ApplyConfiguration(new BuildConfiguration());
        mb.ApplyConfiguration(new SuccessfulBuildConfiguration());
        mb.ApplyConfiguration(new FailedBuildConfiguration());

        mb.ApplyConfiguration(new TestEntryTypeConfiguration());
    }
}

public class AggregateRepository<T, TKey>(
    DbSet<T> dbSet, IQueryable<T> aggregateQuery,
    Expression<Func<T, TKey>> keySelector) where T : class
{
    public T? Find(TKey id) => aggregateQuery.FirstOrDefault(BuildKeyPredicate(id));

    private Expression<Func<T, bool>> BuildKeyPredicate(TKey? id)
    {
        var match = Expression.Equal(keySelector.Body, Expression.Constant(id));

        return Expression.Lambda<Func<T, bool>>(match, keySelector.Parameters);
    }

    public void Add(T entity)
    {
        dbSet.Add(entity);
    }

    public void Remove(T entity)
    {
        dbSet.Remove(entity);
    }
}