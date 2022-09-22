using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.DailyOperation;
using Kanakku.Shared;
using MediatR;

namespace Kanakku.Application.Requests.DailyOperation;

public class SaveFilterConditionCommand : DailyOperationFilterDto, IRequest<bool>
{
}

public class SaveFilterConditionCommandHandler : IRequestHandler<SaveFilterConditionCommand, bool>
{
    readonly ISessionContext sessionContext;
    readonly IAppSecureStorage appSecureStorage;
    public SaveFilterConditionCommandHandler(ISessionContext sessionContext, IAppSecureStorage appSecureStorage)
    {
        this.sessionContext = sessionContext;
        this.appSecureStorage = appSecureStorage;
    }

    public async Task<bool> Handle(SaveFilterConditionCommand request, CancellationToken cancellationToken)
    {
        var userId = await sessionContext.GetUserId();
        string filterKey = string.Format(SecureStorageKey.USER_DAILY_OPERATION_FILTER, userId);
        await appSecureStorage.SetAsync(filterKey, (DailyOperationFilterDto)request);
        return true;
    }
}
