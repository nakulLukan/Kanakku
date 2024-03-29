﻿using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.DailyOperation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Application.Requests.Product
{
    public class ProductMappingQuery : IRequest<ProductMappingDto[]>
    {
    }

    public class ProductMappingQueryHandler : IRequestHandler<ProductMappingQuery, ProductMappingDto[]>
    {
        readonly IAppDbContext appDbContext;

        public ProductMappingQueryHandler(IAppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<ProductMappingDto[]> Handle(ProductMappingQuery request, CancellationToken cancellationToken)
        {
            return await appDbContext.Products.Where(x => x.IsActive)
                .OrderBy(x => x.ShortCode)
                .Select(x => new ProductMappingDto
                {
                    ProductId = x.Id,
                    ProductName = $"{x.ShortCode} - {x.Name}",
                    Operations = x.Works.OrderBy(x => x.Name).Select(y => new OperationMappingDto
                    {
                        OperationId = y.Id,
                        OperationName = y.Name
                    }).ToArray()
                }).ToArrayAsync(cancellationToken);
        }
    }
}
