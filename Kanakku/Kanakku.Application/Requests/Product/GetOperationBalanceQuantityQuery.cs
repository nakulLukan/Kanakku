using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.DailyOperation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Application.Requests.Product;

public class GetOperationBalanceQuantityQuery : IRequest<List<OperaitonVariantDto>>
{
    public int OperationId { get; set; }
}
public class GetOperationBalanceQuantityQueryHandler : IRequestHandler<GetOperationBalanceQuantityQuery, List<OperaitonVariantDto>>
{
    readonly IAppDbContext _dbContext;
    public GetOperationBalanceQuantityQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<OperaitonVariantDto>> Handle(GetOperationBalanceQuantityQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.ProductWorkInstances
            .Where(x => x.WorkId == request.OperationId)
            .OrderBy(x => x.ProductInstance.ProductSize.Order)
            .Select(x => new OperaitonVariantDto
            {
                NetQuantity = x.NetQuantity,
                OperationInstanceId = x.Id,
                SizeName = x.ProductInstance.ProductSize.Size,
                SizeId = x.ProductInstance.ProductSizeId,
                ProductInstanceId = x.ProductInstanceId,
            }).ToListAsync(cancellationToken);
    }
}
