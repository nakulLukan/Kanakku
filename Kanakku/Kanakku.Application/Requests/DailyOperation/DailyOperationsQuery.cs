using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.DailyOperation;
using Kanakku.Shared.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Application.Requests.DailyOperation
{
    public class DailyOperationsQuery : DailyOperationFilterDto, IRequest<OperationItemDto[]>
    {
    }

    public class DailyOperationsQueryHandler : IRequestHandler<DailyOperationsQuery, OperationItemDto[]>
    {
        readonly IAppDbContext dbContext;

        public DailyOperationsQueryHandler(IAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<OperationItemDto[]> Handle(DailyOperationsQuery request, CancellationToken cancellationToken)
        {
            var baseLinq = dbContext.WorkHistories.AsQueryable();
            if (request.WorkedBy != null && request.WorkedBy.Any())
            {
                baseLinq = baseLinq.Where(x => request.WorkedBy.Contains(x.EmployeeId));
            }

            if (request.WorkedFrom.HasValue)
            {
                request.WorkedFrom = request.WorkedFrom.Value.ToDateTimeKind();
                baseLinq = baseLinq.Where(x => x.WorkedOn >= request.WorkedFrom.Value);
            }

            if (request.WorkedTo.HasValue)
            {
                request.WorkedTo = request.WorkedTo.Value.ToDateTimeKind();
                baseLinq = baseLinq.Where(x => x.WorkedOn < request.WorkedTo.Value);
            }

            if (request.QuickFilter.HasValue)
            {
                DateTime filterDate = DateTime.Now.Date;
                DateTime filterDateTo = DateTime.Now.Date.AddYears(1);
                if (request.QuickFilter.Value == Shared.DateFilter.ThisMonth)
                {
                    filterDate = new DateTime(filterDate.Year, filterDate.Month, 1);
                }
                else if (request.QuickFilter.Value == Shared.DateFilter.LastMonth)
                {
                    filterDateTo = new DateTime(filterDate.Year, filterDate.Month, 1);
                    filterDate = new DateTime(filterDate.Year, filterDate.Month, 1).AddMonths(-1);
                }
                else if (request.QuickFilter.Value == Shared.DateFilter.Yesterday)
                {
                    filterDate = filterDate.AddDays(-1);
                    filterDateTo = filterDate.AddDays(1);
                }

                filterDate = filterDate.ToUniversalTime();
                filterDateTo = filterDateTo.ToUniversalTime();
                baseLinq = baseLinq.Where(x => x.WorkedOn >= filterDate && x.WorkedOn < filterDateTo);
            }

            if (request.Operations != null && request.Operations.Any())
            {
                baseLinq = baseLinq.Where(x => request.Operations.Contains(x.Work.Id));
            }
            else if (request.Products != null && request.Products.Any())
            {
                baseLinq = baseLinq.Where(x => request.Products.Contains(x.Work.ProductId));
            }

            return await baseLinq
                .OrderByDescending(x => x.WorkedOn)
                .Select(x => new OperationItemDto
                {
                    Id = x.Id,
                    Operation = x.Work.Name,
                    Product = $"{x.Work.Product.ShortCode} - {x.Work.Product.Name}",
                    Variant = x.Variant.ProductSize.Size,
                    VariantQty = x.Quantity,
                    WorkedOn = x.WorkedOn,
                    WorkedBy = x.Employee.Name,
                    TotalAmount = x.Quantity * x.Work.Cost,
                    VarianPrice = x.Work.Cost
                })
                .ToArrayAsync(cancellationToken);
        }
    }
}
