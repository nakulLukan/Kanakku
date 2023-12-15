using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.User;
using Kanakku.Domain.User;
using Kanakku.Shared.Utilities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Application.Requests.User;

public class LoginQuery : LoginDto, IRequest<AppUserMinDto>
{
}

public class LoginQueryHandler : IRequestHandler<LoginQuery, AppUserMinDto>
{
    private readonly UserManager<AppUser> _userManager;

    public LoginQueryHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<AppUserMinDto> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var userDetails = await _userManager.FindByNameAsync(request.Username);

        if (userDetails is null || !userDetails.IsActive)
        {
            throw new AppException("Invalid username or password!!!");
        }

        return new AppUserMinDto()
        {
            Id = userDetails.Id,
            Username = userDetails.UserName
        };
    }
}
