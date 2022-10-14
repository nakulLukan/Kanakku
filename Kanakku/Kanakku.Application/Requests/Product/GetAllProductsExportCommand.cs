using Kanakku.Application.Contracts.Essential;
using Kanakku.Application.Models.Product;
using Kanakku.Shared;
using Kanakku.Shared.Models.ExportService;
using MediatR;

namespace Kanakku.Application.Requests.Product;

public class GetAllProductsExportCommand : IRequest<string>
{
}
public class GetAllProductsExportCommandHandler : IRequestHandler<GetAllProductsExportCommand, string>
{
    readonly IMediator mediator;
    readonly IExportService exportService;

    public GetAllProductsExportCommandHandler(IMediator mediator, IExportService exportService)
    {
        this.mediator = mediator;
        this.exportService = exportService;
    }

    public async Task<string> Handle(GetAllProductsExportCommand request, CancellationToken cancellationToken)
    {
        var data = await mediator.Send(new GetAllProductsQuery());
        string title = "Products";
        List<ColumnMetaData> columnMetaData = GetColumnDefinitions();
        string subTitle = "List of all products available in the system.";
        string fileDirectory = DirectoryConstant.EXPORT_DIRECTORY_PATH;
        string fileName = $"Products {DateTime.Now.ToFileTime()}.pdf";
        var config = new PrintConfig<ProductListDto>()
        {
            Data = data,
            ColumnMetaData = columnMetaData,
            FooterMetaData = null,
            ShowSerialNumber = true,
            SubTitle = subTitle,
            Title = title,
        };

        var pdfPath = exportService.GenerateAndSavePdf(config, fileDirectory, fileName);
        return pdfPath;
    }

    private static List<ColumnMetaData> GetColumnDefinitions()
    {
        var columnMetaData = new List<ColumnMetaData>();
        columnMetaData.Add(new()
        {
            DisplayName = "Product Name",
            PropertyName = nameof(ProductListDto.Name),
            MinimumLength = 2f,
        });

        columnMetaData.Add(new()
        {
            DisplayName = "Model Number",
            PropertyName = nameof(ProductListDto.ShortCode),
            MinimumLength = 2f,
        });
        columnMetaData.Add(new()
        {
            DisplayName = "Quantity",
            PropertyName = nameof(ProductListDto.TotalQuantity),
            MinimumLength = 1f,
        });
        return columnMetaData;
    }
}

