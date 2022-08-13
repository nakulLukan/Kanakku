using Kanakku.Application.Contracts.Storage;
using Kanakku.UI.Impl.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanakku.UI;

public static class ServiceRegistry
{
    public static void Register(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IAppSecureStorage, AppSecureStorage>();
    }
}
