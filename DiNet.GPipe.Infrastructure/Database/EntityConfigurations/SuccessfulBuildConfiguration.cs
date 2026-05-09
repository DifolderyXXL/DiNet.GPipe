using DiNet.GPipe.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiNet.GPipe.Infrastructure.Database.EntityConfigurations;

public class SuccessfulBuildConfiguration : IEntityTypeConfiguration<SuccessfullBuild>
{
    public void Configure(EntityTypeBuilder<SuccessfullBuild> builder)
    {
        builder.Property(sb => sb.ApkUrl)
               .IsRequired()
               .HasMaxLength(500);
    }
}
