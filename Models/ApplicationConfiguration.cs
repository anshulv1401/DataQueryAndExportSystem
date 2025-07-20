namespace DataQueryAndExportSystem.Models
{
    public class ApplicationConfiguration
    {
        public required LoggingConfiguration Logging { get; set; }
        public required string ConnectionStrings  { get; set; }
    }
}
