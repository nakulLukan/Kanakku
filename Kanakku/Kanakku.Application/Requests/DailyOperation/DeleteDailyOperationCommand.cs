using Kanakku.Application.Contracts.Storage;
using Kanakku.Shared.Models;
using Kanakku.Shared.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Kanakku.Application.Requests.DailyOperation;

public class DeleteDailyOperationCommand : IRequest<ResponseDto<bool>>
{
    public int? Id { get; set; }
}

public class DeleteDailyOperationCommandHandler : IRequestHandler<DeleteDailyOperationCommand, ResponseDto<bool>>
{
    readonly IAppDbContext _dbContext;
    readonly ISessionContext _sessionContext;
    readonly ILogger<DeleteDailyOperationCommandHandler> _logger;

    public DeleteDailyOperationCommandHandler(IAppDbContext dbContext,
                                              ISessionContext sessionContext,
                                              ILogger<DeleteDailyOperationCommandHandler> logger)
    {
        _dbContext = dbContext;
        _sessionContext = sessionContext;
        _logger = logger;
    }

    public async Task<ResponseDto<bool>> Handle(DeleteDailyOperationCommand request, CancellationToken cancellationToken)
    {
        var work = await _dbContext.WorkHistories.AsTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (work == null)
        {
            throw new AppException("Could not find the work record. Please refresh your screen and try again.");
        }

        var quantity = work.Quantity;
        var workId = work.WorkId;
        var variantId = work.VariantId;

        var variant = await _dbContext.ProductWorkInstances.AsTracking()
            .FirstAsync(x => x.ProductInstanceId == variantId
                && x.WorkId == workId);

        variant.NetQuantity += quantity;
        Log.Logger.Information("Variant #{variantName} quantity updated to {quantity}", variant.Id, variant.NetQuantity);

        _dbContext.WorkHistories.Remove(work);
        await _dbContext.SaveAsync(cancellationToken);

        Log.Logger.Information("Work record {workId}, {workedBy}, {workedOn}, {workVariant} delete by user {user} on {date}",
                               work.Id,
                               work.EmployeeId,
                               work.WorkedOn.ToLocalTime(),
                               work.VariantId,
                               await _sessionContext.GetUserId(),
                               DateTime.Now);
        return new ResponseDto<bool>(true);
    }
}
