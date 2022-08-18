using Kanakku.Domain.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kanakku.Infrastructure.Persistence.EntityConfigurations.Inventory
{
    public class WorkEC : IEntityTypeConfiguration<Work>
    {
        public void Configure(EntityTypeBuilder<Work> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
            builder.Property(x => x.ImageId).IsRequired(false);

            builder.HasOne(x => x.Image)
                .WithMany()
                .HasForeignKey(x => x.ImageId);
            builder.HasOne(x => x.Product)
                .WithMany(x => x.Works)
                .HasForeignKey(x => x.ProductId);
            builder.HasMany(x => x.WorkHistories)
                .WithOne(x => x.Work)
                .HasForeignKey(x => x.WorkId);
            builder.HasMany(x => x.WorkCostHistories)
                .WithOne(x => x.Work)
                .HasForeignKey(x => x.WorkId);
        }
    }
}
