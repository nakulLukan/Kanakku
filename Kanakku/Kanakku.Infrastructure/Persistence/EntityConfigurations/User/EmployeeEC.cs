using Kanakku.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kanakku.Infrastructure.Persistence.EntityConfigurations.User
{
    public class EmployeeEC : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Email).HasMaxLength(255);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Pincode).IsRequired().HasMaxLength(10);
            builder.Property(x => x.StateId).IsRequired();
            builder.Property(x => x.DistrictId).IsRequired();
            builder.Property(x => x.AddressLineOne).IsRequired().HasMaxLength(500);

            builder.HasOne(x => x.Image)
                .WithMany()
                .HasForeignKey(x => x.ImageId);
            builder.HasOne(x => x.State)
                .WithMany()
                .HasForeignKey(x => x.StateId);
            builder.HasOne(x => x.District)
                .WithMany()
                .HasForeignKey(x => x.DistrictId);
            builder.HasMany(x => x.WorkHistories)
                .WithOne(x => x.Employee)
                .HasForeignKey(x => x.EmployeeId);
        }
    }
}
