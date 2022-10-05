using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.User;
using Kanakku.Domain.User;
using Kanakku.Shared.Extensions;
using Kanakku.Shared.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Application.Requests.User
{
    public class EmployeeRegistryEntryCommand : EmployeeRegistryEntryDto, IRequest<int>
    {
    }

    public class EmployeeRegistryEntryCommandHandler : IRequestHandler<EmployeeRegistryEntryCommand, int>
    {
        readonly IAppDbContext dbContext;
        readonly ISessionContext sessionContext;

        public EmployeeRegistryEntryCommandHandler(IAppDbContext dbContext, ISessionContext sessionContext)
        {
            this.dbContext = dbContext;
            this.sessionContext = sessionContext;
        }

        public async Task<int> Handle(EmployeeRegistryEntryCommand request, CancellationToken cancellationToken)
        {
            var daysToSub = (1 - request.SalaryPeriod.Value.Date.Day);
            request.SalaryPeriod = request.SalaryPeriod?.Date.AddDays(daysToSub).ToDateTimeKind();
            var entryExists = await dbContext.EmployeeSalaryHistories
                .AnyAsync(x => x.EmpId == request.EmployeeId.Value
                    && x.Period == request.SalaryPeriod, cancellationToken);

            if (entryExists)
            {
                throw new AppException($"Record for the month '{(request.SalaryPeriod?.ToString("MMMM"))}' already exists.");
            }

            var maxDaysInMonth = DateTime.DaysInMonth(request.SalaryPeriod.Value.Year, request.SalaryPeriod.Value.Month);
            if (DateTime.DaysInMonth(request.SalaryPeriod.Value.Year, request.SalaryPeriod.Value.Month) < request.NumberOfDaysPresent)
            {
                throw new AppException($"Number of days present should be less than or equal to '{maxDaysInMonth}' for the month '{request.SalaryPeriod.Value.ToString("MMMM")}'");
            }
            var userId = await sessionContext.GetUserId();
            var entry = new EmployeeSalaryHistory
            {
                DaysPresent = request.NumberOfDaysPresent,
                EmpId = request.EmployeeId.Value,
                Salary = request.SalaryPerPeriod,
                Period = request.SalaryPeriod.Value,
                CreatedBy = userId,
                ModifiedBy = userId,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow
            };

            await dbContext.EmployeeSalaryHistories.AddAsync(entry);
            await dbContext.SaveAsync(cancellationToken);
            return entry.Id;
        }
    }
}
