using FluentValidation;
using Kanakku.Application.Contracts.Storage;
using Kanakku.Shared.Models;
using Kanakku.Shared.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Application.Requests.Product;

public class UpdateProductNameCommand : IRequest<ResponseDto<bool>>
{
    public int ProductId { get; set; }
    public string NewName { get; set; }
    public string NewShortCode { get; set; }
}

public class UpdateProductNameCommandHandler : IRequestHandler<UpdateProductNameCommand, ResponseDto<bool>>
{
    readonly IAppDbContext _dbContext;

    public UpdateProductNameCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResponseDto<bool>> Handle(UpdateProductNameCommand request, CancellationToken cancellationToken)
    {
        var prod = await _dbContext.Products.AsTracking()
            .FirstAsync(x => x.Id == request.ProductId, cancellationToken);

        if (!string.IsNullOrEmpty(request.NewName))
        {
            prod.Name = request.NewName;
        }

        if (!string.IsNullOrEmpty(request.NewShortCode))
        {
            prod.ShortCode = request.NewShortCode;
        }

        await _dbContext.SaveAsync(cancellationToken);
        return new ResponseDto<bool>(true);
    }
}

public class UpdateProductNameCommandValidator : AppAbstractValidator<UpdateProductNameCommand>
{
    public UpdateProductNameCommandValidator()
    {
        RuleFor(x => x.NewName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.NewShortCode)
            .NotEmpty()
            .MaximumLength(100);
    }
}
