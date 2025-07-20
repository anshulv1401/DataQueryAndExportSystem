using DataQueryAndExportSystem.Models;
using static DataQueryAndExportSystem.Enums.DataServiceEnums;

namespace DataQueryAndExportSystem.Services
{
    public interface IDataService
    {
        Task<IList<IList<KeyValuePair<string, dynamic?>>>> FetchData(string query, int pageNumber);
        Task<ExportStatus> QueueExport(string query, ExportFormat format);
        Task<ExportStatus> GetExportStatus(string jobId);
    }
}
