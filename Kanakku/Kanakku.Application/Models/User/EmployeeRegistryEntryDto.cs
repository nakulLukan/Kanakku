using FluentValidation;
using Kanakku.Shared.Utilities;

namespace Kanakku.Application.Models.User;

public class EmployeeRegistryEntryDto
{
    public int Id { get; set; }
    public Guid? EmployeeId { get; set; }
    public DateTime? SalaryPeriod { get; set; } = DateTime.Now;
    public float SalaryPerPeriod { get; set; }
    public float? Bonus { get; set; }
    public int NumberOfDaysPresent { get; set; }
}

public class EmployeeRegistryEntryDtoValidator : AppAbstractValidator<EmployeeRegistryEntryDto>
{
    public EmployeeRegistryEntryDtoValidator()
    {
        RuleFor(x => x.EmployeeId).NotEmpty();
        RuleFor(x => x.SalaryPeriod)
            .NotNull()
            .NotEmpty();
        RuleFor(x => x.NumberOfDaysPresent)
            .GreaterThanOrEqualTo(0);
        RuleFor(x => x.SalaryPerPeriod)
            .NotNull()
            .GreaterThanOrEqualTo(0);
    }
}
