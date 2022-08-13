using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.User;
using Kanakku.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Application.Requests.User;

public class LoginQuery : IRequest<AppUserMinDto>
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class LoginQueryHandler : IRequestHandler<LoginQuery, AppUserMinDto>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IAppSecureStorage _appSecureStorage;

    public LoginQueryHandler(IAppDbContext appDbContext, IAppSecureStorage appSecureStorage)
    {
        _appDbContext = appDbContext;
        _appSecureStorage = appSecureStorage;
    }

    public async Task<AppUserMinDto> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var userDetails = await _appDbContext.AppUsers.SingleOrDefaultAsync(x => x.Username == request.Username
            && x.Password == request.Password
            && x.IsActive, cancellationToken);

        if (userDetails is null)
        {
            throw new Exception("User not found");
        }

        await _appSecureStorage.SetAsync(SecureStorageKey.IS_LOGGED, true);

        return new AppUserMinDto()
        {
            Id = userDetails.Id,
            Username = userDetails.Username
        };
    }
}
