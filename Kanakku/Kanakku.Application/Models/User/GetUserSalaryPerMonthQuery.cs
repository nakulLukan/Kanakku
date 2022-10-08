using Kanakku.Application.Contracts.Storage;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Application.Models.User;

public class GetUserSalaryPerMonthQuery : IRequest<float>
{
    public Guid EmpId { get; set; }
    public DateTime SalaryMonth { get; set; }
}

public class GetUserSalaryPerMonthQueryHandler : IRequestHandler<GetUserSalaryPerMonthQuery, float>
{
    readonly IAppDbContext appDbContext;

    public GetUserSalaryPerMonthQueryHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<float> Handle(GetUserSalaryPerMonthQuery request, CancellationToken cancellationToken)
    {
        var salaryPeriod = new DateTime(request.SalaryMonth.Year, request.SalaryMonth.Month, 1, 0, 0, 0, DateTimeKind.Local).ToUniversalTime();

        float salary = await appDbContext.WorkHistories
            .Where(x => x.EmployeeId == request.EmpId)
            .Where(x => x.WorkedOn >= salaryPeriod && x.WorkedOn < salaryPeriod.AddMonths(1))
            .SumAsync(x => x.Quantity * x.Work.Cost, cancellationToken);

        return salary;
    }
}

