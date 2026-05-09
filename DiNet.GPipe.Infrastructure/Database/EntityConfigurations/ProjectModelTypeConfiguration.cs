using DiNet.GPipe.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiNet.GPipe.Infrastructure.Database.EntityConfigurations;

public class ProjectModelTypeConfiguration : IEntityTypeConfiguration<ProjectModel>
{
    public void Configure(EntityTypeBuilder<ProjectModel> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Commits)
            .WithOne()
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.WatcherSettings)
            .WithOne()
            .HasForeignKey<WatcherSettings>(x => x.ProjectId)
            .IsRequired();

        builder
            .HasMany(x => x.BranchConfigs)
            .WithOne()
            .HasForeignKey(x => x.ProjectId)
            .IsRequired();

        builder.HasIndex(x => x.GitUrl).IsUnique();
        builder.HasIndex(x => x.Name).IsUnique();
    }
}
