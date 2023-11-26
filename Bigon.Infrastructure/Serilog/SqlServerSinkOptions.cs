using Serilog.Sinks.MSSqlServer;

namespace Bigon.Infrastructure.Serilog
{
    public class SqlServerSinkOptions : MSSqlServerSinkOptions
    {
        public SqlServerSinkOptions()
        {
            AutoCreateSqlTable = true;
            SchemaName = "Serilog";
            TableName = "Logs";
        }
    }
}
