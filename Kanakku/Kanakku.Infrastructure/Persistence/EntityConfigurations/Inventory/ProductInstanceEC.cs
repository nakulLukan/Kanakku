using Kanakku.Domain.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kanakku.Infrastructure.Persistence.EntityConfigurations.Inventory
{
    public class ProductInstanceEC : IEntityTypeConfiguration<ProductInstance>
    {
        public void Configure(EntityTypeBuilder<ProductInstance> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Product)
                .WithMany(x => x.ProductInstances)
                .HasForeignKey(x => x.ProductId);

            builder.HasOne(x => x.ProductSize)
                .WithMany()
                .HasForeignKey(x => x.ProductSizeId);
        }
    }
}
