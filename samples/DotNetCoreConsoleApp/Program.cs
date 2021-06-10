using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using EmailTemplateLibrary;
using EmailTemplateLibrary.Storage.Postgres;
using EmailTemplateLibrary.Storage;

namespace DotNetCoreConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
			string dir = Directory.GetCurrentDirectory();
			if (!File.Exists($"{dir}/appsettings.json"))
				dir = AppDomain.CurrentDomain.BaseDirectory;

			IConfigurationRoot configuration = new ConfigurationBuilder()
				.SetBasePath(dir)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddEnvironmentVariables()
				.Build();

			string connString = configuration.GetConnectionString("MainConnection");

			var services = new ServiceCollection();
			services
				.AddEmailTemplateLibraryServices(new EmailTemplateLibrary.Dashboard.TemplateDashboardOptions())
				.AddPostgresStorage(new PostgreStorageOptions
				{
					ConnectionString = connString
				});
			
			var serviceProvider = services.BuildServiceProvider();
			
			//Above is the setup/config
			//Below we test that we can access our templates
			var templateStore = serviceProvider.GetService<TemplateStorage>();
			
			templateStore.CreateBaseTemplates();
			
			var templates = templateStore.GetTemplates();
            foreach (var t in templates)
            {
				Console.WriteLine($"Template found: {t.TemplateKey}");
				Console.WriteLine($"Template found: {t.TemplateText}");
			}

		}
	}
}
