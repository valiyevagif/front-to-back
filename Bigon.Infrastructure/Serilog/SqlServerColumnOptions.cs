using Serilog.Sinks.MSSqlServer;
using System.Data;

namespace Bigon.Infrastructure.Serilog
{
    public class SqlServerColumnOptions : ColumnOptions
    {
        public SqlServerColumnOptions()
        {
            AdditionalColumns = new[]
                {
                    new SqlColumn("SourceContext", SqlDbType.VarChar),
                    new SqlColumn("ActionName", SqlDbType.VarChar),
                    new SqlColumn("MachineName", SqlDbType.NVarChar, dataLength: 200),
                    new SqlColumn("ApplicationName", SqlDbType.NVarChar, dataLength: 200),
                    new SqlColumn("RequestPath", SqlDbType.VarChar),
                    new SqlColumn("StatusCode", SqlDbType.Int),
                    new SqlColumn("ContentType", SqlDbType.VarChar, dataLength: 100),
                    new SqlColumn("ContentLength", SqlDbType.Int),
                    new SqlColumn("Protocol", SqlDbType.VarChar, dataLength: 100),
                    new SqlColumn("Method", SqlDbType.VarChar, dataLength: 30),
                    new SqlColumn("Scheme", SqlDbType.VarChar, dataLength: 30),
                    new SqlColumn("Host", SqlDbType.VarChar, dataLength: 200),
                    new SqlColumn("PathBase", SqlDbType.VarChar),
                    new SqlColumn("Path", SqlDbType.VarChar),
                    new SqlColumn("QueryString", SqlDbType.VarChar),
                    new SqlColumn("UserId", SqlDbType.Int),
                    new SqlColumn("RequestIp", SqlDbType.VarChar)
                };

            Store = new[] {
                    StandardColumn.Id,
                    StandardColumn.Message,
                    StandardColumn.Level,
                    StandardColumn.TimeStamp,
                    StandardColumn.Exception,
                };
        }
    }
}
