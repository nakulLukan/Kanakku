using Kanakku.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kanakku.Infrastructure.Persistence.EntityConfigurations.User
{
    public class EmployeeSalaryHistoryEC : IEntityTypeConfiguration<EmployeeSalaryHistory>
    {
        public void Configure(EntityTypeBuilder<EmployeeSalaryHistory> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CreatedBy).IsRequired(false);
            builder.Property(x => x.CreatedOn).IsRequired(false);
            builder.Property(x => x.ModifiedBy).IsRequired(false);
            builder.Property(x => x.ModifiedOn).IsRequired(false);
            builder.Property(x => x.Bonus).IsRequired(false).HasDefaultValue(null);

            builder.HasOne(x => x.Employee)
                .WithMany()
                .HasForeignKey(x => x.EmpId);
        }
    }
}
