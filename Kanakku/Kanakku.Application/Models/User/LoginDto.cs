using FluentValidation;
using Kanakku.Shared.Utilities;

namespace Kanakku.Application.Models.User;

public class LoginDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class LoginDtoValidator : AppAbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}
