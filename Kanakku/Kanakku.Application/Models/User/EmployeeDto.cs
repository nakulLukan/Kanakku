using FluentValidation;
using Kanakku.Shared;
using Kanakku.Shared.Utilities;

namespace Kanakku.Application.Models.User;

public class EmployeeDto
{
    public Guid Id { get; set; }
    public int RowNumber { get; set; }
    public string Name { get; set; }
    public int EmpCode { get; set; }
    public string Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime? DateOfJoining { get; set; }
    public DateTime? RegsignedOn { get; set; }
    public string PhoneNumber1 { get; set; }
    public string PhoneNumber2 { get; set; }
    public int? DistrictId { get; set; }
    public string District { get; set; }
    public int? StateId { get; set; }
    public string State { get; set; }
    public string Pincode { get; set; }
    public string AddressLineOne { get; set; }
    public string EpfRegNo { get; set; }
    public string EsiRegNo { get; set; }
    public int? DpImageId { get; set; }
    public int? IdProofImageId { get; set; }
}

public class EmployeeDtoValidator : AppAbstractValidator<EmployeeDto>
{
    public EmployeeDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty()
            .Matches(AppRegex.NAME);
        RuleFor(x => x.EmpCode).NotEmpty()
            .GreaterThan(0)
            .WithMessage("'Employee Code' is mandatory.")
            .When(x => x.Id == Guid.Empty);
        RuleFor(x => x.PhoneNumber1)
            .NotEmpty()
            .Matches(AppRegex.PHONE_NUMBER);
        RuleFor(x => x.PhoneNumber2)
            .Matches(AppRegex.PHONE_NUMBER)
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber2));
        RuleFor(x => x.Email)
            .Matches(AppRegex.EMAIL)
            .When(x => !string.IsNullOrEmpty(x.Email));
        RuleFor(x => x.DateOfBirth)
            .NotEmpty()
            .LessThan(DateTime.UtcNow);
        RuleFor(x => x.DateOfJoining)
            .NotEmpty();
        RuleFor(x => x.Pincode)
            .Matches(AppRegex.PINCODE)
            .When(x => !string.IsNullOrEmpty(x.Pincode));
        RuleFor(x => x.AddressLineOne)
            .NotEmpty()
            .MaximumLength(100);
    }
}