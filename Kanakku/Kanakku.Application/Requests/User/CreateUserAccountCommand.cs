using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.User;
using Kanakku.Domain.User;
using Kanakku.Shared.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Application.Requests.User;

public class CreateUserAccountCommand : SignupDto, IRequest<AppUserMinDto>
{
}

public class CreateUserAccountCommandHandler : IRequestHandler<CreateUserAccountCommand, AppUserMinDto>
{
    private readonly IAppDbContext _appDbContext;

    public CreateUserAccountCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<AppUserMinDto> Handle(CreateUserAccountCommand request, CancellationToken cancellationToken)
    {
        if (request.Password != request.ConfirmPassword)
        {
            throw new AppException("'Password' and 'Confirm Password' should exactly match.");
        }

        var isUsernameExists = await _appDbContext.AppUsers.AnyAsync(x => x.Username == request.Username, cancellationToken);
        if (isUsernameExists)
        {
            throw new AppException("Another user already exists with given 'Username'");
        }

        AppUser newUser = new AppUser
        {
            Username = request.Username,
            Password = request.Password,
            IsActive = true,
            Name = String.Empty,
            Email = String.Empty,
        };

        _appDbContext.AppUsers.Add(newUser);
        await _appDbContext.SaveAsync(cancellationToken);

        return new AppUserMinDto
        {
            Id = newUser.Id,
            Username = newUser.Name
        };
    }
}
