using EmailTemplateLibrary.Storage;
using System;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using Microsoft.AspNetCore.Antiforgery;
using System.Text;

namespace EmailTemplateLibrary.Dashboard
{
    public class DashboardContext 
    {
        public TemplateStorage Storage { get; set; }
        public DashboardOptions Options { get; set; }
        public Match UriMatch { get; set; }
        public DashboardRequest Request { get; set; }
        public DashboardResponse Response { get; set; }
        public string AntiforgeryHeader { get; set; }
        public string AntiforgeryToken { get; set; }
    }

    public class AspNetCoreDashboardContext : DashboardContext 
    {
        public HttpContext HttpContext { get; }

        public AspNetCoreDashboardContext(TemplateStorage storage, DashboardOptions options, HttpContext httpContext)
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

    internal sealed class AspNetCoreDashboardRequest : DashboardRequest
    {
        private readonly HttpContext _context;

        public AspNetCoreDashboardRequest(HttpContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            _context = context;
        }

        public override string Method => _context.Request.Method;
        public override string Path => _context.Request.Path.Value;
        public override string PathBase => _context.Request.PathBase.Value;
        public override string LocalIpAddress => _context.Connection.LocalIpAddress.ToString();
        public override string RemoteIpAddress => _context.Connection.RemoteIpAddress.ToString();
        public override string GetQuery(string key) => _context.Request.Query[key];
        public override string GetBody() 
        {
            string body = "";
            _context.Request.EnableBuffering();
            using (var reader = new StreamReader(_context.Request.Body, Encoding.UTF8, false, 1024, true))
            {
                body = reader.ReadToEnd();
                _context.Request.Body.Seek(0, SeekOrigin.Begin);
            }
            return body;
        }

        public override async Task<IList<string>> GetFormValuesAsync(string key)
        {
            var form = await _context.Request.ReadFormAsync();
            return form[key];
        }        
    }

    internal sealed class AspNetCoreDashboardResponse : DashboardResponse
    {
        private readonly HttpContext _context;

        public AspNetCoreDashboardResponse(HttpContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            _context = context;
        }

        public override string ContentType
        {
            get { return _context.Response.ContentType; }
            set { _context.Response.ContentType = value; }
        }

        public override int StatusCode
        {
            get { return _context.Response.StatusCode; }
            set { _context.Response.StatusCode = value; }
        }

        public override Stream Body => _context.Response.Body;

        public override Task WriteAsync(string text)
        {
            return _context.Response.WriteAsync(text);
        }

        public override void SetExpire(DateTimeOffset? value)
        {
            _context.Response.Headers["Expires"] = value?.ToString("r", CultureInfo.InvariantCulture);
        }
    }
}
