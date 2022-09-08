namespace Kanakku.Application.Models.DailyOperation
{
    public class ProductMappingDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public VariantMappingDto[] Variants { get; set; }
        public OperationMappingDto[] Operations { get; set; }
    }

    public class OperationMappingDto
    {
        public int OperationId { get; set; }
        public string OperationName { get; set; }
    }

    public class VariantMappingDto
    {
        public int VariantId { get; set; }
        public string SizeName { get; set; }
    }
}
