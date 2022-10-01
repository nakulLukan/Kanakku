using FluentValidation;
using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.Product;
using Kanakku.Domain.Inventory;
using Kanakku.Shared.Models;
using Kanakku.Shared.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Application.Requests.Product;

public class AddProductCommand : ProductDetailDto, IRequest<ResponseDto<int>>
{
}

public class AddProductCommandHandler : IRequestHandler<AddProductCommand, ResponseDto<int>>
{
    private readonly IAppDbContext _dbContext;

    public AddProductCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResponseDto<int>> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        request.ShortCode = request.ShortCode.Trim();

        var shortCodeExists = await _dbContext.Products.AnyAsync(x => x.ShortCode.ToLower() == request.ShortCode.ToLower(), cancellationToken);
        if (shortCodeExists)
        {
            throw new AppException("Another product with same 'model number' exists.");
        }

        Domain.Inventory.Product product = new()
        {
            ImageId = request.ImageId,
            IsActive = true,
            ModifiedOn = DateTime.UtcNow,
            CreatedOn = DateTime.UtcNow,
            Name = request.Name,
            ShortCode = request.ShortCode,
        };
        product.ProductInstances = request.ProductVariants.Select(x => new ProductInstance
        {
            Quantity = x.Quantity,
            Product = product,
            ProductSizeId = x.SizeId
        }).ToList();

        _dbContext.Products.Add(product);
        await _dbContext.SaveAsync(cancellationToken);
        return new ResponseDto<int>(product.Id);
    }
}

public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    public AddProductCommandValidator()
    {
        RuleFor(x => x.ShortCode).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.ProductVariants).NotNull().NotEmpty();

        RuleForEach(x => x.ProductVariants)
            .Must(variant => variant.SizeId > 0 && variant.Quantity > 0);
    }
}
