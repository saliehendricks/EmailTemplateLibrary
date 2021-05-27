using EmailTemplateLibrary.Storage;
using System;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace EmailTemplateLibrary.Dashboard
{
    public class DashboardContext 
    {
        public TemplateStorage Storage { get; set; }
        public DashboardOptions Options { get; set; }

        //private readonly Lazy<bool> _isReadOnlyLazy;
        public Match UriMatch { get; set; }

        public DashboardRequest Request { get; set; }
        public DashboardResponse Response { get; set; }

        // public bool IsReadOnly => _isReadOnlyLazy.Value;
        public string AntiforgeryHeader { get; set; }
        public string AntiforgeryToken { get; set; }
    }

    public class AspNetCoreDashboardContext : DashboardContext 
    {
        public AspNetCoreDashboardContext(TemplateStorage storage, DashboardOptions options, HttpContext httpContext)
        {
            if (storage == null) throw new ArgumentNullException(nameof(storage));
            if (options == null) throw new ArgumentNullException(nameof(options));

            Storage = storage;
            Options = options;
            //_isReadOnlyLazy = new Lazy<bool>(() => options.IsReadOnlyFunc(this));
        }       
    }
}
