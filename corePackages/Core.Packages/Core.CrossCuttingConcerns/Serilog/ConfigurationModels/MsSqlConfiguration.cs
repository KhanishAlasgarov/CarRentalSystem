namespace Core.CrossCuttingConcerns.Serilog.ConfigurationModels;

public class MsSqlConfiguration
{
    public MsSqlConfiguration(string connectionString, string tableName, bool autoCreateSqlTable)
    {
        ConnectionString = connectionString;
        TableName = tableName;
        AutoCreateSqlTable = autoCreateSqlTable;
    }

    public MsSqlConfiguration()
    {
        ConnectionString = String.Empty;
        TableName = String.Empty;
    }

    public string ConnectionString { get; set; }
    public string TableName { get; set; }
    public bool AutoCreateSqlTable { get; set; }
}