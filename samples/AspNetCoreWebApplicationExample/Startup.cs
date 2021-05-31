using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailTemplateLibrary;
using EmailTemplateLibrary.Dashboard;
using EmailTemplateLibrary.AspNetCore;
using EmailTemplateLibrary.Storage.Mongo;

namespace AspNetCoreWebApplicationExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddEmailTemplateLibraryServices(new DashboardOptions()
            {
                IgnoreAntiforgeryToken = true,
                LoadBaseTemplates = true
            })
            .AddMongoStorage(new MongoStorageOptions() { 
                UrlConnection = "mongodb://safarinow-cosmos-mongo:RYxTyO6ceduURZ3brB2IcIxHqrqPCu6HEpyPHji8pSrXy0TJSxkrmxWoR9IbeRJClorKKp1LkzQJCq8oei3oyA==@safarinow-cosmos-mongo.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@safarinow-cosmos-mongo@&retrywrites=false"
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            app.UseEmailTemplateLibrary();
        }
    }
}
