namespace Kanakku.Application.Models.DailyOperation
{
    public class OperationItemDto
    {
        public int Id { get; set; }
        public DateTime WorkedOn { get; set; }
        public string WorkedBy { get; set; }
        public string Product { get; set; }
        public string Operation { get; set; }
        public string Variant { get; set; }
        public int VariantQty { get; set; }
        public float TotalAmount { get; set; }
    }
}
