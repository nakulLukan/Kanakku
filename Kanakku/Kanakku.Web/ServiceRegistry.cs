using AutoMapper;
using Blazored.LocalStorage;
using Coravel;
using FluentValidation;
using Kanakku.Application.Contracts.Presentation;
using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.DailyOperation;
using Kanakku.Application.Models.User;
using Kanakku.Application.Requests.DailyOperation;
using Kanakku.Application.Requests.User;
using Kanakku.Domain.User;
using Kanakku.Infrastructure.Persistence;
using Kanakku.UI.Contracts.Event;
using Kanakku.UI.Contracts.StaticApi;
using Kanakku.UI.Impl.Event;
using Kanakku.UI.Impl.Presentation;
using Kanakku.UI.Impl.Storage;
using Kanakku.Web.HostedServices;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MudBlazor.Services;
using Refit;

namespace Kanakku.UI;

public static class ServiceRegistry
{
    public static void Register(this IServiceCollection serviceCollection,
        Microsoft.Extensions.Configuration.IConfigurationRoot configuration)
    {
        serviceCollection.AddTransient<IToastService, ToastService>();
        serviceCollection.AddScoped<IAppSecureStorage, AppSecureStorage>();
        serviceCollection.AddScoped<ISessionContext, SessionContext>();
        serviceCollection.AddTransient<IAppMediator, AppMediator>();
        serviceCollection.AddMediatR(typeof(Application.ServiceRegistry).Assembly);
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        serviceCollection.AddValidatorsFromAssembly(typeof(Application.ServiceRegistry).Assembly);
        serviceCollection.AddBlazoredLocalStorage();
        serviceCollection.AddMudServices();
        serviceCollection.AddScheduler();
        serviceCollection.AddAuthentication("cookie")
            .AddCookie("cookie", opt =>
            {
                opt.Cookie.Name = "kanakku";
                opt.ExpireTimeSpan = TimeSpan.FromHours(10);
                opt.LoginPath = "/login";
            });
        var autoMapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<EmployeeDto, CreateEmployeeCommand>();
            cfg.CreateMap<EmployeeDto, EditEmployeeCommand>();
            cfg.CreateMap<DailyOperationDto, SubmitDailyOperationCommand>();
            cfg.CreateMap<DailyOperationDetailDto, EditDailyOperationDetailCommand>();
            cfg.CreateMap<EmployeeRegistryEntryDto, EmployeeRegistryEntryCommand>();
            cfg.CreateMap<DailyOperationFilterDto, DailyOperationsQuery>();
            cfg.CreateMap<DailyOperationFilterDto, DailyOperationsExportCommand>();
            cfg.CreateMap<EmployeeRegistryFilterDto, EmployeeRegistryExportCommand>();
            cfg.CreateMap<EmployeeRegistryFilterDto, EmployeeRegistryQuery>();
        });

        serviceCollection.AddSingleton(autoMapperConfiguration.CreateMapper());

        serviceCollection.AddDefaultIdentity<AppUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.User.RequireUniqueEmail = false;
        })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        serviceCollection
            .AddRefitClient<IGeneral>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["StaticApi:BaseUrl"]));

        serviceCollection.AddTransient<DbBackupJob>();
    }
}
