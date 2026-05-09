using DiNet.GPipe.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiNet.GPipe.Infrastructure.Database.EntityConfigurations;

public class BuildRegistryTypeConfiguration : IEntityTypeConfiguration<BuildRegistry>
{
    public void Configure(EntityTypeBuilder<BuildRegistry> builder)
    {
        builder.HasKey(x => x.CommitHash);

        builder
            .HasOne(x => x.Project)
            .WithMany(x => x.Builds)
            .HasForeignKey(x=>x.ProjectId);

        builder
            .ComplexProperty(x => x.Version);
    }
}
