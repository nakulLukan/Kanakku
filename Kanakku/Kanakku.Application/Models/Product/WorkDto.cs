using FluentValidation;
using Kanakku.Shared;
using Kanakku.Shared.Utilities;

namespace Kanakku.Application.Models.Product
{
    public class WorkDto
    {
        public int Id { get; set; }
        public int RowNumber { get; set; }
        public string WorkName { get; set; }
        public float Rate { get; set; }
        public int ProductId { get; set; }
    }

    public class WorkDtoValidator : AppAbstractValidator<WorkDto>
    {
        public WorkDtoValidator()
        {
            RuleFor(x => x.WorkName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Rate)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
