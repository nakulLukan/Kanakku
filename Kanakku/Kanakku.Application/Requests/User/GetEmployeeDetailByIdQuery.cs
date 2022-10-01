using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Application.Requests.User
{
    public class GetEmployeeDetailByIdQuery : IRequest<EmployeeDto>
    {
        public Guid EmpId { get; set; }
    }

    public class GetEmployeeDetailByIdHandler : IRequestHandler<GetEmployeeDetailByIdQuery, EmployeeDto>
    {
        readonly IAppDbContext appDbContext;
        public GetEmployeeDetailByIdHandler(IAppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<EmployeeDto> Handle(GetEmployeeDetailByIdQuery request, CancellationToken cancellationToken)
        {
            return await (from emp in appDbContext.Employees
                          join state in appDbContext.LookupDetails
                          on emp.StateId equals state.Id into States
                          from s in States.DefaultIfEmpty()
                          join district in appDbContext.LookupDetails
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
                          .FirstAsync(x => x.Id == request.EmpId, cancellationToken);
        }
    }
}
