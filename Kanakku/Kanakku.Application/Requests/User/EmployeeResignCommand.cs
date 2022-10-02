using Kanakku.Application.Contracts.Storage;
using Kanakku.Domain.User;
using MediatR;

namespace Kanakku.Application.Requests.User;

public class EmployeeResignCommand : IRequest<bool>
{
    public Guid EmpId { get; set; }
    public DateTime? ResignOn { get; set; }
}

public class EmployeeResignCommandHandler : IRequestHandler<EmployeeResignCommand, bool>
{
    readonly IAppDbContext dbContext;

    public EmployeeResignCommandHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<bool> Handle(EmployeeResignCommand request, CancellationToken cancellationToken)
    {
        Employee emp = new Employee
        {
            Id = request.EmpId,
            ResignedOn = request.ResignOn?.ToUniversalTime()
        };
        dbContext.ChangePropertyStateToModified(emp, nameof(Employee.ResignedOn));
        await dbContext.SaveAsync(cancellationToken);
        return true;
    }
}
