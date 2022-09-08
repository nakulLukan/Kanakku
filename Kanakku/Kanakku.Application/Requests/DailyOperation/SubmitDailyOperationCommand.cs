using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.DailyOperation;
using Kanakku.Domain.Inventory;
using Kanakku.Shared.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Application.Requests.DailyOperation
{
    public class SubmitDailyOperationCommand : DailyOperationDto, IRequest<int>
    {
    }

    public class SubmitDailyOperationCommandHandler : IRequestHandler<SubmitDailyOperationCommand, int>
    {
        readonly IAppDbContext appDbContext;
        readonly ISessionContext sessionContext;

        public SubmitDailyOperationCommandHandler(IAppDbContext appDbContext, ISessionContext sessionContext)
        {
            this.appDbContext = appDbContext;
            this.sessionContext = sessionContext;
        }

        public async Task<int> Handle(SubmitDailyOperationCommand request, CancellationToken cancellationToken)
        {
            var product = await appDbContext.Products.AsTracking()
                .Include(x => x.ProductInstances)
                    .ThenInclude(x => x.ProductSize)
                .FirstAsync(x => x.Id == request.ProductId, cancellationToken);
            List<WorkHistory> works = new List<WorkHistory>();
            var userId = await sessionContext.GetUserId();
            if (request.VariantsPerOperation.Where(x => x.IsChecked).All(x => x.Quantity == 0))
            {
                throw new AppException($"Add quantity");
            }
            foreach (var variant in request.VariantsPerOperation.Where(x => x.IsChecked).ToArray())
            {
                var prodVar = product.ProductInstances.First(x => x.Id == variant.OperationInstanceId);
                if (prodVar.NetQuantity < variant.Quantity)
                {
                    throw new AppException($"Quantity for size '{prodVar.ProductSize.Size}' cannot be greater than {prodVar.NetQuantity}.");
                }

                works.Add(new()
                {
                    EmployeeId = request.WorkedBy.Value,
                    WorkId = request.OperationId.Value,
                    VariantId = variant.OperationInstanceId,
                    WorkedOn = request.WorkedOn.Value.Add(request.WorkedTime.Value).ToUniversalTime(),
                    ModifiedOn = DateTime.UtcNow,
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = userId,
                    ModifiedBy = userId,
                    Quantity = variant.Quantity
                });
                prodVar.NetQuantity -= variant.Quantity;
            }

            await appDbContext.WorkHistories.AddRangeAsync(works);
            await appDbContext.SaveAsync(cancellationToken);
            return 1;
        }
    }
}
