using DataQueryAndExportSystem.Models;
using DuckDB.NET.Data;
using System.Collections.Generic;
using System.Data;
using System.Text;
using static DataQueryAndExportSystem.Enums.DataServiceEnums;
using static DuckDB.NET.Native.NativeMethods;

namespace DataQueryAndExportSystem.Databases;

public class DatabaseService
{
    public Dictionary<DatabaseType, IDatabaseAdapter> DatabaseAdapters { get; set; } = [];
    private readonly ILogger<DatabaseService> _logger;
    public DatabaseService(ILogger<DatabaseService> logger, [FromKeyedServices(DatabaseType.DuckDb)] IDatabaseAdapter database)
    {
        DatabaseAdapters.Add(DatabaseType.DuckDb, database);
        _logger = logger;
    }


    public async Task<IList<IList<KeyValuePair<string, dynamic?>>>> FetchData(string query, int numberOfPages)
    {
        var connectionDetails = new ConnectionDetails
        {
            Server = "sales.duckdb",
            Database = ""
        };

        return await DatabaseAdapters[DatabaseType.DuckDb].FetchData(connectionDetails, query, numberOfPages * 100);
    }
}