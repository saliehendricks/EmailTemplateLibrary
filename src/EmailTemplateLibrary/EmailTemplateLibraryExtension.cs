
// This file is part of Hangfire.
// Copyright © 2021-2022 Salie Hendricks @saliehendricks github/.comsaliehendricks.
// 
// EmailTemplateLibraryExtension is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as 
// published by the Free Software Foundation, either version 3 
// of the License, or any later version.
// 
// EmailTemplateLibraryExtension is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public 
// License along with Hangfire. If not, see <http://www.gnu.org/licenses/>.

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
#if NETCOREAPP3_0
using Microsoft.Extensions.Hosting;
#else
using Microsoft.AspNetCore.Hosting;
#endif
using EmailTemplateLibrary.Storage;
using EmailTemplateLibrary.Dashboard;

namespace EmailTemplateLibrary
{
    public static class EmailTemplateLibraryExtension
    {
        /// <summary>
        /// Adds Dashboard UI middleware to the request processing pipeline under 
        /// the <c>/managetemplates</c> path.
        /// </summary>
        /// <param name="builder">OWIN application builder.</param>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is null.</exception>
        /// 
        /// <remarks>
        /// Please see <see cref="AppBuilderExtensions"/> for details and examples.
        /// </remarks>
        public static IApplicationBuilder UseEmailTemplateLibrary(this IApplicationBuilder builder, 
            string pathMatch = "/templates",
            TemplateStorage storage = null)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (pathMatch == null) throw new ArgumentNullException(nameof(pathMatch));

            var services = builder.ApplicationServices;
            var routes = builder.ApplicationServices.GetRequiredService<RouteCollection>();
            
            storage = storage == null ? new FileTemplateStorage() : storage;
            var options = builder.ApplicationServices.GetRequiredService<DashboardOptions>();
            builder.Map(new PathString(pathMatch), x => x.UseMiddleware<AspNetCoreDashboardMiddleware>(storage, options, routes));
            return builder;
        }

        public static string RegistrationActivationEmailTemplate()
        {
            string html = $@"<h4>Hi {{firstname}}, Welcome to HAPI</h4><p>Your account is under review and will be activated shortly.</p>
<p>Feel free to familiarize yourself with the documentation and samples in the meant time.</p><br/>";
            return html;
        }
    }
}
