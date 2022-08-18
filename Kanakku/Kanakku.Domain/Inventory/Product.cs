using Kanakku.Domain.Attachment;

namespace Kanakku.Domain.Inventory;

public class Product : DomainBase
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ShortCode { get; set; }
    public bool IsActive { get; set; }
    public int? ImageId { get; set; }

    public List<Work> Works { get; set; }
    public BinaryResource Image { get; set; }
    public List<ProductInstance> ProductInstances { get; set; }
}
