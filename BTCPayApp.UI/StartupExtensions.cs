﻿using Fluxor;
using Microsoft.Extensions.DependencyInjection;
namespace BTCPayApp.UI;

public static class StartupExtensions
{
    public static IServiceCollection AddBTCPayAppUIServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddFluxor(options =>
        {
            options.UseRouting();
            options.ScanAssemblies(typeof(App).Assembly);
#if DEBUG
            options.UseReduxDevTools();
#endif
            options.AddMiddleware<StateMiddleware>();
        });
        return serviceCollection;
    }
}
