using DiNet.GPipe.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiNet.GPipe.Infrastructure.Database.EntityConfigurations;

public class FailedBuildConfiguration : IEntityTypeConfiguration<FailedBuild>
{
    public void Configure(EntityTypeBuilder<FailedBuild> builder)
    {
        builder.Property(fb => fb.ErrorText)
               .IsRequired()
               .HasMaxLength(4000);
    }
}