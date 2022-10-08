using FluentValidation;
using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.User;
using Kanakku.Domain.User;
using Kanakku.Shared.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Application.Requests.User;

public class CreateDesignationCommand : DesignationDto, IRequest<int>
{
}

public class CreateDesignationCommandHandler : IRequestHandler<CreateDesignationCommand, int>
{
    readonly IAppDbContext appDbContext;
    readonly ISessionContext sessionContext;

    public CreateDesignationCommandHandler(IAppDbContext appDbContext, ISessionContext sessionContext)
    {
        this.appDbContext = appDbContext;
        this.sessionContext = sessionContext;
    }

    public async Task<int> Handle(CreateDesignationCommand request, CancellationToken cancellationToken)
    {
        request.Name = request.Name.Trim();
        var nameExists = await appDbContext.Designations.AnyAsync(x => x.Name.ToLower() == request.Name.ToLower(), cancellationToken);
        if (nameExists)
        {
            throw new AppException("Another desingation with same name already exists. Please enter a unique name.");
        }

        string userId = await sessionContext.GetUserId();
        var entity = new Designation
        {
            Name = request.Name,
            CreatedBy = userId,
            ModifiedBy = userId,
            CreatedOn = DateTime.UtcNow,
            ModifiedOn = DateTime.UtcNow
        };

        appDbContext.Designations.Add(entity);
        await appDbContext.SaveAsync(cancellationToken);
        return entity.Id;
    }
}

public class CreateDesignationCommandValidator : AppAbstractValidator<CreateDesignationCommand>
{
    public CreateDesignationCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}

