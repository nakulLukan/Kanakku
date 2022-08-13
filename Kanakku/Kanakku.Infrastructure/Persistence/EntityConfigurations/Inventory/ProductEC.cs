using Kanakku.Domain.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kanakku.Infrastructure.Persistence.EntityConfigurations.Inventory
{
    public class ProductEC : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);

            builder.HasOne(x => x.Image)
                .WithMany()
                .HasForeignKey(x => x.ImageId);
            builder.HasMany(x => x.Works)
               .WithOne(x => x.Product)
               .HasForeignKey(x => x.ProductId);
        }
    }
}
