using Coravel;
using Kanakku.Domain.User;
using Kanakku.Infrastructure;
using Kanakku.Infrastructure.Persistence;
using Kanakku.Shared;
using Kanakku.UI;
using Kanakku.Web.HostedServices;
using Kanakku.Web.Impl.Authentication;
using Kanakku.Web.Middlewares;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add Logger
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
        .WriteTo.File($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/Kanakku/logs/log-.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();
Log.Logger.Information("Booting application");
var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
#if RELEASE
                .AddJsonFile($"appsettings.release.json", optional: true)
#endif
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
builder.Configuration.AddConfiguration(configuration);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.Register(builder.Configuration);
builder.Services.RegisterInfrastructure();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<AppUser>>();
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000);
    //options.ListenAnyIP(5001, listenOptions => listenOptions.UseHttps()); // HTTPS

});
var app = builder
    .Build();
Log.Logger.Information("Db mirgation started");
// Run db migration
using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
await dbContext.Database.MigrateAsync();
Log.Logger.Information("Db mirgation comleted");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseStaticFiles();

if (!Directory.Exists(DirectoryConstant.EXPORT_DIRECTORY_PATH))
{
    Directory.CreateDirectory(DirectoryConstant.EXPORT_DIRECTORY_PATH);
    Log.Information("Static directory '{Directory}' created.", DirectoryConstant.EXPORT_DIRECTORY_PATH);
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(DirectoryConstant.EXPORT_DIRECTORY_PATH),
    RequestPath = "/static/exports"
});
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<AuthenticationMiddleware>();
app.MapControllers()
    .RequireAuthorization();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Services.UseScheduler(scheduler =>
{
    scheduler.Schedule<DbBackupJob>()
    .Daily()
    .RunOnceAtStart();
});
app.Run();
