using Kanakku.Domain.Inventory;
using Kanakku.Shared;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Infrastructure.Seeder
{
    public static class ProductSizeMasterSeeder
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            List<ProductSizeMaster> groups = new List<ProductSizeMaster>();
            groups.Add(new()
            {
                Id = (int)SizeGroup.General,
                Order = 1,
                MasterName = "General"
            });

            foreach (var seed in groups)
            {
                modelBuilder.Entity<ProductSizeMaster>().HasData(seed);
            }
        }
    }
}
