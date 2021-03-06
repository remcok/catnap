using System;
using System.Data;

namespace Catnap.Database.Sqlite
{
    public abstract class BaseSqliteAdapter : BaseDbAdapter
    {
        private readonly SqliteValueConverter typeConverter = new SqliteValueConverter();

        protected BaseSqliteAdapter(Type connectionType) : base(connectionType) { }

        protected BaseSqliteAdapter(string connectionTypeAssemblyName) : base(ResolveConnectionType(connectionTypeAssemblyName)) { }

        public override object ConvertToDb(object value)
        {
            return typeConverter.ConvertToDb(value);
        }

        public override object ConvertFromDb(object value, Type toType)
        {
            return typeConverter.ConvertFromDb(value, toType);
        }

        public override string GetGeneralStringType()
        {
            return "varchar";
        }

        public override IDbCommand CreateLastInsertIdCommand(string tableName, IDbCommandFactory commandFactory)
        {
            return commandFactory.Create(null, "SELECT last_insert_rowid()");
        }

        public override IDbCommand CreateGetTableMetadataCommand(string tableName, IDbCommandFactory commandFactory)
        {
            var parameters = new[] { new Parameter("tableName", tableName) };
            var sql = string.Format("select * from sqlite_master where tbl_name = {0}tableName", parameterPrefix);
            return commandFactory.Create(parameters, sql);
        }
    }
}