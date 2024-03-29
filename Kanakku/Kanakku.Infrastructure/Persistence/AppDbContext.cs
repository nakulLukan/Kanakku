﻿using Kanakku.Application.Contracts.Storage;
using Kanakku.Domain.Attachment;
using Kanakku.Domain.Inventory;
using Kanakku.Domain.Lookup;
using Kanakku.Domain.User;
using Kanakku.Infrastructure.Seeder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Kanakku.Infrastructure.Persistence;

public class AppDbContext : IdentityDbContext<AppUser>, IAppDbContext
{
    private readonly IConfiguration configuration;

    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
        : base(options)
    {
        this.configuration = configuration;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public DbSet<BinaryResource> BinaryResources { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Work> Works { get; set; }
    public DbSet<WorkCostHistory> WorkCostHistories { get; set; }
    public DbSet<WorkHistory> WorkHistories { get; set; }
    public DbSet<LookupDetail> LookupDetails { get; set; }
    public DbSet<LookupMaster> LookupMasters { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<ProductSize> ProductSizes { get; set; }
    public DbSet<ProductInstance> ProductInstances { get; set; }
    public DbSet<ProductWorkInstance> ProductWorkInstances { get; set; }
    public DbSet<EmployeeSalaryHistory> EmployeeSalaryHistories { get; set; }
    public DbSet<Designation> Designations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = configuration is not null ? configuration["ConnectionStrings:DbConnection"]
            : "Host=localhost;Username=nakul;Password=password;Database=kanakku";
        optionsBuilder.UseNpgsql(connectionString)
            .UseSnakeCaseNamingConvention()
            .LogTo(Serilog.Log.Logger.Information);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
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

    public void ChangePropertyStateToModified<TEntity>(TEntity entity, string property)
    {
        Entry(entity).Property(property).IsModified = true;
    }

}
