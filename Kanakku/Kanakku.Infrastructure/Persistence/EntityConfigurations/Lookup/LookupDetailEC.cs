using Kanakku.Domain.Lookup;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kanakku.Infrastructure.Persistence.EntityConfigurations.Lookup
{
    public class LookupDetailEC : IEntityTypeConfiguration<LookupDetail>
    {
        public void Configure(EntityTypeBuilder<LookupDetail> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Value).IsRequired().HasMaxLength(255);
            builder.Property(x => x.CreatedBy).IsRequired(false);
            builder.Property(x => x.ModifiedBy).IsRequired(false);
            builder.Property(x => x.DependentLookupDetailId).IsRequired(false);

            builder.HasOne(x => x.LookupMaster)
                .WithMany(x => x.LookupDetails)
                .HasForeignKey(x => x.LookupMasterId);

            builder.HasOne(x => x.DependentLookupDetail)
                .WithMany()
                .HasForeignKey(x => x.DependentLookupDetailId)
                .HasPrincipalKey(x => x.Id);
        }
    }
}
