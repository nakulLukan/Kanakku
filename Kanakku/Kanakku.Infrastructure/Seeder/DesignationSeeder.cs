using Kanakku.Domain.Inventory;
using Kanakku.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Infrastructure.Seeder
{
    public static class DesignationSeeder
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            List<Designation> groups = new List<Designation>();
            groups.Add(new()
            {
                Id = 1,
                Name = "Tailor"
            });

            foreach (var seed in groups)
            {
                modelBuilder.Entity<Designation>().HasData(seed);
            }
        }
    }
}
