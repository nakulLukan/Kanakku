using FluentValidation;
using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.Product;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Application.Requests.Product;

public class GetAllProductsQuery : IRequest<List<ProductListDto>>
{
}

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductListDto>>
{
    private readonly IAppDbContext _dbContext;

    public GetAllProductsQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ProductListDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var result = await _dbContext.Products.Where(x => x.IsActive)
            .Select(x => new ProductListDto
            {
                Id = x.Id,
                Name = x.Name,
                ShortCode = x.ShortCode,
                TotalQuantity = x.ProductInstances.Select(x => x.Quantity).Sum()
            })
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
        int serialNum = 1;
        result.ForEach(x => x.RowNumber = serialNum++);
        return result;
    }
}
