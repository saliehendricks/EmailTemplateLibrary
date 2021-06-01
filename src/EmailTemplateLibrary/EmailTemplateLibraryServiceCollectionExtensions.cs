using EmailTemplateLibrary.Dashboard;
using EmailTemplateLibrary.Storage;
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
            this IServiceCollection services, TemplateDashboardOptions options)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            TemplateStorage.Current = new FileTemplateStorage(); //set default
            services.TryAddSingleton(_ => {
                if (options.LoadBaseTemplates && !TemplateStorage.IsBaseTemplatesLoaded)
                {
                    TemplateStorage.Current.CreateBaseTemplates();
                    TemplateStorage.IsBaseTemplatesLoaded = true;
                }
                return TemplateStorage.Current; 
            });
            services.TryAddSingleton(_ => DashboardRoutes.Routes);
            services.TryAddTransient(_ => options);
            return services;
        }
    }
}
