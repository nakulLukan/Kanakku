using Kanakku.Application.Contracts.Storage;
using Kanakku.Shared;

namespace Kanakku.UI.Impl.Storage;

public class SessionContext : ISessionContext
{
    private readonly IAppSecureStorage _storage;
    public SessionContext(IAppSecureStorage storage)
    {
        _storage = storage;
    }

    public async Task<string> GetUserId()
    {
        return (await _storage.GetAsync<Guid>(SecureStorageKey.USER_ID)).ToString();
    }
}
