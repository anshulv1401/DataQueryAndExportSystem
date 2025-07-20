using DataQueryAndExportSystem.Models;
using static DataQueryAndExportSystem.Enums.DataServiceEnums;

namespace DataQueryAndExportSystem.Services
{
    public interface IDataService
    {
        IList<IList<IDictionary<string, dynamic>>> FetchData(string query, int pageNumber);
        ExportStatus QueueExport(string query, ExportFormat format);
        ExportStatus GetExportStatus(string jobId);
    }
}
