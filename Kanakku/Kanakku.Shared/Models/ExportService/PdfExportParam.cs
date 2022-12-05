namespace Kanakku.Shared.Models.ExportService;

public class ColumnMetaData
{
    public string PropertyName { get; set; }
    public string DisplayName { get; set; }
    public float? MinimumLength { get; set; }
}

public class FooterMetaData
{
    public string FooterText { get; set; }
    public string FooterTextValue { get; set; }
    public int FooterFontSize { get; set; } = 15;
}

public class DisplayPictureConfig
{
    public string DpPath { get; set; }
    public float RelativeSpaceRequired { get; set; } = 0.5f;
}

public class PrintConfig<T>
{
    public string Title { get; set; }
    public string SubTitle { get; set; }
    public List<ColumnMetaData> ColumnMetaData { get; set; }
    public FooterMetaData FooterMetaData { get; set; }
    public bool ShowSerialNumber { get; set; }
    public List<T> Data { get; set; }
    public DisplayPictureConfig DisplayPictureConfig { get; set; }
    public int TableFontSize { get; set; } = 10;
}