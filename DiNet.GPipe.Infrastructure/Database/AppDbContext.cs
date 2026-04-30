using DiNet.GPipe.Domain;
using Microsoft.EntityFrameworkCore;

namespace DiNet.GPipe.Infrastructure.Database;

public class AppDbContext : DbContext
{
    public DbSet<BuildRegistry> BuildRegistries => Set<BuildRegistry>();
    public DbSet<ProjectModel> Projects => Set<ProjectModel>();
    public DbSet<WatcherSettings> WatcherSettings => Set<WatcherSettings>();
    public DbSet<BranchWatcherConfig> BranchWatcherConfigs => Set<BranchWatcherConfig>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<BuildRegistry>()
            .HasKey(x => x.CommitHash);
            
        mb.Entity<BuildRegistry>()
            .HasOne(x=>x.Project)
            .WithMany(x=>x.Builds);

        mb.Entity<BuildRegistry>()
            .ComplexProperty(x => x.Version);


        mb.Entity<ProjectModel>()
            .HasKey(x => x.Id);

        mb.Entity<ProjectModel>()
            .HasOne(x => x.WatcherSettings)
            .WithOne()
            .HasForeignKey<WatcherSettings>(x=>x.ProjectId)
            .IsRequired();

        mb.Entity<ProjectModel>()
            .HasMany(x => x.BranchConfigs)
            .WithOne()
            .HasForeignKey(x => x.ProjectId)
            .IsRequired();

        mb.Entity<ProjectModel>().HasIndex(x => x.GitUrl).IsUnique();
        mb.Entity<ProjectModel>().HasIndex(x => x.Name).IsUnique();
    }
}
