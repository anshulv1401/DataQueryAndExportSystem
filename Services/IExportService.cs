using DataQueryAndExportSystem.Models;
using static DataQueryAndExportSystem.Enums.DataServiceEnums;

namespace DataQueryAndExportSystem.Services
{
    public interface IExportService
    {
        Task<List<ExportFileInfo>> Export(string query, int pageNumber);
    }
}
