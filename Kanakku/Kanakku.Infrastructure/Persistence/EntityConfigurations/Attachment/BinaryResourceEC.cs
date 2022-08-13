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
        }
    }
}
