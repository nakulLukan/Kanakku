using Kanakku.Domain.Inventory;
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
                Size = "S",
                MasterId = (int)SizeGroup.General
            });
            sizes.Add(new()
            {
                Id = id++,
                InternalName = "medium",
                Order = 2,
                Size = "M",
                MasterId = (int)SizeGroup.General
            });
            sizes.Add(new()
            {
                Id = id,
                InternalName = "large",
                Order = 3,
                Size = "L",
                MasterId = (int)SizeGroup.General
            });

            foreach (var seed in sizes)
            {
                modelBuilder.Entity<ProductSize>().HasData(seed);
            }
        }
    }
}
