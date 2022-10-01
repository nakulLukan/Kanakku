using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.User;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Xml.Linq;

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
        var result = await (from emp in _dbContext.Employees
                     join state in _dbContext.LookupDetails
                     on emp.StateId equals state.Id into States
                     from s in States.DefaultIfEmpty()
                     join district in _dbContext.LookupDetails
                     on emp.DistrictId equals district.Id into Districts
                     from d in Districts.DefaultIfEmpty()
                     orderby emp.Code
                     select new EmployeeDto
                     {
                         Id = emp.Id,
                         Name = emp.Name,
                         DistrictId = emp.DistrictId,
                         District = d.Value,
                         Email = emp.Email,
                         StateId = emp.StateId,
                         State = s.Value,
                         Pincode = emp.Pincode,
                         PhoneNumber1 = emp.PhoneNumber1,
                         AddressLineOne = emp.AddressLineOne,
                         DateOfBirth = emp.DateOfBirth,
                         DpImageId = emp.DpImageId,
                         EmpCode = emp.Code,
                         EpfRegNo = emp.EpfRegNo,
                         EsiRegNo = emp.EsiRegNo,
                         IdProofImageId = emp.IdProofImageId,
                         PhoneNumber2 = emp.PhoneNumber2,
                         DateOfJoining = emp.DateOfJoining,
                     })
                     .ToListAsync(cancellationToken);

        int rowNum = 1;
        result.ForEach(x => x.RowNumber = rowNum++);
        return result;
    }
}
