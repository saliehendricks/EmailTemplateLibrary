using System;
using ServiceStack.OrmLite;
using EmailTemplateLibrary.Storage.Base;

namespace EmailTemplateLibrary.Storage.Sql
{
    public class SqlTemplateStorage : SqlDbTemplateStorage
    {
        public SqlTemplateStorage(SqlStorageOptions options) : base(options.ConnectionString, SqlServerDialect.Provider)
        {}
    }

}
