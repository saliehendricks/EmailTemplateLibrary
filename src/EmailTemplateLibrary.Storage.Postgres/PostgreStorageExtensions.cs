using System;
using Microsoft.Extensions.DependencyInjection;
namespace EmailTemplateLibrary.Storage.Postgres
{

    public static class PostgreStorageExtensions
    {
        public static IServiceCollection AddPostgresStorage(
            this IServiceCollection services, PostgreStorageOptions options)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            TemplateStorage.Current = new PostgreTemplateStorage(options);
            return services;
        }
    }
}
