using Kanakku.Infrastructure;
using Kanakku.Infrastructure.Persistence;
using Kanakku.UI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MudBlazor.Services;
using Serilog;

namespace Kanakku.UI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        Log.Logger = new LoggerConfiguration()
        .WriteTo.File($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/Kanakku/logs/log-.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();
        Log.Logger.Information("Booting application");
        try
        {
            var builder = MauiApp.CreateBuilder();
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
#if RELEASE
                .AddJsonFile($"appsettings.release.json", optional: true)
#endif
                .Build();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });
            builder.Configuration.AddConfiguration(configuration);
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddMudServices();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();

#endif
            builder.Services.AddSingleton<WeatherForecastService>();

            builder.Services.Register();
            builder.Services.RegisterInfrastructure();

            Task.Run(() =>
            {
                try
                {
                    Log.Logger.Information("Db mirgation started");
                    // Run db migration
                    var dbContext = new AppDbContext(builder.Configuration);
                    dbContext.Database.Migrate();
                    dbContext.Dispose();
                    Log.Logger.Information("Db mirgation comleted");
                }
                catch(Exception ex)
                {
                    Log.Logger.Information("Db mirgation failed.\nMessage: {message}\nStack: {stack}", ex.Message, ex.StackTrace);
                }
            });

            return builder.Build();
        }
        catch (Exception ex)
        {
            Log.Logger.Information("Failed to boot application");
            Log.Logger.Error("Message: {message}, Stack: {stack}", ex.Message, ex.StackTrace);
            throw;
        }
    }
}
