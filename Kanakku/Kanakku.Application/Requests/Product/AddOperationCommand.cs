﻿using FluentValidation;
using Kanakku.Application.Contracts.Storage;
using Kanakku.Domain.Inventory;
using Kanakku.Shared.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Application.Requests.Product;

public class AddOperationCommand : IRequest<int>
{
    public string OperationName { get; set; }
    public float Rate { get; set; }
    public int ProductId { get; set; }
}

public class AddOperationCommandHandler : IRequestHandler<AddOperationCommand, int>
{
    private readonly IAppDbContext _dbContext;
    private readonly ISessionContext _sessionContext;

    public AddOperationCommandHandler(IAppDbContext dbContext, ISessionContext sessionContext)
    {
        _dbContext = dbContext;
        _sessionContext = sessionContext;
    }

    public async Task<int> Handle(AddOperationCommand request, CancellationToken cancellationToken)
    {
        var userId = await _sessionContext.GetUserId();
        var product = await _dbContext.Products
            .Include(x => x.ProductInstances)
            .FirstAsync(x => x.Id == request.ProductId, cancellationToken);
        Work work = new Work
        {
            Cost = request.Rate,
            IsActive = true,
            IsPayPerHour = false,
            Name = request.OperationName,
            ProductId = request.ProductId,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = userId,
            ModifiedBy = userId,
            ModifiedOn = DateTime.UtcNow,
            ProductWorkInstances = product.ProductInstances.Select(x => new ProductWorkInstance
            {
                NetQuantity = x.Quantity,
                ProductInstanceId = x.Id,
            }).ToList()
        };
        if (await _dbContext.Works.AnyAsync(x => x.ProductId == request.ProductId && x.Name.ToLower() == request.OperationName.ToLower(), cancellationToken))
        {
            throw new AppException($"Operation with name '{request.OperationName}' already exists for this product. Please use another name.");
        }
        _dbContext.Works.Add(work);
        await _dbContext.SaveAsync(cancellationToken);
        return work.Id;
    }
}

public class AddOperationCommandValidator : AppAbstractValidator<AddOperationCommand>
{
    public AddOperationCommandValidator()
    {
        RuleFor(x => x.OperationName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Rate)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.ProductId)
            .GreaterThan(0);
    }
}