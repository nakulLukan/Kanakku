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
            builder.Property(x => x.Email).IsRequired(false).HasMaxLength(255);
            builder.Property(x => x.Code).IsRequired(true);
            builder.Property(x => x.DateOfBirth).IsRequired(true);
            builder.Property(x => x.DateOfJoining).IsRequired(true);
            builder.Property(x => x.ResignedOn).IsRequired(false);
            builder.Property(x => x.PhoneNumber1).IsRequired(true);
            builder.Property(x => x.PhoneNumber2).IsRequired(false);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Pincode).IsRequired(false).HasMaxLength(10);
            builder.Property(x => x.StateId).IsRequired(false);
            builder.Property(x => x.DistrictId).IsRequired(false);
            builder.Property(x => x.AddressLineOne).IsRequired().HasMaxLength(500);
            builder.Property(x => x.EpfRegNo).IsRequired(false); ;
            builder.Property(x => x.EsiRegNo).IsRequired(false);
            builder.Property(x => x.DpImageId).IsRequired(false);
            builder.Property(x => x.IdProofImageId).IsRequired(false);

            builder.HasOne(x => x.DisplayPicture)
                .WithMany()
                .HasForeignKey(x => x.DpImageId);
            builder.HasOne(x => x.IdProof)
                .WithMany()
                .HasForeignKey(x => x.IdProofImageId);
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
