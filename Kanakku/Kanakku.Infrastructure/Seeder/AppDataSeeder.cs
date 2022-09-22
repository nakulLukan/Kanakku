using Microsoft.EntityFrameworkCore;

namespace Kanakku.Infrastructure.Seeder;

public static class AppDataSeeder
{
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        LookupMasterSeeder.SeedData(modelBuilder);
        LookupDetailSeeder.SeedData(modelBuilder);
        ProductSizeMasterSeeder.SeedData(modelBuilder);
        ProductSizeSeeder.SeedData(modelBuilder);
    }
}
