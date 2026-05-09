using DiNet.GPipe.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiNet.GPipe.Infrastructure.Database.EntityConfigurations;

public class TestEntryTypeConfiguration : IEntityTypeConfiguration<TestEntry>
{
    public void Configure(EntityTypeBuilder<TestEntry> builder)
    {
        builder.HasKey(x => x.Id);
    }
}