using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.DailyOperation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Application.Requests.User
{
    public class EmployeeMappingQuery : IRequest<EmployeeMappingDto[]>
    {
    }
    public class EmployeeMappingQueryHandler : IRequestHandler<EmployeeMappingQuery, EmployeeMappingDto[]>
    {
        readonly IAppDbContext _appDbContext;

        public EmployeeMappingQueryHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<EmployeeMappingDto[]> Handle(EmployeeMappingQuery request, CancellationToken cancellationToken)
        {
            return await _appDbContext.Employees.OrderBy(x => x.Code)
                .Select(x => new EmployeeMappingDto
                {
                    EmployeeId = x.Id,
                    EmployeeName = $"({x.Code}) - {x.Name}"
                }).ToArrayAsync(cancellationToken);
        }
    }
}
