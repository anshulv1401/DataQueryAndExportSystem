using DataQueryAndExportSystem.Models;
using static DataQueryAndExportSystem.Enums.DataServiceEnums;

namespace DataQueryAndExportSystem.Services.ExportServices
{
    public interface IExportService
    {
        Task<List<ExportFileInfo>> Export(string query);
    }
}
