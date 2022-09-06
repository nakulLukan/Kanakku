using Kanakku.Domain.Attachment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kanakku.Infrastructure.Persistence.EntityConfigurations.Attachment
{
    public class BinaryResourceEC : IEntityTypeConfiguration<BinaryResource>
    {
        public void Configure(EntityTypeBuilder<BinaryResource> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.FileName).HasMaxLength(255).IsRequired(false);
            builder.Property(x => x.Extension).HasMaxLength(50).IsRequired(false);
            builder.Property(x => x.FileFullName).HasMaxLength(255).IsRequired(false);
        }
    }
}
