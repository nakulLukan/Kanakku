﻿using AutoMapper;
using FluentValidation;
using Kanakku.Application.Contracts.Essential;
using Kanakku.Application.Contracts.Presentation;
using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.DailyOperation;
using Kanakku.Application.Models.User;
using Kanakku.Application.Requests.DailyOperation;
using Kanakku.Application.Requests.User;
using Kanakku.App.Contracts.Event;
using Kanakku.App.Contracts.StaticApi;
using Kanakku.App.Impl.Essential;
using Kanakku.App.Impl.Event;
using Kanakku.App.Impl.Presentation;
using Kanakku.App.Impl.Storage;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Kanakku.App;

public static class ServiceRegistry
{
    public static void Register(this IServiceCollection serviceCollection,
        Microsoft.Extensions.Configuration.IConfigurationRoot configuration)
    {
        serviceCollection.AddTransient<IToastService, ToastService>();
        serviceCollection.AddSingleton<IAppSecureStorage, AppSecureStorage>();
        serviceCollection.AddSingleton<ISessionContext, SessionContext>();
        serviceCollection.AddSingleton<IPermissionService, PermissionService>();
        serviceCollection.AddTransient<IAppMediator, AppMediator>();
        serviceCollection.AddMediatR(cfg=> cfg.RegisterServicesFromAssembly(typeof(Application.ServiceRegistry).Assembly));
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        serviceCollection.AddValidatorsFromAssembly(typeof(Application.ServiceRegistry).Assembly);

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

        serviceCollection
            .AddRefitClient<IGeneral>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["StaticApi:BaseUrl"]));
    }
}
