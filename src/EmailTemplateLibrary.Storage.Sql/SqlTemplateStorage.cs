using System;
using EmailTemplateLibrary.Model;
using ServiceStack.OrmLite;
using System.Collections.Generic;
using System.Linq;
using EmailTemplateLibrary.Storage.Base;

namespace EmailTemplateLibrary.Storage.Sql
{
    public class SqlTemplateStorage : SqlDbTemplateStorage
    {
        public SqlTemplateStorage(SqlStorageOptions options) : base(options.ConnectionString, SqlServerDialect.Provider)
        {}
    }

}
