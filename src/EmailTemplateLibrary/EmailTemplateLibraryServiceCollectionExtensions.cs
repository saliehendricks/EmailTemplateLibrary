using EmailTemplateLibrary.Dashboard;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailTemplateLibrary
{
    public static class EmailTemplateLibraryServiceCollectionExtensions
    {
        public static IServiceCollection AddEmailTemplateLibraryServices(
            this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            //services.TryAddSingletonChecked(_ => TemplateStorage.Current);

            services.TryAddSingleton(_ => DashboardRoutes.Routes);
            
            //services.AddSingleton<IGlobalConfiguration>(serviceProvider =>
            //{
            //    var configurationInstance = GlobalConfiguration.Configuration;

            //    // init defaults for log provider and job activator
            //    // they may be overwritten by the configuration callback later

            //    var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            //    if (loggerFactory != null)
            //    {
            //        configurationInstance.UseLogProvider(new AspNetCoreLogProvider(loggerFactory));
            //    }
                
            //    return configurationInstance;
            //});

            return services;
        }
    }
}
