using Kanakku.Application.Contracts.Essential;
using Kanakku.Application.Models.Product;
using Kanakku.Application.Requests.Attachment;
using Kanakku.Shared;
using Kanakku.Shared.Extensions;
using Kanakku.Shared.Models.ExportService;
using MediatR;
using System.Text;

namespace Kanakku.Application.Requests.Product;

public class ProductDetailExportCommand : IRequest<string>
{
    public int ProductId { get; set; }
}

public class ProductDetailExportCommandHandler : IRequestHandler<ProductDetailExportCommand, string>
{
    readonly IMediator mediator;
    readonly IExportService exportService;

    public ProductDetailExportCommandHandler(IExportService exportService, IMediator mediator)
    {
        this.exportService = exportService;
        this.mediator = mediator;
    }

    public async Task<string> Handle(ProductDetailExportCommand request, CancellationToken cancellationToken)
    {
        var data = await GetDataToExport(request.ProductId);
        List<ColumnMetaData> columnMetaData = GetColumnDefinitions();
        FooterMetaData footerMetaData = new FooterMetaData
        {
            FooterText = "Total : ",
            FooterTextValue = $"{data.TotalAmt.ToCurrencyAsAscii()} INR",
        };
        string fileDirectory = DirectoryConstant.EXPORT_DIRECTORY_PATH;
        string fileName = $"Products {DateTime.Now.ToFileTime()}.pdf";
        var config = new PrintConfig<WorkExportDto>()
        {
            Data = data.ExportData,
            ColumnMetaData = columnMetaData,
            FooterMetaData = footerMetaData,
            ShowSerialNumber = true,
            SubTitle = data.SubTitle,
            Title = data.Title,
            DisplayPictureConfig = new DisplayPictureConfig
            {
                DpPath = data.DpPath,
                RelativeSpaceRequired = string.IsNullOrEmpty(data.DpPath) ? 0f : 1f
            }
        };

        var pdfPath = exportService.GenerateAndSavePdf(config, fileDirectory, fileName);
        return pdfPath;
    }

    private async Task<(List<WorkExportDto> ExportData,
        float TotalAmt,
        string Title,
        string SubTitle,
        string DpPath)>
        GetDataToExport(int prodId)
    {
        var data = await mediator.Send(new GetProductDetailByIdQuery()
        {
            Id = prodId
        });

        string dpPath = string.Empty;
        if (data.ImageId.HasValue)
        {
            var dpPathDetails = await mediator.Send(new ResourceQuery()
            {
                ResourceId = data.ImageId.Value
            });
            dpPath = dpPathDetails.LocalPath;
        }
        float totalAmount = data.Works.Sum(x => x.Rate);
        string title = $"{data.Name} ({data.ShortCode})";
        StringBuilder subTitle = new StringBuilder(" ");
        return (data.Works
            .Select(x => new WorkExportDto
            {
                Rate = x.Rate.ToCurrencyAsAscii(),
                WorkName = x.WorkName,
            })
            .ToList(), totalAmount, title, subTitle.ToString(), dpPath);
    }

    private static List<ColumnMetaData> GetColumnDefinitions()
    {
        var columnMetaData = new List<ColumnMetaData>();
        columnMetaData.Add(new()
        {
            DisplayName = "Operation Name",
            PropertyName = nameof(WorkExportDto.WorkName),
            MinimumLength = 1f,
        });

        columnMetaData.Add(new()
        {
            DisplayName = "Rate",
            PropertyName = nameof(WorkExportDto.Rate),
            MinimumLength = 1f,
        });
        return columnMetaData;
    }
}
