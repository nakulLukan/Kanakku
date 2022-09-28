using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.User;
using Kanakku.Domain.User;
using Kanakku.Shared.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Application.Requests.User;

public class CreateEmployeeCommand : EmployeeDto, IRequest<Guid>
{
}

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Guid>
{
    private readonly IAppDbContext _dbContext;

    public CreateEmployeeCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employees = await _dbContext.Employees.Where(x => x.Email == request.Email
            || x.PhoneNumber1 == request.PhoneNumber1
            || x.PhoneNumber1 == request.PhoneNumber2
            || x.PhoneNumber2 == request.PhoneNumber1
            || (x.PhoneNumber2 == request.PhoneNumber2 && !string.IsNullOrEmpty(request.PhoneNumber2)))
            .Select(x => new
            {
                x.Id,
                x.Email,
                x.PhoneNumber1,
                x.PhoneNumber2,
                x.Code
            }).ToListAsync(cancellationToken);

        if (employees.Any(x => x.Code == request.EmpCode))
        {
            throw new AppException("Employee code cannot be duplicate");
        }
        if (!string.IsNullOrEmpty(request.Email) && employees.Any(x => x.Email == request.Email))
        {
            throw new AppException("User with same email already regeistered.");
        }

        if (employees.Any(x => x.PhoneNumber1 == request.PhoneNumber1
            || x.PhoneNumber1 == request.PhoneNumber2
            || x.PhoneNumber2 == request.PhoneNumber1
            || (x.PhoneNumber2 == request.PhoneNumber2 && !string.IsNullOrEmpty(request.PhoneNumber2))))
        {
            throw new AppException("User with same phone number already registered.");
        }

        Employee emp = new Employee
        {
            PhoneNumber1 = request.PhoneNumber1,
            Email = request.Email,
            DistrictId = request.DistrictId,
            StateId = request.StateId,
            Name = request.Name,
            Pincode = request.Pincode,
            AddressLineOne = request.AddressLineOne,
            CreatedOn = DateTime.UtcNow,
            ModifiedOn = DateTime.UtcNow,
            CreatedBy = String.Empty,
            ModifiedBy = String.Empty,
            Code = request.EmpCode,
            PhoneNumber2 = request.PhoneNumber2,
            DateOfBirth = request.DateOfBirth.Value.ToUniversalTime(),
            DateOfJoining = request.DateOfJoining.Value.ToUniversalTime(),
            EpfRegNo = request.EpfRegNo,
            EsiRegNo = request.EsiRegNo,
            DpImageId = request.DpImageId,
            IdProofImageId = request.IdProofImageId,
        };
        _dbContext.Employees.Add(emp);
        await _dbContext.SaveAsync(cancellationToken);

        return emp.Id;
    }
}

