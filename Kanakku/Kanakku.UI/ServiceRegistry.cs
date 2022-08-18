using FluentValidation;
using Kanakku.Application.Contracts.Storage;
using Kanakku.UI.Contracts.Event;
using Kanakku.UI.Impl.Event;
using Kanakku.UI.Impl.Storage;
using MediatR;

namespace Kanakku.UI;

public static class ServiceRegistry
{
    public static void Register(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IAppSecureStorage, AppSecureStorage>();
        serviceCollection.AddTransient<IAppMediator, AppMediator>();
        serviceCollection.AddMediatR(typeof(Application.ServiceRegistry).Assembly);
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        serviceCollection.AddValidatorsFromAssembly(typeof(Application.ServiceRegistry).Assembly);
    }
}
