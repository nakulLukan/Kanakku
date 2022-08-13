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
    public class LookupDetailEC : IEntityTypeConfiguration<LookupDetail>
    {
        public void Configure(EntityTypeBuilder<LookupDetail> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Value).IsRequired().HasMaxLength(255);

            builder.HasOne(x => x.LookupMaster)
                .WithMany(x => x.LookupDetails)
                .HasForeignKey(x => x.LookupMasterId);
        }
    }
}
