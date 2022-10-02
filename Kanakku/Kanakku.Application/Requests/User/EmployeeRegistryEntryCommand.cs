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
            request.SalaryPeriod = request.SalaryPeriod?.ToDateTimeKind();
            var entryExists = await dbContext.EmployeeSalaryHistories
                .AnyAsync(x => x.EmpId == request.EmployeeId.Value
                    && x.Period == request.SalaryPeriod, cancellationToken);

            if (entryExists)
            {
                throw new AppException($"Record for the month '{(request.SalaryPeriod?.ToString("MMMM"))}' already exists.");
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
