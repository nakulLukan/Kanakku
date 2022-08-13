namespace Kanakku.Domain.Inventory;

public class WorkCostHistory : DomainBase
{
    public int Id { get; set; }
    public int WorkId { get; set; }
    public float Cost { get; set; }
    public bool IsInUse { get; set; }

    public Work Work { get; set; }
}
