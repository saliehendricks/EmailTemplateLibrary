using System;
using EmailTemplateLibrary.Dashboard;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EmailTemplateLibrary.Storage.Mongo
{
    public class MongoStorageOptions 
    {
        public string UrlConnection { get; set; }        
    }

    public static class EmailTemplateLibraryServiceCollectionMongoStorageExtensions
    {
        public static IServiceCollection AddMongoStorage(
            this IServiceCollection services, MongoStorageOptions options)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            TemplateStorage.Current = new MongoTemplateStorage(options);
            return services;
        }
    }
}
