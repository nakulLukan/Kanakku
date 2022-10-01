﻿using AutoMapper;
using FluentValidation;
using Kanakku.Application.Contracts.Essential;
using Kanakku.Application.Contracts.Presentation;
using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.DailyOperation;
using Kanakku.Application.Models.User;
using Kanakku.Application.Requests.DailyOperation;
using Kanakku.Application.Requests.User;
using Kanakku.UI.Contracts.Event;
using Kanakku.UI.Impl.Essential;
using Kanakku.UI.Impl.Event;
using Kanakku.UI.Impl.Presentation;
using Kanakku.UI.Impl.Storage;
using MediatR;

namespace Kanakku.UI;

public static class ServiceRegistry
{
    public static void Register(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IToastService, ToastService>();
        serviceCollection.AddSingleton<IAppSecureStorage, AppSecureStorage>();
        serviceCollection.AddSingleton<ISessionContext, SessionContext>();
        serviceCollection.AddSingleton<IPermissionService, PermissionService>();
        serviceCollection.AddTransient<IAppMediator, AppMediator>();
        serviceCollection.AddMediatR(typeof(Application.ServiceRegistry).Assembly);
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        serviceCollection.AddValidatorsFromAssembly(typeof(Application.ServiceRegistry).Assembly);

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<EmployeeDto, CreateEmployeeCommand>();
            cfg.CreateMap<EmployeeDto, EditEmployeeCommand>();
            cfg.CreateMap<DailyOperationDto, SubmitDailyOperationCommand>();
        });

        serviceCollection.AddSingleton(configuration.CreateMapper());
    }
}