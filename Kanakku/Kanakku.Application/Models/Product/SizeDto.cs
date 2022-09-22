namespace Kanakku.Application.Models.Product
{
    public class SizeDto
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public string Value { get; set; }
        public int? MasterId { get; set; }
        public string MasterName { get; set; }
    }
}
