using DiNet.GPipe.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiNet.GPipe.Infrastructure.Database.EntityConfigurations;

public class CommitEntryTypeConfiguration : IEntityTypeConfiguration<CommitEntry>
{
    public void Configure(EntityTypeBuilder<CommitEntry> builder)
    {
        builder.HasKey(x => x.Id);

        builder.ComplexProperty(x => x.BuildVersion);

        builder.HasMany(x => x.TestEntries).WithOne()
            .HasForeignKey(x=>x.CommitId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.SuccessfullBuilds).WithOne()
            .HasForeignKey(x => x.CommitId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.FailedBuilds).WithOne()
            .HasForeignKey(x => x.CommitId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
