namespace Kanakku.Application.Models.Product
{
    public class ProductDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortCode { get; set; }
        public List<ProductInstanceDto> ProductVariants { get; set; }
        public int? ImageId { get; set; }
    }
}
