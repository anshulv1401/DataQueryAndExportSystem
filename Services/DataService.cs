using DataQueryAndExportSystem.Databases;
using DataQueryAndExportSystem.Enums;
using DataQueryAndExportSystem.Models;
using static DataQueryAndExportSystem.Enums.DataServiceEnums;

namespace DataQueryAndExportSystem.Services
{
    public class DataService : IDataService
    {
        private readonly DatabaseService _databaseService;
        private readonly ILogger<DataService> _logger;
        public DataService(ILogger<DataService> logger, DatabaseService databaseService)
        {
            _databaseService = databaseService;
            _logger = logger;
        }


        public async Task<ExportStatus> QueueExport(string query, DataServiceEnums.ExportFormat format)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<IList<KeyValuePair<string, dynamic?>>>> FetchData(string query, int numberOfPages)
        {
            return await _databaseService.FetchData(query, numberOfPages);
        }

        public async Task<ExportStatus> GetExportStatus(string jobId)
        {
            throw new NotImplementedException();
        }
    }
}
