using Kanakku.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kanakku.Infrastructure.Persistence.EntityConfigurations.User
{
    public class DesignationEC : IEntityTypeConfiguration<Designation>
    {
        public void Configure(EntityTypeBuilder<Designation> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CreatedBy).IsRequired(false);
            builder.Property(x => x.CreatedOn).IsRequired(false);
            builder.Property(x => x.ModifiedBy).IsRequired(false);
            builder.Property(x => x.ModifiedOn).IsRequired(false);
            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(100);
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
