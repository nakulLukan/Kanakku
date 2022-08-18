using Kanakku.Domain.Inventory;
using Kanakku.Domain.Lookup;
using Kanakku.Shared;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Infrastructure.Seeder
{
    public static class ProductSizeSeeder
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            int id = 1;
            List<ProductSize> sizes = new List<ProductSize>();
            sizes.Add(new()
            {
                Id = id++,
                InternalName = "small",
                Order = 1,
                Size = "S"
            });
            sizes.Add(new()
            {
                Id = id++,
                InternalName = "medium",
                Order = 2,
                Size = "M"
            });
            sizes.Add(new()
            {
                Id = id,
                InternalName = "large",
                Order = 3,
                Size = "L"
            });
            

            foreach (var seed in sizes)
            {
                modelBuilder.Entity<ProductSize>().HasData(seed);
            }
        }
    }
}
