using Kanakku.Domain.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kanakku.Infrastructure.Persistence.EntityConfigurations.Inventory
{
    public class WorkCostHistoryEC : IEntityTypeConfiguration<WorkCostHistory>
    {
        public void Configure(EntityTypeBuilder<WorkCostHistory> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasOne(x => x.Work)
                .WithMany(x => x.WorkCostHistories)
                .HasForeignKey(x => x.WorkId);
        }
    }
}
