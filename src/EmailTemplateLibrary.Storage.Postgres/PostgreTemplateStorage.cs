using EmailTemplateLibrary.Storage.Base;
using ServiceStack.OrmLite;

namespace EmailTemplateLibrary.Storage.Postgres
{
    public class PostgreTemplateStorage : SqlDbTemplateStorage
    {
        public PostgreTemplateStorage(PostgreStorageOptions options) : base(options.ConnectionString, PostgreSqlDialect.Provider)
        {}
    }
}
