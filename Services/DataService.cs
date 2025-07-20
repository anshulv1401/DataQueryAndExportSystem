using DataQueryAndExportSystem.Databases;
using DataQueryAndExportSystem.Enums;
using DataQueryAndExportSystem.Models;
using static DataQueryAndExportSystem.Enums.DataServiceEnums;

namespace DataQueryAndExportSystem.Services
{
    public class DataService : IDataService
    {
        private readonly IDatabaseAdapter _database;
        public DataService([FromKeyedServices(DatabaseType.DuckDb)] IDatabaseAdapter database)
        {
            _database = database;
        }

        public ExportStatus QueueExport(string query, DataServiceEnums.ExportFormat format)
        {
            throw new NotImplementedException();
        }

        public IList<IList<KeyValuePair<string, dynamic?>>> FetchData(string query, int pageNumber)
        {
            var connectionDetails = new ConnectionDetails
            {
                Server = "sales.duckdb",
                Database = ""
            };

            return _database.FetchData(connectionDetails, query, pageNumber * 100).GetAwaiter().GetResult();
        }

        public ExportStatus GetExportStatus(string jobId)
        {
            throw new NotImplementedException();
        }
    }
}
