using Kanakku.Domain.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kanakku.Infrastructure.Persistence.EntityConfigurations.Inventory;

public class ProductSizeMasterEC : IEntityTypeConfiguration<ProductSizeMaster>
{
    public void Configure(EntityTypeBuilder<ProductSizeMaster> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.MasterName).IsRequired().HasMaxLength(100);
    }
}
