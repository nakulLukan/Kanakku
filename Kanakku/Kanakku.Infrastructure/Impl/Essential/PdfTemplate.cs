using Kanakku.Shared;
using Kanakku.Shared.Models.ExportService;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Reflection;

namespace Kanakku.Infrastructure.Impl.Essential;

internal class PdfTemplate<T> : IDocument
{
    public string Title { get; set; }
    public string SubTitle { get; set; }
    public List<T> Data { get; set; }
    public List<ColumnMetaData> ColumnMetaData { get; set; }
    public FooterMetaData FooterMetaData { get; set; }
    public bool ShowSerialNumber { get; set; }
    public DisplayPictureConfig DisplayPictureConfig { get; set; }
    public PrintConfig<T> Config { get; set; }

    readonly IDictionary<string, PropertyInfo> propertyInfos = new Dictionary<string, PropertyInfo>();

    public PdfTemplate(PrintConfig<T> config)
    {
        Title = config.Title;
        Data = config.Data;
        ColumnMetaData = config.ColumnMetaData;
        FooterMetaData = config.FooterMetaData;
        ShowSerialNumber = config.ShowSerialNumber;
        SubTitle = config.SubTitle;
        DisplayPictureConfig = config.DisplayPictureConfig;
        Config = config;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(30);

            page.Header().Element(ComposeHeader);
            page.Content().Element(ComposeContent);
            page.Footer().Element(ComposeFooter);
        });
    }

    void ComposeHeader(IContainer container)
    {
    }

    void ComposeContent(IContainer container)
    {
        const int TitleFontSize = 16;
        const int SubTitleFontSize = 12;
        container.Column(col =>
        {
            col.Item()
            .AlignLeft()
            .Text(Title)
                .FontSize(TitleFontSize)
                .ExtraBold()
                .Underline();

            col.Item()
            .Row(row =>
            {
                row.RelativeColumn()
                .ExtendHorizontal()
                .Column(col =>
                {

                    if (!string.IsNullOrEmpty(SubTitle))
                    {
                        col.Item()
                        .AlignLeft()
                        .PaddingTop(10)
                        .PaddingBottom(5)
                        .Text(SubTitle)
                            .FontSize(SubTitleFontSize)
                            .Bold();
                    }

                    col.Item()
                    .Table(ComposeTable);
                });

                if (DisplayPictureConfig != null && !string.IsNullOrEmpty(DisplayPictureConfig.DpPath))
                {
                    row.RelativeItem(DisplayPictureConfig.RelativeSpaceRequired)
                    .PaddingLeft(10)
                    .ShowOnce()
                    .Column(col =>
                    {
                        col.Item()
                        .Padding(10)
                        .Text(" ");

                        col.Item()
                        .PaddingLeft(20)
                        .AlignCenter()
                        .MaxHeight(200)
                        .Image(DisplayPictureConfig.DpPath, ImageScaling.FitHeight);
                    });
                }
            });
        });
    }

    void ComposeFooter(IContainer container)
    {
        int FooterFontSize = 10;
        container
            .BorderTop(1)
            .PaddingTop(5)
            .Grid(grid =>
            {
                grid.Columns(12);

                grid.Item(6)
                .AlignLeft()
                .Text(text =>
                {
                    text
                    .Span("Generated on ")
                    .FontSize(FooterFontSize)
                    .FontColor(Colors.Grey.Darken1);

                    text
                    .Span(DateTime.Now.ToString(AppSetting.DATE_TIME_FORMAT))
                    .FontSize(FooterFontSize)
                    .FontColor(Colors.Grey.Darken1)
                    .Bold();
                });

                grid.Item(6)
                .AlignRight()
                .Text(text =>
                {
                    text.Span("Page ")
                    .FontSize(FooterFontSize)
                    .FontColor(Colors.Grey.Darken1);
                    text.CurrentPageNumber()
                    .FontSize(FooterFontSize)
                    .FontColor(Colors.Grey.Darken1);
                    text.Span(" / ")
                    .FontSize(FooterFontSize)
                    .FontColor(Colors.Grey.Darken1);
                    text.TotalPages()
                    .FontSize(FooterFontSize)
                    .FontColor(Colors.Grey.Darken1);
                });
            });
    }

    void ComposeTable(TableDescriptor table)
    {
        int TableFontSize = Config.TableFontSize;
        table.ColumnsDefinition(columnDef =>
        {
            if (ShowSerialNumber)
            {
                columnDef.ConstantColumn(30);
            }
            ColumnMetaData.ForEach(cm =>
            {
                columnDef.RelativeColumn(cm.MinimumLength ?? 1);
            });
        });

        table.Header(header =>
        {
            static IContainer CellStyle(IContainer container)
            {
                return container.DefaultTextStyle(x => x.SemiBold())
                .PaddingVertical(5)
                .BorderHorizontal(1)
                .BorderColor(Colors.Black)
                .PaddingHorizontal(2);
            }

            if (ShowSerialNumber)
            {
                header.Cell().Element(CellStyle)
                   .AlignCenter()
                   .AlignMiddle()
                   .Text("#")
                   .FontSize(TableFontSize);
            }
            ColumnMetaData.ForEach(cm =>
            {
                header.Cell().Element(CellStyle)
                .AlignLeft()
                .AlignMiddle()
                .PaddingHorizontal(1)
                .Text(cm.DisplayName)
                .FontSize(TableFontSize);
            });
        });

        // Data table
        uint index = 1;
        foreach (var record in Data)
        {
            static IContainer CellStyle(IContainer container)
            {
                return container.BorderBottom(1)
                    .BorderColor(Colors.Grey.Lighten2)
                    .Padding(2);
            }

            if (ShowSerialNumber)
            {
                table.Cell().Element(CellStyle)
                        .AlignMiddle()
                        .AlignCenter()
                        .Text(index++)
                        .FontSize(TableFontSize);
            }

            foreach (var cm in ColumnMetaData)
            {
                table.Cell().Element(CellStyle)
                    .AlignMiddle()
                    .AlignLeft()
                    .Text(GetProperty(cm.PropertyName).GetValue(record))
                    .FontSize(TableFontSize);
            }
        }

        // Set table footer
        if (FooterMetaData != null)
        {
            table.Cell()
            .ColumnSpan((uint)(ColumnMetaData.Count + (ShowSerialNumber ? 1 : 0)))
            .AlignRight()
            .PaddingVertical(5)
            .Row(row =>
            {
                row.AutoItem()
                .Text(FooterMetaData.FooterText)
                .FontSize(FooterMetaData.FooterFontSize);
                row.AutoItem()
                .Text(FooterMetaData.FooterTextValue)
                .FontSize(FooterMetaData.FooterFontSize)
                .Bold();
            });
        }
    }

    PropertyInfo GetProperty(string propertyName)
    {
        if (propertyInfos.ContainsKey(propertyName))
        {
            return propertyInfos[propertyName];
        }

        var propInfo = typeof(T).GetProperty(propertyName);
        if (propInfo == null)
        {
            throw new ArgumentNullException();
        }

        propertyInfos.Add(propertyName, propInfo);
        return propInfo;
    }
}

