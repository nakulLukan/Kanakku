namespace Kanakku.Domain.Inventory;

public class ProductWorkInstance
{
    public int Id { get; set; }
    public int ProductInstanceId { get; set; }
    public int WorkId { get; set; }
    public int NetQuantity { get; set; }

    public ProductInstance ProductInstance { get; set; }
    public Work Work { get; set; }
}
