using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.User;
using Kanakku.Shared.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Application.Requests.User;

public class EmployeeRegistryQuery : EmployeeRegistryFilterDto, IRequest<EmployeeRegistryDto[]>
{
}

public class EmployeeRegistryQueryHandler : IRequestHandler<EmployeeRegistryQuery, EmployeeRegistryDto[]>
{
    readonly IAppDbContext _appDbContext;

    public EmployeeRegistryQueryHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public Task<EmployeeRegistryDto[]> Handle(EmployeeRegistryQuery request, CancellationToken cancellationToken)
    {
        request.From = request.From.ToDateTimeKind();
        request.To = request.To.ToDateTimeKind();

        var linq = _appDbContext.EmployeeSalaryHistories.AsQueryable();
        if (request.Employees != null && request.Employees.Any())
        {
            linq = linq.Where(x => request.Employees.Contains(x.EmpId));
        }

        if (request.DateFilter == Shared.EmployeeSalaryDateFilter.ThisMonth)
        {
            linq = linq.Where(x => x.Period.Month == DateTime.Now.Month && x.Period.Year == DateTime.Now.Year);
        }
        else if (request.DateFilter == Shared.EmployeeSalaryDateFilter.ThisYear)
        {
            linq = linq.Where(x => x.Period.Year == DateTime.Now.Year);
        }

        if (request.From.HasValue)
        {
            linq = linq.Where(x => x.Period >= request.From);
        }

        if (request.To.HasValue)
        {
            linq = linq.Where(x => x.Period <= request.To);
        }

        return linq
            .OrderByDescending(x => x.Period)
            .ThenByDescending(x => x.ModifiedOn)
            .Select(x => new EmployeeRegistryDto
            {
                DaysPresent = x.DaysPresent,
                EmpCode = x.Employee.Code,
                EmpName = x.Employee.Name,
                Id = x.Id,
                Salary = x.Salary,
                SalaryMonth = x.Period
            })
            .ToArrayAsync(cancellationToken);
    }
}
