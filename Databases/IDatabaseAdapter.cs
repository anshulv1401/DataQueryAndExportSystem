
namespace DataQueryAndExportSystem.Databases
{
    public interface IDatabaseAdapter
    {
        Task<(List<List<KeyValuePair<string, dynamic?>>>, List<KeyValuePair<string, string>>)> FetchData(ConnectionDetails sqlConnection, string queryString, int? limit = null);
    }
}