using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.DailyOperation;
using Kanakku.Shared.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Application.Requests.DailyOperation;

public class EditDailyOperationDetailCommand : DailyOperationDetailDto, IRequest<int>
{
}

public class EditDailyOperationDetailHandler : IRequestHandler<EditDailyOperationDetailCommand, int>
{
    readonly IAppDbContext appDbContext;
    readonly ISessionContext sessionContext;

    public EditDailyOperationDetailHandler(IAppDbContext appDbContext, ISessionContext sessionContext)
    {
        this.appDbContext = appDbContext;
        this.sessionContext = sessionContext;
    }

    public async Task<int> Handle(EditDailyOperationDetailCommand request, CancellationToken cancellationToken)
    {
        var operationDetail = await appDbContext.WorkHistories.AsTracking()
            .FirstAsync(x => x.Id == request.Id, cancellationToken);
        var workInstance = await appDbContext.ProductWorkInstances.AsTracking()
            .FirstAsync(x => x.ProductInstanceId == request.VariantId.Value && x.WorkId == request.OperationId, cancellationToken);

        var userId = await sessionContext.GetUserId();
        operationDetail.WorkedOn = request.WorkedOn.Value.Date.Add(request.WorkedTime.Value).ToUniversalTime();
        operationDetail.EmployeeId = request.WorkedBy.Value;

        bool isDifferentVariant = false;
        int oldWorkId = operationDetail.WorkId;
        int oldVariantId = operationDetail.VariantId;
        if (operationDetail.WorkId != request.OperationId.Value)
        {
            operationDetail.WorkId = request.OperationId.Value;
            isDifferentVariant = true;
        }

        if (operationDetail.VariantId != request.VariantId.Value)
        {
            operationDetail.VariantId = request.VariantId.Value;
            isDifferentVariant = true;
        }

        if (isDifferentVariant)
        {
            var oldWorkInstance = await appDbContext.ProductWorkInstances.AsTracking()
            .FirstAsync(x => x.ProductInstanceId == oldVariantId && x.WorkId == oldWorkId, cancellationToken);
            if (workInstance.NetQuantity < request.Quantity)
            {
                throw new AppException($"Quantity cannot be greater than {workInstance.NetQuantity}.");
            }

            oldWorkInstance.NetQuantity += operationDetail.Quantity;
            workInstance.NetQuantity -= request.Quantity.Value;
            operationDetail.Quantity = request.Quantity.Value;
        }
        else if (operationDetail.Quantity != request.Quantity.Value)
        {
            var netQuantity = request.Quantity.Value - operationDetail.Quantity;
            if (workInstance.NetQuantity < netQuantity)
            {
                throw new AppException($"Quantity cannot be greater than {workInstance.NetQuantity}.");
            }
            workInstance.NetQuantity -= netQuantity;
            operationDetail.Quantity = request.Quantity.Value;
        }

        operationDetail.ModifiedBy = userId;
        operationDetail.ModifiedOn = DateTime.UtcNow;
        await appDbContext.SaveAsync(cancellationToken);
        return request.Id;
    }
}
