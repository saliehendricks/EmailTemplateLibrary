
// This file is part of Hangfire.
#if NETCOREAPP3_0

using Hangfire.Annotations;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using EmailTemplateLibrary.Dashboard;

namespace EmailTemplateLibrary
{
    public static class EmailTemplateLibraryEndpointRouteBuilderExtensions
    {
        public static IEndpointConventionBuilder MapEmailTemplateDashboard(
            this IEndpointRouteBuilder endpoints,
            string pattern = "/managetemplates",
            TemplateStorage storage = null, 
            DashboardOptions options = null)
        {
            if (endpoints == null) throw new ArgumentNullException(nameof(endpoints));
            if (pattern == null) throw new ArgumentNullException(nameof(pattern));

            var app = endpoints.CreateApplicationBuilder();
            if (app == null) throw new ArgumentNullException(nameof(app));

            var services = app.ApplicationServices;

            storage = storage ?? services.GetRequiredService<JobStorage>();
            options = options ?? services.GetService<DashboardOptions>() ?? new DashboardOptions();
            var routes = app.ApplicationServices.GetRequiredService<RouteCollection>();

            var pipeline = app
                .UsePathBase(pattern)
                .UseMiddleware<AspNetCoreDashboardMiddleware>(storage, options, routes)
                .Build();

            return endpoints.Map(pattern + "/{**path}", pipeline);
        }
    }
}

#endif