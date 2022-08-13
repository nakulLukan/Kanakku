using Kanakku.Domain.Attachment;

namespace Kanakku.Domain.Inventory;

public class Work : DomainBase
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public int ProductId { get; set; }
    public float Cost { get; set; }
    public bool IsPayPerHour { get; set; }
    public int? ImageId { get; set; }

    public Product Product { get; set; }
    public BinaryResource Image { get; set; }
    public List<WorkCostHistory> WorkCostHistories { get; set; }
    public List<WorkHistory> WorkHistories { get; set; }
}
