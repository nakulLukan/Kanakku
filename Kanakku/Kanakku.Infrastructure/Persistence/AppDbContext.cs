using Kanakku.Application.Contracts.Storage;
using Kanakku.Domain.Attachment;
using Kanakku.Domain.Inventory;
using Kanakku.Domain.Lookup;
using Kanakku.Domain.User;
using Kanakku.Infrastructure.Seeder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Kanakku.Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    private readonly IConfiguration configuration;

    public AppDbContext()
    {
    }

    public AppDbContext(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public DbSet<BinaryResource> BinaryResources { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Work> Works { get; set; }
    public DbSet<WorkCostHistory> WorkCostHistories { get; set; }
    public DbSet<WorkHistory> WorkHistories { get; set; }
    public DbSet<LookupDetail> LookupDetails { get; set; }
    public DbSet<LookupMaster> LookupMasters { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<ProductSize> ProductSizes { get; set; }
    public DbSet<ProductInstance> ProductInstances { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = configuration is not null ? configuration["ConnectionStrings:DbConnection"]
            : "Host=localhost;Username=nakul;Password=password;Database=kanakku";

        optionsBuilder.UseNpgsql(connectionString)
            .UseSnakeCaseNamingConvention();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly(), t => t.GetInterfaces().Any(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)
            )
        );

        modelBuilder.SeedData();
    }

    public int Save()
    {
        return base.SaveChanges();
    }

    public async Task<int> SaveAsync(CancellationToken cancellationToken)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
