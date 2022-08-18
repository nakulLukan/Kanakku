namespace Kanakku.Domain.Inventory;

public class ProductInstance
{
    public int Id { get; set; }
    public int ProductSizeId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public int NetQuantity { get; set; }

    public Product Product { get; set; }
    public ProductSize ProductSize { get; set; }
}
