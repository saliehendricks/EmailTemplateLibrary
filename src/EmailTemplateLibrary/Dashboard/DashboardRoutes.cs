using EmailTemplateLibrary.Dashboard.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Text.Json.Serialization;
using EmailTemplateLibrary.Model;
using System.Reflection;
using System.Threading.Tasks;

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
            //Routes.AddCommand("/savetemplate", async context => {
            //    await SaveTemplateHandler(context);
            //    return true;
            //});
            Routes.AddCommand("/savetemplate", context => {
                var result = Task.FromResult(SaveTemplateHandler(context));
                return true;
            });
            Routes.Add("/js[0-9]+", new CombinedResourceDispatcher(
                "application/javascript",
                GetExecutingAssembly(),
                GetContentFolderNamespace("js"),
                Javascripts));
        }

        private static async Task<bool> SaveTemplateHandler(DashboardContext context) 
        {
            string body = await context.Request.GetBodyAsync();
            Template model = JsonSerializer.Deserialize<Template>(body, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true});
            context.Storage.SaveTemplate(model.TemplateKey, model.TemplateText);
            return true;
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
