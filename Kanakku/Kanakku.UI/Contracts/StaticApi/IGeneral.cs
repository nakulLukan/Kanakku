using Kanakku.Application.Models.StaticApi;
using Refit;

namespace Kanakku.UI.Contracts.StaticApi;

internal interface IGeneral
{
    [Get("/api/auth.json")]
    public Task<AuthDto> GetAuthenticationInfo();
}
