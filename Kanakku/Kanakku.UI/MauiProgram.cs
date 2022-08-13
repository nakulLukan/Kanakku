using Kanakku.Infrastructure;
using Kanakku.Infrastructure.Persistence;
using Kanakku.UI.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Kanakku.UI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });
        builder.Configuration.AddConfiguration(configuration);
        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();

#endif
        builder.Services.AddMediatR(typeof(Kanakku.Application.ServiceRegistry).Assembly);
        builder.Services.AddSingleton<WeatherForecastService>();

        builder.Services.Register();
        builder.Services.RegisterInfrastructure();

        // Run db migration
        var dbContext = new AppDbContext(builder.Configuration);
        dbContext.Database.Migrate();
        dbContext.Dispose();
        return builder.Build();
    }
}
