namespace DataQueryAndExportSystem.Models
{
    public class ExportFileInfo
    {
        public required string FullPath { get; set; }
        public required string FileName { get; set; }
        public required long Size { get; set; }
    }
}
