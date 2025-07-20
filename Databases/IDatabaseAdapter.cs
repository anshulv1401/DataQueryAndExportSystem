
namespace DataQueryAndExportSystem.Databases
{
    public interface IDatabaseAdapter
    {
        Task<IList<IList<KeyValuePair<string, dynamic?>>>> FetchData(ConnectionDetails sqlConnection, string query, int? limit = null);

        public string GetConnectionString(ConnectionDetails connection);
    }
}