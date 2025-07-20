using DataQueryAndExportSystem.Enums;
using DataQueryAndExportSystem.Models;

namespace DataQueryAndExportSystem.Services
{
    public class DataService : IDataService
    {
        public ExportStatus QueueExport(string query, DataServiceEnums.ExportFormat format)
        {
            throw new NotImplementedException();
        }

        public IList<IList<KeyValuePair<string, dynamic?>>> FetchData(string query, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public ExportStatus GetExportStatus(string jobId)
        {
            throw new NotImplementedException();
        }
    }
}
