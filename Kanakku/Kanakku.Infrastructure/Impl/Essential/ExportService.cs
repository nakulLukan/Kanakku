using Kanakku.Application.Contracts.Essential;
using Kanakku.Shared.Models.ExportService;
using QuestPDF.Fluent;

namespace Kanakku.Infrastructure.Impl.Essential;

public class ExportService : IExportService
{
    public string GenerateAndSavePdf<T>(PrintConfig<T> config,
                                 string directory, string fileName)
    {
        var document = new PdfTemplate<T>(config);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        QuestPDF.Settings.DocumentLayoutExceptionThreshold = 10000;
        QuestPDF.Settings.CheckIfAllTextGlyphsAreAvailable = false;
        string path = Path.Combine(directory, fileName);
        document.GeneratePdf(path);
        return path;
    }
}
