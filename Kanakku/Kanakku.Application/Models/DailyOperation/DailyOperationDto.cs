﻿namespace Kanakku.Application.Models.DailyOperation
{
    public class DailyOperationDto
    {
        public int Id { get; set; }
        public DateTime? WorkedOn { get; set; } = DateTime.Now;
        public TimeSpan? WorkedTime { get; set; } = DateTime.Now.TimeOfDay;
        public Guid? WorkedBy { get; set; }
        public int? ProductId { get; set; }
        public int? OperationId { get; set; }
        public List<OperaitonVariantDto> VariantsPerOperation { get; set; } = new();
    }

    public class OperaitonVariantDto
    {
        public int OperationInstanceId { get; set; }
        public int Quantity { get; set; }
        public bool IsChecked { get; set; }
    }
}
