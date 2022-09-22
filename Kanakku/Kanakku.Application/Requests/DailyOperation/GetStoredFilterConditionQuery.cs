using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.DailyOperation;
using Kanakku.Shared;
using MediatR;

namespace Kanakku.Application.Requests.DailyOperation;

public class GetStoredFilterConditionQuery : IRequest<DailyOperationFilterDto>
{
}

public class GetStoredFilterConditionQueryHandler : IRequestHandler<GetStoredFilterConditionQuery, DailyOperationFilterDto>
{
    readonly ISessionContext sessionContext;
    readonly IAppSecureStorage appSecureStorage;
    public GetStoredFilterConditionQueryHandler(ISessionContext sessionContext, IAppSecureStorage appSecureStorage)
    {
        this.sessionContext = sessionContext;
        this.appSecureStorage = appSecureStorage;
    }

    public async Task<DailyOperationFilterDto> Handle(GetStoredFilterConditionQuery request, CancellationToken cancellationToken)
    {
        var userId = await sessionContext.GetUserId();
        string filterKey = string.Format(SecureStorageKey.USER_DAILY_OPERATION_FILTER, userId);
        return (await appSecureStorage.GetAsync<DailyOperationFilterDto>(filterKey)) ?? new();
    }
}

