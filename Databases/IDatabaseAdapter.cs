
namespace DataQueryAndExportSystem.Databases
{
    public interface IDatabaseAdapter
    {
        Task<List<List<KeyValuePair<string, dynamic?>>>> FetchData(ConnectionDetails sqlConnection, string queryString, int? limit = null);

        public string GetConnectionString(ConnectionDetails connection);
    }
}