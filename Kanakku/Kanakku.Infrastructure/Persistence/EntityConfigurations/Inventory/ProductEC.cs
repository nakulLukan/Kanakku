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
            builder.Property(x => x.ShortCode).IsRequired().HasMaxLength(30);
            builder.Property(x => x.ImageId).IsRequired(false);
            builder.Property(x => x.CreatedBy).IsRequired(false).HasMaxLength(255);
            builder.Property(x => x.ModifiedBy).IsRequired(false).HasMaxLength(255);

            builder.HasIndex(u => u.ShortCode).IsUnique();

            builder.HasOne(x => x.Image)
                .WithMany()
                .HasForeignKey(x => x.ImageId);
            builder.HasMany(x => x.Works)
               .WithOne(x => x.Product)
               .HasForeignKey(x => x.ProductId);
        }
    }
}
