using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace Core.CrossCuttingConcerns.Serilog.Loggers;

public class MsSqlLogger : LoggerServiceBase
{
    private readonly IConfiguration _configuration;
    public MsSqlLogger(IConfiguration configuration)
    {
        _configuration = configuration;

        var tableName = _configuration["SeriLogConfigurations/MsSqlConfiguration/TableName"] ?? "Logs";
        var autoCreateSqlTable = bool.Parse(_configuration["SeriLogConfigurations/MsSqlConfiguration/AutoCreateSqlTable"]
            ??
            "true");

        var connectionString = _configuration["SeriLogConfigurations/MsSqlConfiguration/ConnectionString"] ??
            "data source=SEATTLE;initial catalog=RentACarDb;Trusted_Connection=True;TrustServerCertificate=True;";

        MSSqlServerSinkOptions mSSqlServerSinkOptions = new()
        {
            TableName = tableName,
            AutoCreateSqlTable = autoCreateSqlTable,
        };

        Logger = new LoggerConfiguration()
            .WriteTo.MSSqlServer(connectionString, sinkOptions: mSSqlServerSinkOptions).CreateLogger();
    }
}
