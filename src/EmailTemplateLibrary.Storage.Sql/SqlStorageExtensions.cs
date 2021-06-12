using System;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack.OrmLite.SqlServer;

namespace EmailTemplateLibrary.Storage.Sql
{
    public static class SqlStorageExtensions
    {
        public static IServiceCollection AddSqlServerStorage(
            this IServiceCollection services, SqlStorageOptions options)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            TemplateStorage.Current = new SqlTemplateStorage(options);
            return services;
        }
    }

}
