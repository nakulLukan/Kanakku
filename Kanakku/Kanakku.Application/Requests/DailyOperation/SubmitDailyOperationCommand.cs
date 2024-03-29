﻿using Kanakku.Application.Contracts.Storage;
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
                    .ThenInclude(x => x.ProductWorkInstances)
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
                var workInstance = product.ProductInstances.SelectMany(x=>x.ProductWorkInstances)
                    .First(x => x.WorkId == request.OperationId.Value 
                        && x.Id == variant.OperationInstanceId);
                if (workInstance.NetQuantity < variant.Quantity)
                {
                    throw new AppException($"Quantity for size '{workInstance.ProductInstance.ProductSize.Size}' cannot be greater than {workInstance.NetQuantity}.");
                }

                works.Add(new()
                {
                    EmployeeId = request.WorkedBy.Value,
                    WorkId = request.OperationId.Value,
                    VariantId = workInstance.ProductInstanceId,
                    WorkedOn = request.WorkedOn.Value.Date.Add(request.WorkedTime.Value).ToUniversalTime(),
                    ModifiedOn = DateTime.UtcNow,
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = userId,
                    ModifiedBy = userId,
                    Quantity = variant.Quantity,
                });
                workInstance.NetQuantity -= variant.Quantity;
            }

            await appDbContext.WorkHistories.AddRangeAsync(works);
            await appDbContext.SaveAsync(cancellationToken);
            return 1;
        }
    }
}
