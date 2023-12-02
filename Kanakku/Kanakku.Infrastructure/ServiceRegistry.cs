using Kanakku.Application.Contracts.Essential;
using Kanakku.Application.Contracts.Storage;
using Kanakku.Infrastructure.Impl.Essential;
using Kanakku.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using QuestPDF.Infrastructure;

namespace Kanakku.Infrastructure;

public static class ServiceRegistry
{
    public static void RegisterInfra(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<AppDbContext>(ServiceLifetime.Transient);
        serviceCollection.AddTransient<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());

        serviceCollection.AddTransient<IExportService, ExportService>();

        // Initialise quest pdf
        QuestPDF.Settings.License = LicenseType.Community;
    }
}
