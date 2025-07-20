using static DataQueryAndExportSystem.Enums.DataServiceEnums;

namespace DataQueryAndExportSystem.Models
{
    public class ExportStatus
    {
        public required string JobId { set; get; }
        public required JobStatus Status { set; get; }
    }
}
