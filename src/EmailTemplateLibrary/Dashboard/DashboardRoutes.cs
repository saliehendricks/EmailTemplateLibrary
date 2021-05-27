using EmailTemplateLibrary.Dashboard.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EmailTemplateLibrary.Dashboard
{
    public static class DashboardRoutes
    {
        public static RouteCollection Routes { get; }
        static DashboardRoutes()
        {
            Routes = new RouteCollection();
            Routes.AddRazorPage("/", x => new HomePage());
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
    }
}
