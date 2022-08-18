using Kanakku.Domain.Lookup;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kanakku.Infrastructure.Persistence.EntityConfigurations.Lookup
{
    public class LookupMasterEC : IEntityTypeConfiguration<LookupMaster>
    {
        public void Configure(EntityTypeBuilder<LookupMaster> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.InternalName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.CreatedBy).IsRequired(false);
            builder.Property(x => x.ModifiedBy).IsRequired(false);
            builder.Property(x => x.DependentLookupMasterId).IsRequired(false);

            builder.HasMany(x => x.LookupDetails)
                .WithOne(x => x.LookupMaster)
                .HasForeignKey(x => x.LookupMasterId);

            builder.HasOne(x => x.DependentLookupMaster)
                .WithMany()
                .HasForeignKey(x => x.DependentLookupMasterId)
                .HasPrincipalKey(x => x.Id);
        }
    }
}
