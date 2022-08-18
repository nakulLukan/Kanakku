using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.Lookup;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Application.Requests.Lookup;

public class GetLookupByMasterQuery : IRequest<List<LookupDetailDto>>
{
    public string LookupMasterInternalName { get; set; }
}

public class GetLookupByMasterQueryHandler : IRequestHandler<GetLookupByMasterQuery, List<LookupDetailDto>>
{
    private readonly IAppDbContext _dbContext;

    public GetLookupByMasterQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<LookupDetailDto>> Handle(GetLookupByMasterQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.LookupDetails.Where(x => x.LookupMaster.InternalName == request.LookupMasterInternalName)
            .Select(x => new LookupDetailDto
            {
                Id = x.Id,
                Value = x.Value
            }).ToListAsync(cancellationToken);
    }
}


