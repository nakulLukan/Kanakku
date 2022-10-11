using Kanakku.Shared.Models.ExportService;

namespace Kanakku.Application.Contracts.Essential;

public interface IExportService
{
    string GenerateAndSavePdf<T>(PrintConfig<T> config,
                                 string directory, 
                                 string fileName);
    
}
