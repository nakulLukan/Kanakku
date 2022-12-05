using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Application.Requests.User;

public class GetSalaryHistoryByIdQuery : IRequest<EmployeeRegistryEntryDto>
{
    public int Id { get; set; }
}
public class GetSalaryHistoryByIdQueryHandler : IRequestHandler<GetSalaryHistoryByIdQuery, EmployeeRegistryEntryDto>
{
    readonly IAppDbContext appDbContext;

    public GetSalaryHistoryByIdQueryHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<EmployeeRegistryEntryDto> Handle(GetSalaryHistoryByIdQuery request, CancellationToken cancellationToken)
    {
        return await appDbContext.EmployeeSalaryHistories
            .Select(x => new EmployeeRegistryEntryDto
            {
                Id = x.Id,
                NumberOfDaysPresent = x.DaysPresent,
                EmployeeId = x.EmpId,
                SalaryPeriod = x.Period.ToLocalTime(),
                SalaryPerPeriod = x.Salary,
                Bonus = x.Bonus
            })
            .SingleAsync(x => x.Id == request.Id, cancellationToken);
    }
}
