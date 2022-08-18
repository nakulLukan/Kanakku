using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.User;
using Kanakku.Domain.User;
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
        var employees = await _dbContext.Employees.Where(x => x.Email == request.Email || x.PhoneNumber == x.PhoneNumber)
            .Select(x => new
            {
                x.Id,
                x.Email,
                x.PhoneNumber
            }).ToListAsync(cancellationToken);

        if (employees.Any(x => x.Email == request.Email))
        {
            throw new Exception("User with same email already exists.");
        }

        if (employees.Any(x => x.PhoneNumber == request.PhoneNumber))
        {
            throw new Exception("User with same phone number already exists");
        }

        Employee emp = new Employee
        {
            PhoneNumber = request.PhoneNumber,
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
        };

        try
        {
            _dbContext.Employees.Add(emp);
            await _dbContext.SaveAsync(cancellationToken);
        }
        catch(Exception ex)
        {

        }
        return emp.Id;
    }
}

