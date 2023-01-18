using AutoMapper;
using Kanakku.Application.Contracts.Essential;
using Kanakku.Application.Models.User;
using Kanakku.Shared;
using Kanakku.Shared.Extensions;
using Kanakku.Shared.Models.ExportService;
using MediatR;
using System.Text;

namespace Kanakku.Application.Requests.User;

public class EmployeeRegistryExportCommand : EmployeeRegistryFilterDto, IRequest<string>
{
}

public class SalaryHistoryExportCommandHandler : IRequestHandler<EmployeeRegistryExportCommand, string>
{
    readonly IMediator mediator;
    readonly IMapper mapper;
    readonly IExportService exportService;

    public SalaryHistoryExportCommandHandler(IMediator mediator, IMapper mapper, IExportService exportService)
    {
        this.mediator = mediator;
        this.mapper = mapper;
        this.exportService = exportService;
    }

    public async Task<string> Handle(EmployeeRegistryExportCommand request, CancellationToken cancellationToken)
    {
        var filter = (EmployeeRegistryFilterDto)request;
        var data = await GetDataToExport(filter);
        string title = "Salary History";

        var columnMetaData = new List<ColumnMetaData>
        {
            new()
            {
                DisplayName = "Name",
                PropertyName = nameof(EmployeeRegistryExportDto.EmpName),
                MinimumLength = 1.5f,
            },
            new()
            {
                DisplayName = "Code",
                PropertyName = nameof(EmployeeRegistryExportDto.EmpCode),
                MinimumLength = 0.5f,
            },
            new()
            {
                DisplayName = "Period",
                PropertyName = nameof(EmployeeRegistryExportDto.SalaryMonth)
            },
            new()
            {
                DisplayName = "Present Days",
                PropertyName = nameof(EmployeeRegistryExportDto.DaysPresent),
                MinimumLength = 1f,
            },
            new()
            {
                DisplayName = "Salary",
                PropertyName = nameof(EmployeeRegistryExportDto.Salary),
                MinimumLength = 1.5f
            },
        };

        if (request.ShowBonus)
        {
            columnMetaData.Add(
                new()
                {
                    DisplayName = "Bonus",
                    PropertyName = nameof(EmployeeRegistryExportDto.Bonus),
                    MinimumLength = 1f
                });
        }

        var footerMetaData = new FooterMetaData
        {
            FooterText = "Total Salary : ",
            FooterTextValue = $"{data.TotalAmt.ToCurrencyAsAscii()} INR"
        };

        if (request.ShowBonus)
        {
            footerMetaData.FooterText = $"{footerMetaData.FooterText}\nTotal Bonus : ";
            footerMetaData.FooterTextValue = $"{data.TotalAmt.ToCurrencyAsAscii()} INR\n{data.TotalBonus.ToCurrencyAsAscii()} INR";
        }

        string fileDirectory = DirectoryConstant.EXPORT_DIRECTORY_PATH;
        string fileName = $"Salary History {DateTime.Now.ToFileTime()}.pdf";
        var config = new PrintConfig<EmployeeRegistryExportDto>()
        {
            Data = data.ExportData,
            ColumnMetaData = columnMetaData,
            FooterMetaData = footerMetaData,
            ShowSerialNumber = true,
            SubTitle = data.SubTitle,
            Title = title,
        };

        var pdfPath = exportService.GenerateAndSavePdf(config, fileDirectory, fileName);

        return pdfPath;
    }

    private async Task<(List<EmployeeRegistryExportDto> ExportData,
        float TotalAmt,
        float TotalBonus,
        string SubTitle)>
        GetDataToExport(EmployeeRegistryFilterDto filter)
    {
        var data = await mediator.Send(mapper.Map(filter, new EmployeeRegistryQuery()));
        foreach (var record in data)
        {
            record.SalaryMonth = record.SalaryMonth.ToLocalTime();
        }
        var totalAmount = data.Sum(x => x.Salary);
        var totalBonus = data.Sum(x => x.Bonus) ?? 0F;
        StringBuilder subTitle = new StringBuilder();
        if (data.Any())
        {
            string userPlaceholder = "employees";
            if (data.Select(x => x.SalaryMonth).Distinct().Count() == 1)
            {
                userPlaceholder = data.First().EmpName;
            }
            subTitle.Append($"Salary history of {userPlaceholder}");
            var minDate = data.Min(x => x.SalaryMonth).ToString(AppSetting.MONTH_YEAR_FORMAT);
            var maxDate = data.Max(x => x.SalaryMonth).ToString(AppSetting.MONTH_YEAR_FORMAT);
            if (minDate == maxDate)
            {
                subTitle.Append($" in the month of {minDate}.");
            }
            else
            {
                subTitle.Append($" from {minDate} to {maxDate}.");
            }
        }

        return (data
            .Select(x => new EmployeeRegistryExportDto
            {
                Salary = x.Salary.ToCurrencyAsAscii(),
                SalaryMonth = x.SalaryMonth.ToString(AppSetting.MONTH_YEAR_FORMAT),
                EmpName = x.EmpName,
                DaysPresent = x.DaysPresent,
                EmpCode = x.EmpCode,
                Bonus = x.Bonus.ToCurrencyAsAscii(),
            })
            .ToList(), totalAmount, totalBonus, subTitle.ToString());
    }
}

