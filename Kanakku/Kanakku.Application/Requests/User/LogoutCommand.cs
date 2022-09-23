using Kanakku.Application.Contracts.Storage;
using Kanakku.Shared;
using MediatR;

namespace Kanakku.Application.Requests.User;

public class LogoutCommand : IRequest<bool>
{
}
public class LogoutCommandHandler : IRequestHandler<LogoutCommand, bool>
{
    readonly IAppSecureStorage secureStorage;

    public LogoutCommandHandler(IAppSecureStorage secureStorage)
    {
        this.secureStorage = secureStorage;
    }

    public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        await secureStorage.SetAsync(SecureStorageKey.IS_LOGGED, false);
        await secureStorage.SetAsync(SecureStorageKey.USER_ID, Guid.Empty);
        AppInMemoryStore.IsLoggedIn = false;
        AppInMemoryStore.UserId = Guid.Empty;
        return true;
    }
}


