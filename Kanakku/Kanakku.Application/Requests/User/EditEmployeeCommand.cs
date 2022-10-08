using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.User;
using Kanakku.Shared.Extensions;
using Kanakku.Shared.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Application.Requests.User;

public class EditEmployeeCommand : EmployeeDto, IRequest<Guid>
{
}

public class EditEmployeeCommandHandler : IRequestHandler<EditEmployeeCommand, Guid>
{
    private readonly IAppDbContext _dbContext;
    readonly ISessionContext _sessionContext;

    public EditEmployeeCommandHandler(IAppDbContext dbContext, ISessionContext sessionContext)
    {
        _dbContext = dbContext;
        _sessionContext = sessionContext;
    }

    public async Task<Guid> Handle(EditEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employees = await _dbContext.Employees.Where(x => x.Id != request.Id && ((x.Email == request.Email && !string.IsNullOrEmpty(x.Email))
            || x.PhoneNumber1 == request.PhoneNumber1
            || x.PhoneNumber1 == request.PhoneNumber2
            || x.PhoneNumber2 == request.PhoneNumber1
            || (x.PhoneNumber2 == request.PhoneNumber2 && !string.IsNullOrEmpty(request.PhoneNumber2))))
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
        if (employees.Any(x => x.Email == request.Email))
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

        string userId = await _sessionContext.GetUserId();
        var employee = await _dbContext.Employees.AsTracking().SingleAsync(x => x.Id == request.Id);
        employee.PhoneNumber1 = request.PhoneNumber1;
        employee.Email = request.Email;
        employee.DistrictId = request.DistrictId;
        employee.StateId = request.StateId;
        employee.Name = request.Name;
        employee.Pincode = request.Pincode;
        employee.AddressLineOne = request.AddressLineOne;
        employee.ModifiedOn = DateTime.UtcNow;
        employee.ModifiedBy = userId;
        employee.DateOfJoining = request.DateOfJoining.Value.ToUniversalTime();
        employee.DateOfBirth = request.DateOfBirth.Value.ToUniversalTime();
        employee.PhoneNumber2 = request.PhoneNumber2;
        employee.EpfRegNo = request.EpfRegNo;
        employee.EsiRegNo = request.EsiRegNo;
        employee.DpImageId = request.DpImageId;
        employee.IdProofImageId = request.IdProofImageId;
        employee.DesignationId = request.DesignationId.Value;
        await _dbContext.SaveAsync(cancellationToken);

        return employee.Id;
    }
}

