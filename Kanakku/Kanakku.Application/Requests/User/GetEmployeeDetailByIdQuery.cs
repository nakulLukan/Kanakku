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
            return await appDbContext.Employees
                .Select(x => new EmployeeDto
                {
                    Id = x.Id,
                    AddressLineOne = x.AddressLineOne,
                    DateOfBirth = x.DateOfBirth,
                    DistrictId = x.DistrictId,
                    StateId = x.StateId,
                    EmpCode = x.Code,
                    Email = x.Email,
                    Pincode = x.Pincode,
                    PhoneNumber2 = x.PhoneNumber2,
                    PhoneNumber1 = x.PhoneNumber1,
                    DpImageId = x.DpImageId,
                    Name = x.Name,
                    IdProofImageId = x.IdProofImageId,
                    EpfRegNo = x.EpfRegNo,
                    EsiRegNo = x.EsiRegNo,
                    State = x.State.Value,
                    District = x.District.Value,
                })
                .FirstAsync(x => x.Id == request.EmpId, cancellationToken);
        }
    }
}
