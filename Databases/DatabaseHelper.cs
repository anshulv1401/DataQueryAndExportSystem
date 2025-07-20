using DataQueryAndExportSystem.Models;
using DuckDB.NET.Data;
using System.Collections.Generic;
using System.Data;
using System.Text;
using static DataQueryAndExportSystem.Enums.DataServiceEnums;
using static DuckDB.NET.Native.NativeMethods;

namespace DataQueryAndExportSystem.Databases;

public class DatabaseHelper
{
    public Dictionary<DatabaseType, IDatabaseAdapter> DatabaseAdapters { get; set; } = [];
    private readonly ILogger<DatabaseHelper> _logger;
    public DatabaseHelper(ILogger<DatabaseHelper> logger, [FromKeyedServices(DatabaseType.DuckDb)] IDatabaseAdapter database)
    {
        DatabaseAdapters.Add(DatabaseType.DuckDb, database);
        _logger = logger;
    }


    public async Task<IList<IList<KeyValuePair<string, dynamic?>>>> FetchData(string query, int? numberOfPages = null)
    {
        var connectionDetails = new ConnectionDetails
        {
            Server = "sales.duckdb",
            Database = ""
        };

        return await DatabaseAdapters[DatabaseType.DuckDb].FetchData(connectionDetails, query, numberOfPages * 100);
    }
}