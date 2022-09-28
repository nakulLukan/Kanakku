namespace Kanakku.Application.Models.Product;

public class ProductListDto
{
    public int Id { get; set; }
    public int RowNumber { get; set; }
    public string Name { get; set; }
    public string ShortCode { get; set; }
    public int TotalQuantity { get; set; }
}
