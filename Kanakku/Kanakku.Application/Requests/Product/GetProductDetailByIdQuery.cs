using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.Product;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Application.Requests.Product
{
    public class GetProductDetailByIdQuery : IRequest<ProductDetailDto>
    {
        public int Id { get; set; }
    }
    public class GetProductDetailByIdQueryHandler : IRequestHandler<GetProductDetailByIdQuery, ProductDetailDto>
    {
        private readonly IAppDbContext _dbContext;

        public GetProductDetailByIdQueryHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProductDetailDto> Handle(GetProductDetailByIdQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Products.Select(x => new ProductDetailDto
            {
                Id = x.Id,
                ImageId = x.ImageId,
                Name = x.Name,
                ShortCode = x.ShortCode,
                ProductVariants = x.ProductInstances.Select(y => new ProductInstanceDto
                {
                    Id = y.Id,
                    Quantity = y.Quantity,
                    SizeId = y.ProductSizeId,
                }).ToList()
            }).SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}
