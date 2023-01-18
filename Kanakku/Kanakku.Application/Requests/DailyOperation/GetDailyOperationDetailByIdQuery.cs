using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.DailyOperation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Application.Requests.DailyOperation;

public class GetDailyOperationDetailByIdQuery : IRequest<DailyOperationDetailDto>
{
    public int Id { get; set; }
}
public class GetDailyOperationDetailByIdQueryHandler : IRequestHandler<GetDailyOperationDetailByIdQuery, DailyOperationDetailDto>
{
    readonly IAppDbContext appDbContext;

    public GetDailyOperationDetailByIdQueryHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<DailyOperationDetailDto> Handle(GetDailyOperationDetailByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await appDbContext.WorkHistories.Select(x => new DailyOperationDetailDto
        {
            Id = x.Id,
            OperationId = x.WorkId,
            ProductId = x.Work.ProductId,
            WorkedBy = x.EmployeeId,
            WorkedOn = x.WorkedOn,
            Quantity = x.Quantity,
            VariantId = x.VariantId,
        })
            .FirstAsync(x => x.Id == request.Id, cancellationToken);
        result.WorkedTime = result.WorkedOn.Value.ToLocalTime().TimeOfDay;
        result.WorkedOn = result.WorkedOn.Value.ToLocalTime().Date;

        return result;
    }
}

