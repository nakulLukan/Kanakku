using FluentValidation;
using Kanakku.Shared.Utilities;

namespace Kanakku.Application.Models.User;

public class SignupDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
public class SignupDtoValidator : AppAbstractValidator<SignupDto>
{
    public SignupDtoValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.ConfirmPassword).NotEmpty()
            .Equal(x => x.Password)
            .WithMessage("'Confirm Password' should exactly match 'Password'");
    }
}
