namespace DataQueryAndExportSystem.Databases
{
    public class ConnectionDetails
    {
        public required string Server { get; set; }
        public required string Type { get; set; }
        public required string Database { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public long? Port { get; set; }
        public string? Table { get; set; }
        public string? Schema { get; set; }
    }
}
