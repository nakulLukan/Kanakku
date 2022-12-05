using AutoMapper;
using Kanakku.Application.Contracts.Essential;
using Kanakku.Application.Models.DailyOperation;
using Kanakku.Shared;
using Kanakku.Shared.Extensions;
using Kanakku.Shared.Models.ExportService;
using MediatR;
using System.Text;

namespace Kanakku.Application.Requests.DailyOperation;

public class DailyOperationsExportCommand : DailyOperationFilterDto, IRequest<string>
{
}

public class DailyOperationsExportCommandHandler : IRequestHandler<DailyOperationsExportCommand, string>
{
    readonly IMediator mediator;
    readonly IMapper mapper;
    readonly IExportService exportService;

    public DailyOperationsExportCommandHandler(IMediator mediator, IMapper mapper, IExportService exportService)
    {
        this.mediator = mediator;
        this.mapper = mapper;
        this.exportService = exportService;
    }

    public async Task<string> Handle(DailyOperationsExportCommand request, CancellationToken cancellationToken)
    {
        var filter = (DailyOperationFilterDto)request;
        (List<OperationItemExportDto> data, float totalAmt, string subTitle, bool isSingleUser) =
            await GetDataToExport(filter);
        string title = "Daily Operations";
        List<ColumnMetaData> columnMetaData = GetColumnDefinitions(isSingleUser);

        var footerMetaData = new FooterMetaData
        {
            FooterText = "Total Amount : ",
            FooterTextValue = $"{totalAmt.ToCurrencyAsAscii()} INR"
        };

        string fileDirectory = DirectoryConstant.EXPORT_DIRECTORY_PATH;
        string fileName = $"Daily Oerations {DateTime.Now.ToFileTime()}.pdf";
        var config = new PrintConfig<OperationItemExportDto>()
        {
            Data = data,
            ColumnMetaData = columnMetaData,
            FooterMetaData = footerMetaData,
            ShowSerialNumber = false,
            SubTitle = subTitle,
            Title = title,
        };

        var pdfPath = exportService.GenerateAndSavePdf(config, fileDirectory, fileName);

        return pdfPath;
    }

    private static List<ColumnMetaData> GetColumnDefinitions(bool isSingleUser)
    {
        var columnMetaData = new List<ColumnMetaData>();
        columnMetaData.Add(new()
        {
            DisplayName = "Worked On",
            PropertyName = nameof(OperationItemExportDto.WorkedOn),
            MinimumLength = 1.1f,
        });

        if (!isSingleUser)
        {
            columnMetaData.Add(new()
            {
                DisplayName = "Worked By",
                PropertyName = nameof(OperationItemExportDto.WorkedBy),
                MinimumLength = 1.1f,
            });
        }
        columnMetaData.Add(new()
        {
            DisplayName = "Product",
            PropertyName = nameof(OperationItemExportDto.Product),
            MinimumLength = 1.7f,
        });
        columnMetaData.Add(new()
        {
            DisplayName = "Operation",
            PropertyName = nameof(OperationItemExportDto.Operation),
            MinimumLength = 1.7f,
        });
        columnMetaData.Add(new()
        {
            DisplayName = "Size",
            PropertyName = nameof(OperationItemExportDto.Variant),
            MinimumLength = 1f
        });
        columnMetaData.Add(new()
        {
            DisplayName = "Price",
            PropertyName = nameof(OperationItemExportDto.VarianPrice),
            MinimumLength = 0.7f
        });
        columnMetaData.Add(new()
        {
            DisplayName = "Qty",
            PropertyName = nameof(OperationItemExportDto.VariantQty),
            MinimumLength = 0.5f
        });
        columnMetaData.Add(new()
        {
            DisplayName = "Amt",
            PropertyName = nameof(OperationItemExportDto.TotalAmount),
            MinimumLength = 0.8f
        });
        return columnMetaData;
    }

    private async Task<(List<OperationItemExportDto> ExportData,
        float TotalAmt,
        string SubTitle,
        bool isSingleUser)>
        GetDataToExport(DailyOperationFilterDto filter)
    {
        var data = await mediator.Send(mapper.Map(filter, new DailyOperationsQuery()));
        float totalAmount = data.Sum(x => x.TotalAmount);
        bool isSingleUser = false;
        foreach (var record in data)
        {
            record.WorkedOn = record.WorkedOn.ToLocalTime();
        }

        StringBuilder subTitle = new StringBuilder();
        if (data.Any())
        {
            string userPlaceholder = "employees";
            if (data.Select(x => x.WorkedBy).Distinct().Count() == 1)
            {
                userPlaceholder = data.First().WorkedBy;
                isSingleUser = true;
            }
            subTitle.Append($"Work history of {userPlaceholder}");
            var minDate = data.Min(x => x.WorkedOn).ToString(AppSetting.DATE_FORMAT);
            var maxDate = data.Max(x => x.WorkedOn).ToString(AppSetting.DATE_FORMAT);
            if (minDate == maxDate)
            {
                subTitle.Append($" on {minDate}.");
            }
            else
            {
                subTitle.Append($" from {minDate} to {maxDate}.");
            }
        }

        return (data
            .Select(x => new OperationItemExportDto
            {
                Operation = x.Operation,
                Product = x.Product,
                TotalAmount = x.TotalAmount.ToCurrencyAsAscii(),
                VarianPrice = x.VarianPrice.ToCurrencyAsAscii(),
                Variant = x.Variant,
                VariantQty = x.VariantQty,
                WorkedBy = x.WorkedBy,
                WorkedOn = x.WorkedOn.ToString(AppSetting.DATE_FORMAT),
            })
            .ToList(), totalAmount, subTitle.ToString(), isSingleUser);
    }
}

