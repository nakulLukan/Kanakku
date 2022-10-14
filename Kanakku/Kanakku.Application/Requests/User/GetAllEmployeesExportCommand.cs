using Kanakku.Application.Contracts.Essential;
using Kanakku.Application.Models.User;
using Kanakku.Shared;
using Kanakku.Shared.Models.ExportService;
using MediatR;

namespace Kanakku.Application.Requests.User;

public class GetAllEmployeesExportCommand : IRequest<string>
{
}

public class GetAllEmployeesExportCommandHandler : IRequestHandler<GetAllEmployeesExportCommand, string>
{
    readonly IMediator mediator;
    readonly IExportService exportService;

    public GetAllEmployeesExportCommandHandler(IMediator mediator, IExportService exportService)
    {
        this.mediator = mediator;
        this.exportService = exportService;
    }

    public async Task<string> Handle(GetAllEmployeesExportCommand request, CancellationToken cancellationToken)
    {
        var data = await GetDataToExport();
        string title = "Employees";
        List<ColumnMetaData> columnMetaData = GetColumnDefinitions();
        string subTitle = "List of all employees available in the system.";
        string fileDirectory = DirectoryConstant.EXPORT_DIRECTORY_PATH;
        string fileName = $"Employees {DateTime.Now.ToFileTime()}.pdf";
        var config = new PrintConfig<EmployeeExportDto>()
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
            DisplayName = "Name",
            PropertyName = nameof(EmployeeExportDto.Name),
            MinimumLength = 0.9f,
        });

        columnMetaData.Add(new()
        {
            DisplayName = "Code",
            PropertyName = nameof(EmployeeExportDto.EmpCode),
            MinimumLength = 0.4f,
        });
        columnMetaData.Add(new()
        {
            DisplayName = "DOB",
            PropertyName = nameof(EmployeeExportDto.DateOfBirth),
            MinimumLength = 0.8f,
        });
        columnMetaData.Add(new()
        {
            DisplayName = "Address",
            PropertyName = nameof(EmployeeExportDto.Address),
            MinimumLength = 1f,
        });
        columnMetaData.Add(new()
        {
            DisplayName = "Contact Number",
            PropertyName = nameof(EmployeeExportDto.PhoneNumber1),
            MinimumLength = 1f,
        });
        columnMetaData.Add(new()
        {
            DisplayName = "Date of Joining",
            PropertyName = nameof(EmployeeExportDto.DateOfJoining),
            MinimumLength = 1f,
        });
        columnMetaData.Add(new()
        {
            DisplayName = "Working Status",
            PropertyName = nameof(EmployeeExportDto.WorkingStatus),
            MinimumLength = 0.8f,
        });
        return columnMetaData;
    }

    private async Task<List<EmployeeExportDto>>
        GetDataToExport()
    {
        var data = await mediator.Send(new GetAllEmployeesQuery());
        foreach (var record in data)
        {
            record.DateOfBirth = record.DateOfBirth?.ToLocalTime();
            record.DateOfJoining = record.DateOfJoining?.ToLocalTime();
        }

        return data
            .Select(x => new EmployeeExportDto
            {
                Address = x.AddressLineOne,
                EmpCode = x.EmpCode,
                Name = x.Name,
                PhoneNumber1 = x.PhoneNumber1,
                WorkingStatus = GlobalFunctions.GetWorkingStatus(x.RegsignedOn),
                DateOfBirth = x.DateOfBirth?.ToString(AppSetting.DATE_FORMAT),
                DateOfJoining = x.DateOfJoining?.ToString(AppSetting.DATE_FORMAT),
            })
            .ToList();
    }
}

