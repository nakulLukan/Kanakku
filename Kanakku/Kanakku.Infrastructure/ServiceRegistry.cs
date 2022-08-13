using Kanakku.Application.Contracts.Storage;
using Kanakku.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kanakku.Infrastructure;

public static class ServiceRegistry
{
    public static void RegisterInfrastructure(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<AppDbContext>();
        serviceCollection.AddTransient<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
    }
}
