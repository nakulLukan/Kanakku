using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.Product;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Application.Requests.Product
{
    public class GetProductInstancesByProductIdQuery : IRequest<ProductInstanceDetailDto[]>
    {
        public int ProductId { get; set; }
    }
    public class GetProductInstancesByProductIdQueryHandler : IRequestHandler<GetProductInstancesByProductIdQuery, ProductInstanceDetailDto[]>
    {
        readonly IAppDbContext dbContext;

        public GetProductInstancesByProductIdQueryHandler(IAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ProductInstanceDetailDto[]> Handle(GetProductInstancesByProductIdQuery request, CancellationToken cancellationToken)
        {
            var result = await dbContext.ProductInstances
                .Where(x => x.ProductId == request.ProductId)
                .OrderBy(x => x.ProductSize.Order)
                .Select(x => new ProductInstanceDetailDto
                {
                    Id = x.Id,
                    Quantity = x.Quantity,
                    Size = x.ProductSize.Size,
                    SizeId = x.ProductSizeId
                })
                .ToListAsync(cancellationToken);
            int rowNum = 1;
            result.ForEach(x => x.RowNumber = rowNum++);
            return result.ToArray();
        }
    }
}
