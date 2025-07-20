namespace DataQueryAndExportSystem.Enums
{
    public class DataServiceEnums
    {
        public enum ExportFormat
        {
            CSV,
            XLSX,
            PDF,
            JSON
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
