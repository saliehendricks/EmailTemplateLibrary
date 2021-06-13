using System;
using ServiceStack.OrmLite;
using EmailTemplateLibrary.Storage.Base;
using ServiceStack.OrmLite.Sqlite;

namespace EmailTemplateLibrary.Storage.Sql
{
    public class SqlTemplateStorage : SqlDbTemplateStorage
    {
        public SqlTemplateStorage(SqlStorageOptions options) : base(options.ConnectionString, options.ConnectionString == ":memory:" ? SqliteOrmLiteDialectProvider.Instance : SqlServerDialect.Provider)
        {}
    }

}
