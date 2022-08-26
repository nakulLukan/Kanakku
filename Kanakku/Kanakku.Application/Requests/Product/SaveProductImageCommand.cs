using FluentValidation;
using Kanakku.Application.Contracts.Storage;
using MediatR;

namespace Kanakku.Application.Requests.Product;

public class SaveProductImageCommand : IRequest<bool>
{
    public int ProductId { get; set; }
    public int ImageId { get; set; }
}

public class SaveProductImageCommandHandler : IRequestHandler<SaveProductImageCommand, bool>
{
    private readonly IAppDbContext dbContext;

    public SaveProductImageCommandHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<bool> Handle(SaveProductImageCommand request, CancellationToken cancellationToken)
    {
        Domain.Inventory.Product product = new()
        {
            Id = request.ProductId,
            ImageId = request.ImageId,
        };

        dbContext.ChangePropertyStateToModified(product, nameof(product.ImageId));
        await dbContext.SaveAsync(cancellationToken);
        return true;
    }
}
public class SaveProductImageCommandValidator : AbstractValidator<SaveProductImageCommand>
{
    public SaveProductImageCommandValidator()
    {
        RuleFor(x => x.ProductId).GreaterThan(0)
            .WithMessage("Cannot find a valid product");
    }
}
