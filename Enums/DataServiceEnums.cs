namespace DataQueryAndExportSystem.Enums
{
    public class DataServiceEnums
    {
        public enum ExportFormat
        {
            JSON,
            CSV,
            PDF,
            XLSX
        }

        public enum JobStatus
        {
            Queued,
            InProgress,
            Completed,
            Failed,
            Canceled
        }

        public enum DatabaseType
        {
            DuckDb
        }
    }
}
