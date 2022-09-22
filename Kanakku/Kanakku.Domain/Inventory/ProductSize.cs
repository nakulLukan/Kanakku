namespace Kanakku.Domain.Inventory;

public class ProductSize
{
    public int Id { get; set; }
    public string InternalName { get; set; }
    public int Order { get; set; }
    public string Size { get; set; }
    public int? MasterId { get; set; }
    public ProductSizeMaster Master { get; set; }
}
