using FluentValidation;
using Kanakku.Shared.Utilities;

namespace Kanakku.Application.Models.DailyOperation;

public class DailyOperationDetailDto
{
    public int Id { get; set; }
    public DateTime? WorkedOn { get; set; }
    public TimeSpan? WorkedTime { get; set; }
    public Guid? WorkedBy { get; set; }
    public int? ProductId { get; set; }
    public int? OperationId { get; set; }
    public int? VariantId { get; set; }
    public int? Quantity { get; set; }
}

public class DailyOperationDetailDtoValidator : AppAbstractValidator<DailyOperationDetailDto>
{
    public DailyOperationDetailDtoValidator()
    {
        RuleFor(x => x.WorkedBy).NotNull().NotEmpty();
        RuleFor(x => x.ProductId).NotNull().NotEmpty();
        RuleFor(x => x.OperationId).NotNull().NotEmpty();
        RuleFor(x => x.VariantId).NotNull().NotEmpty();
        RuleFor(x => x.Quantity).NotNull().NotEmpty();
        RuleFor(x => x.WorkedOn).NotNull().NotEmpty();
        RuleFor(x => x.WorkedTime).NotNull().NotEmpty();
    }
}

