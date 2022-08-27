﻿using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Application.Requests.User;

public class GetAllEmployeesQuery : IRequest<List<EmployeeDto>>
{
}

public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, List<EmployeeDto>>
{
    private readonly IAppDbContext _dbContext;

    public GetAllEmployeesQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<EmployeeDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Employees.Select(x => new EmployeeDto
        {
            Id = x.Id,
            Name = x.Name,
            DistrictId = x.DistrictId,
            District = x.District.Value,
            Email = x.Email,
            State = x.State.Value,
            StateId = x.StateId,
            Pincode = x.Pincode,
            PhoneNumber = x.PhoneNumber,
            AddressLineOne = x.AddressLineOne
        }).ToListAsync(cancellationToken);
    }
}