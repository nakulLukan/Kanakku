using Kanakku.Domain.Lookup;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanakku.Infrastructure.Persistence.EntityConfigurations.Lookup
{
    public class LookupMasterEC : IEntityTypeConfiguration<LookupMaster>
    {
        public void Configure(EntityTypeBuilder<LookupMaster> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.InternalName).IsRequired().HasMaxLength(50);

            builder.HasMany(x => x.LookupDetails)
                .WithOne(x => x.LookupMaster)
                .HasForeignKey(x => x.LookupMasterId);
        }
    }
}
