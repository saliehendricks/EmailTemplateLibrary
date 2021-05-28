using EmailTemplateLibrary.Dashboard.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Text.Json.Serialization;
using EmailTemplateLibrary.Model;
using System.Reflection;

namespace EmailTemplateLibrary.Dashboard
{
    public static class DashboardRoutes
    {
        private static readonly string[] Javascripts =
        {
            "templatelibrary.js"
        };
        public static RouteCollection Routes { get; }
        static DashboardRoutes()
        {
            Routes = new RouteCollection();
            Routes.AddRazorPage("/", x => new HomePage());
            Routes.AddCommand("/savetemplate",
                context =>
                {
                    string body = context.Request.GetBody();
                    Template model = JsonSerializer.Deserialize<Template>(body);
                    context.Storage.SaveTemplate(model.TemplateKey, model.TemplateText);

                    //var client = context.GetBackgroundJobClient();
                    //return client.ChangeState(context.UriMatch.Groups["JobId"].Value, CreateDeletedState());
                    return true;                    
                });
            Routes.Add("/js[0-9]+", new CombinedResourceDispatcher(
                "application/javascript",
                GetExecutingAssembly(),
                GetContentFolderNamespace("js"),
                Javascripts));
        }

        public static void AddRazorPage(
            this RouteCollection routes,
            string pathTemplate,
            Func<Match, RazorPage> pageFunc)
        {
            if (routes == null) throw new ArgumentNullException(nameof(routes));
            if (pathTemplate == null) throw new ArgumentNullException(nameof(pathTemplate));
            if (pageFunc == null) throw new ArgumentNullException(nameof(pageFunc));

            routes.Add(pathTemplate, new RazorPageDispatcher(pageFunc));
        }

        private static Assembly GetExecutingAssembly()
        {
            return typeof(DashboardRoutes).GetTypeInfo().Assembly;
        }

        internal static string GetContentFolderNamespace(string contentFolder)
        {
            return $"{typeof(DashboardRoutes).Namespace}.Content.{contentFolder}";
        }
        internal static string GetContentResourceName(string contentFolder, string resourceName)
        {
            return $"{GetContentFolderNamespace(contentFolder)}.{resourceName}";
        }
    }
}
