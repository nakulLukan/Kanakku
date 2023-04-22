using FluentValidation;
using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Requests.Product;
using Kanakku.Shared.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Application.Models.Product;

public class UpdateProductQuantityCommand : IRequest<bool>
{
    public int ProductId { get; set; }
    public List<ProductInstanceDetailDto> ProductQuantity { get; set; }
    public bool AddToExistingQuantity { get; set; }
}

public class UpdateProductQuantityCommandHandler : IRequestHandler<UpdateProductQuantityCommand, bool>
{
    readonly IAppDbContext _dbContext;

    public UpdateProductQuantityCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(UpdateProductQuantityCommand request, CancellationToken cancellationToken)
    {
        var productSizes = request.ProductQuantity.Select(x => x.SizeId);
        var currentProductVariants = await _dbContext.ProductInstances.AsTracking()
            .Include(x => x.ProductWorkInstances)
            .Where(x => x.ProductId == request.ProductId
                && productSizes.Contains(x.ProductSizeId))
            .ToListAsync(cancellationToken);

        foreach (var productInstance in currentProductVariants)
        {
            var quantity = request.ProductQuantity.First(x => x.SizeId == productInstance.ProductSizeId).Quantity;
            var newQuantity = request.AddToExistingQuantity ? productInstance.Quantity + quantity
                : quantity;
            productInstance.Quantity = newQuantity;
            productInstance.ProductWorkInstances.ForEach(x => x.NetQuantity = newQuantity);
        }

        var existingProductSizes = currentProductVariants.Select(x => x.ProductSizeId).ToArray();
        var newProductInstances = request.ProductQuantity.Where(x => !existingProductSizes.Contains(x.SizeId)).ToArray();

        if (newProductInstances.Any())
        {
            var worksUnderProduct = await _dbContext.Works
                .Where(x => x.ProductId == request.ProductId)
                .Select(x => x.Id)
                .ToArrayAsync(cancellationToken);
            await _dbContext.ProductInstances.AddRangeAsync(newProductInstances.Select(x => new Domain.Inventory.ProductInstance
            {
                ProductId = request.ProductId,
                ProductSizeId = x.SizeId,
                Quantity = x.Quantity,
                ProductWorkInstances = worksUnderProduct.Select(workId => new Domain.Inventory.ProductWorkInstance
                {
                    NetQuantity = x.Quantity,
                    WorkId = workId,
                }).ToList()
            }));
        }

        await _dbContext.SaveAsync(cancellationToken);
        return true;
    }
}

public class UpdateProductQuantityCommandValidator : AppAbstractValidator<UpdateProductQuantityCommand>
{
    public UpdateProductQuantityCommandValidator()
    {
        RuleForEach(x => x.ProductQuantity)
            .ChildRules(x => x.RuleFor(y => y.Quantity).GreaterThanOrEqualTo(0));
    }
}