using Kanakku.Domain.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kanakku.Infrastructure.Persistence.EntityConfigurations.Inventory
{
    public class ProductWorkInstanceEC : IEntityTypeConfiguration<ProductWorkInstance>
    {
        public void Configure(EntityTypeBuilder<ProductWorkInstance> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.ProductInstance)
                .WithMany(x => x.ProductWorkInstances)
                .HasForeignKey(x => x.ProductInstanceId);

            builder.HasOne(x => x.Work)
                .WithMany(x => x.ProductWorkInstances)
                .HasForeignKey(x => x.WorkId);
        }
    }
}
