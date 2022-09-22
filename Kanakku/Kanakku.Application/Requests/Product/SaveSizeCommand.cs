using FluentValidation;
using Kanakku.Application.Contracts.Storage;
using Kanakku.Shared.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Application.Requests.Product;

public class SaveSizeCommand : IRequest<int>
{
    public string SizeName { get; set; }
    public int MasterId { get; set; }
}

public class SaveSizeCommandHandler : IRequestHandler<SaveSizeCommand, int>
{
    readonly IAppDbContext _appDbContext;

    public SaveSizeCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<int> Handle(SaveSizeCommand request, CancellationToken cancellationToken)
    {
        request.SizeName = request.SizeName.Trim();
        var isDuplicate = await _appDbContext.ProductSizes.AnyAsync(x => x.Size == request.SizeName, cancellationToken);
        if (isDuplicate)
        {
            throw new AppException($"Size with name '{request.SizeName}' already exists.");
        }

        var lastSizeInGroup = await _appDbContext.ProductSizes
            .Where(x => x.MasterId == request.MasterId)
            .OrderByDescending(x => x.Order)
            .FirstOrDefaultAsync(cancellationToken);

        int newOrder = 1;
        if (lastSizeInGroup != null)
        {
            newOrder = lastSizeInGroup.Order + 1;
        }

        var newSize = new Domain.Inventory.ProductSize
        {
            InternalName = "dynamic",
            MasterId = request.MasterId,
            Order = newOrder,
            Size = request.SizeName
        };

        _appDbContext.ProductSizes.Add(newSize);

        await _appDbContext.SaveAsync(cancellationToken);
        return newSize.Id;
    }
}

public class SaveSizeCommandValidator : AppAbstractValidator<SaveSizeCommand>
{
    public SaveSizeCommandValidator()
    {
        RuleFor(x => x.SizeName)
            .NotEmpty()
            .MaximumLength(20);
    }
}
