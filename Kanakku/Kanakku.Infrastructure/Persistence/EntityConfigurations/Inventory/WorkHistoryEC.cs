using Kanakku.Domain.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanakku.Infrastructure.Persistence.EntityConfigurations.Inventory
{
    public class WorkHistoryEC : IEntityTypeConfiguration<WorkHistory>
    {
        public void Configure(EntityTypeBuilder<WorkHistory> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasOne(x => x.Work)
                .WithMany(x => x.WorkHistories)
                .HasForeignKey(x => x.WorkId);
            builder.HasOne(x => x.Employee)
                .WithMany(x => x.WorkHistories)
                .HasForeignKey(x => x.EmployeeId);
        }
    }
}
