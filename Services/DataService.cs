using DataQueryAndExportSystem.Databases;
using DataQueryAndExportSystem.Enums;
using DataQueryAndExportSystem.Models;
using DataQueryAndExportSystem.Services.ExportServices;
using static DataQueryAndExportSystem.Enums.DataServiceEnums;

namespace DataQueryAndExportSystem.Services
{
    public class DataService : IDataService
    {
        private readonly DatabaseHelper _databaseHelper;
        private readonly ExportHelper _exportHelper;
        private readonly ILogger<DataService> _logger;
        public DataService(ILogger<DataService> logger, DatabaseHelper databaseHelper, 
            ExportHelper exportHelper)
        {
            _databaseHelper = databaseHelper;
            _exportHelper = exportHelper;
            _logger = logger;
        }


        public async Task<ExportStatus> QueueExport(ExportFormat format, string query)
        {
            await _exportHelper.ExportData(format, query);

            return new ExportStatus
            {
                Status = JobStatus.Queued,
                JobId = Guid.NewGuid().ToString()
            };
        }

        public async Task<IList<IList<KeyValuePair<string, dynamic?>>>> FetchData(string query, int numberOfPages)
        {
            return await _databaseHelper.FetchData(query, numberOfPages);
        }

        public async Task<ExportStatus> GetExportStatus(string jobId)
        {
            throw new NotImplementedException();
        }
    }
}
