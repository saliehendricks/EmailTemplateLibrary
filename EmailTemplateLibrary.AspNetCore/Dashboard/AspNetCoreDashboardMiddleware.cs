using EmailTemplateLibrary.Dashboard;
using EmailTemplateLibrary.Storage;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplateLibrary.AspNetCore.Dashboard
{
    public class AspNetCoreDashboardMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly DashboardOptions _options;
        private readonly RouteCollection _routes;
        private readonly TemplateStorage _storage;

        public AspNetCoreDashboardMiddleware(
            RequestDelegate next,
            TemplateStorage storage,
            DashboardOptions options,
            RouteCollection routes)
        {
            if (next == null) throw new ArgumentNullException(nameof(next));
            if (storage == null) throw new ArgumentNullException(nameof(storage));
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (routes == null) throw new ArgumentNullException(nameof(routes));

            _next = next;
            _storage = storage;
            _options = options;
            _routes = routes;

            if (_options.LoadBaseTemplates)
            {
                if (_storage != null)
                {
                    _storage.CreateBaseTemplates();
                }
            }
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var context = new AspNetCoreDashboardContext(_storage, _options, httpContext);
            var findResult = _routes.FindDispatcher(httpContext.Request.Path.Value);

            if (findResult == null)
            {
                return;
            }

            if (_options.Authorization != null)
            {
                foreach (var filter in _options.Authorization)
                {
                    if (!filter.Authorize(context))
                    {
                        var isAuthenticated = httpContext.User?.Identity?.IsAuthenticated;

                        httpContext.Response.StatusCode = isAuthenticated == true
                            ? (int)HttpStatusCode.Forbidden
                            : (int)HttpStatusCode.Unauthorized;

                        return;
                    }
                }
            }

            if (!_options.IgnoreAntiforgeryToken)
            {
                var antiforgery = httpContext.RequestServices.GetService<IAntiforgery>();

                if (antiforgery != null)
                {
                    var requestValid = await antiforgery.IsRequestValidAsync(httpContext);

                    if (!requestValid)
                    {
                        // Invalid or missing CSRF token
                        httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        return;
                    }
                }
            }

            context.UriMatch = findResult.Item2;

            await findResult.Item1.Dispatch(context);

        }
    }
}

