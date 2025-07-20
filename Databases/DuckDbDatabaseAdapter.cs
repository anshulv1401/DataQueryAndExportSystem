using DataQueryAndExportSystem.Models;
using System.Data;
using System.Text;
using static DataQueryAndExportSystem.Enums.DataServiceEnums;

namespace DataQueryAndExportSystem.Databases;

public class DuckDbDatabaseAdapter(ILogger<DuckDbDatabaseAdapter> logger, ApplicationConfiguration configuration) : IDatabaseAdapter
{
    public DatabaseType DatabaseType { get { return DatabaseType.DuckDb; } }

    public override async Task<(List<List<KeyValuePair<string, dynamic?>>>, List<KeyValuePair<string, string>>)> FetchData(ConnectionDetails sqlConnection, string queryString, int? limit = null)
    {
        throw new NotImplementedException("DuckDB support is not implemented yet.");
    }
}