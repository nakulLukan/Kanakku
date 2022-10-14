namespace Kanakku.Application.Models.Product
{
    public class ProductDetailExportDto
    {
        public string Name { get; set; }
        public string ShortCode { get; set; }
        public List<WorkDto> Works { get; set; }
        public int? ImageId { get; set; }
    }

    public class WorkExportDto
    {
        public string WorkName { get; set; }
        public string Rate { get; set; }
    }
}
