using DataQueryAndExportSystem.Databases;
using DataQueryAndExportSystem.Models;
using DataQueryAndExportSystem.Services.ExportServices;
using Hangfire;
using Hangfire.Server;
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
            var exportJobId = Guid.NewGuid().ToString();
            BackgroundJob.Enqueue(() => QueueExportDataJob(format, query, exportJobId, null));

            return new ExportStatus
            {
                Status = JobStatus.Queued,
                JobId = exportJobId
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

        public async Task QueueExportDataJob(ExportFormat format, string query, string ExportJobId, PerformContext backgroundJobContext = null)
        {
            await _exportHelper.ExportData(format, query);
        }
    }
}
