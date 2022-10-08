using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Application.Requests.User;

public class GetAllDesignationQuery : IRequest<List<DesignationDto>>
{
}

public class GetAllDesignationQueryHandler : IRequestHandler<GetAllDesignationQuery, List<DesignationDto>>
{
    readonly IAppDbContext _dbContext;

    public GetAllDesignationQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<DesignationDto>> Handle(GetAllDesignationQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Designations.Select(x => new DesignationDto
        {
            Id = x.Id,
            Name = x.Name
        })
        .OrderBy(x => x.Name)
        .ToListAsync(cancellationToken);
    }
}

