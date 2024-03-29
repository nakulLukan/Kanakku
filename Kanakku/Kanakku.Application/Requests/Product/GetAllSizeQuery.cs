﻿using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.Product;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Application.Requests.Product;

public class GetAllSizeQuery : IRequest<List<SizeDto>>
{
}

public class GetAllSizeQueryHandler : IRequestHandler<GetAllSizeQuery, List<SizeDto>>
{
    private readonly IAppDbContext _dbContext;

    public GetAllSizeQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<SizeDto>> Handle(GetAllSizeQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.ProductSizes
            .Select(x => new
            {
                Id = x.Id,
                Value = x.Size,
                Order = x.Order,
                MasterId = x.MasterId,
                MasterOrder = x.Master != null ? x.Master.Order : 0,
                MasterName = x.Master != null ? x.Master.MasterName : String.Empty
            })
            .OrderBy(x => x.MasterOrder)
            .ThenBy(x => x.Order)
            .Select(x => new SizeDto
            {
                Id = x.Id,
                Value = x.Value,
                Order = x.Order,
                MasterId = x.MasterId,
                MasterName = x.MasterName
            })
            .ToListAsync(cancellationToken);
    }
}
