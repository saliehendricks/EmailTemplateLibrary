using EmailTemplateLibrary.Dashboard;
using EmailTemplateLibrary.Storage;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EmailTemplateLibrary.AspNetCore.Dashboard
{
    public class AspNetCoreDashboardContext : DashboardContext
    {
        public HttpContext HttpContext { get; }

        public AspNetCoreDashboardContext(TemplateStorage storage, TemplateDashboardOptions options, HttpContext httpContext)
        {
            if (storage == null) throw new ArgumentNullException(nameof(storage));
            if (options == null) throw new ArgumentNullException(nameof(options));

            Storage = storage;
            Options = options;

            HttpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
            Request = new AspNetCoreDashboardRequest(httpContext);
            Response = new AspNetCoreDashboardResponse(httpContext);

            if (!options.IgnoreAntiforgeryToken)
            {
                var antiforgery = HttpContext.RequestServices.GetService<IAntiforgery>();
                var tokenSet = antiforgery?.GetAndStoreTokens(HttpContext);

                if (tokenSet != null)
                {
                    AntiforgeryHeader = tokenSet.HeaderName;
                    AntiforgeryToken = tokenSet.RequestToken;
                }
            }
        }
    }
}
