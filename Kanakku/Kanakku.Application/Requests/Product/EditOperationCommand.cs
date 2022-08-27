using FluentValidation;
using Kanakku.Application.Contracts.Storage;
using Kanakku.Domain.Inventory;
using Kanakku.Shared;
using Kanakku.Shared.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Application.Requests.Product;

public class EditOperationCommand : IRequest<int>
{
    public int WordId { get; set; }
    public string OperationName { get; set; }
    public float Rate { get; set; }
}

public class EditOperationCommandHandler : IRequestHandler<EditOperationCommand, int>
{
    private readonly IAppDbContext _dbContext;
    private readonly ISessionContext _sessionContext;

    public EditOperationCommandHandler(IAppDbContext dbContext, ISessionContext sessionContext)
    {
        _dbContext = dbContext;
        _sessionContext = sessionContext;
    }

    public async Task<int> Handle(EditOperationCommand request, CancellationToken cancellationToken)
    {
        var userId = await _sessionContext.GetUserId();
        var work = await _dbContext.Works.AsTracking()
            .SingleAsync(x => x.Id == request.WordId, cancellationToken);

        work.Name = request.OperationName;
        work.ModifiedBy = userId;
        work.ModifiedOn = DateTime.UtcNow;
        if (work.Cost != request.Rate)
        {
            WorkCostHistory costHistory = new()
            {
                Cost = work.Cost,
                CreatedBy = userId,
                ModifiedBy = userId,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow,
                IsInUse = false,
                WorkId = work.Id
            };

            _dbContext.WorkCostHistories.Add(costHistory);
            work.Cost = request.Rate;
        }

        if (await _dbContext.Works
            .AnyAsync(x => work.ProductId == x.ProductId
            && x.Name.ToLower() == request.OperationName.ToLower()
            && x.Id != work.Id, cancellationToken))
        {
            throw new AppException($"Operation with name '{request.OperationName}' already exists for this product. Please use another name.");
        }
        await _dbContext.SaveAsync(cancellationToken);
        return work.Id;
    }
}

public class EditOperationCommandValidator : AbstractValidator<EditOperationCommand>
{
    public EditOperationCommandValidator()
    {
        RuleFor(x => x.OperationName)
            .NotEmpty()
            .Matches(AppRegex.NAME)
            .MaximumLength(50);

        RuleFor(x => x.Rate)
            .NotEmpty()
            .GreaterThan(0);
    }
}