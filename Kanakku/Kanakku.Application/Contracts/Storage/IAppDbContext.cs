using Kanakku.Domain.Attachment;
using Kanakku.Domain.Inventory;
using Kanakku.Domain.Lookup;
using Kanakku.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Application.Contracts.Storage;
public interface IAppDbContext
{
    DbSet<BinaryResource> BinaryResources { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<Work> Works { get; set; }
    DbSet<WorkCostHistory> WorkCostHistories { get; set; }
    DbSet<WorkHistory> WorkHistories { get; set; }
    DbSet<LookupDetail> LookupDetails { get; set; }
    DbSet<LookupMaster> LookupMasters { get; set; }
    DbSet<AppUser> AppUsers { get; set; }
    DbSet<Employee> Employees { get; set; }
    DbSet<ProductSize> ProductSizes { get; set; }
    DbSet<ProductInstance> ProductInstances { get; set; }
    public DbSet<ProductWorkInstance> ProductWorkInstances { get; set; }
    public DbSet<EmployeeSalaryHistory> EmployeeSalaryHistories { get; set; }

    int Save();
    Task<int> SaveAsync(CancellationToken cancellationToken);
    void ChangePropertyStateToModified<TEntity>(TEntity entity, string property);
}
