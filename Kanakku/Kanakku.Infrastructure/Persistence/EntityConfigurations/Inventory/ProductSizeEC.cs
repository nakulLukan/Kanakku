using Kanakku.Domain.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kanakku.Infrastructure.Persistence.EntityConfigurations.Inventory;

public class ProductSizeEC : IEntityTypeConfiguration<ProductSize>
{
    public void Configure(EntityTypeBuilder<ProductSize> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Size).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Order).IsRequired();
        builder.Property(x => x.InternalName).IsRequired(false).HasMaxLength(20);
    }
}
