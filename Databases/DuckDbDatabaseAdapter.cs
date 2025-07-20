using DataQueryAndExportSystem.Models;
using DuckDB.NET.Data;
using System.Data;
using System.Text;
using static DataQueryAndExportSystem.Enums.DataServiceEnums;

namespace DataQueryAndExportSystem.Databases;

public class DuckDbDatabaseAdapter(ILogger<DuckDbDatabaseAdapter> logger) : IDatabaseAdapter
{
    public DatabaseType DatabaseType { get { return DatabaseType.DuckDb; } }

    public async Task<List<List<KeyValuePair<string, dynamic?>>>> FetchData(ConnectionDetails connectionDetails, string queryString, int? limit = null)
    {
        await using var connection = new DuckDBConnection(GetConnectionString(connectionDetails));
        try
        {
            logger.LogInformation("Opening Connection");
            await connection.OpenAsync();
            logger.LogInformation("Connection Opened");

            using var command = connection.CreateCommand();
            command.CommandText = queryString;

            var results = new List<List<KeyValuePair<string, dynamic?>>>();
            var dataTypes = new List<KeyValuePair<string, string>>();

            using var reader = await command.ExecuteReaderAsync();
            logger.LogInformation("Executing query {@query}", queryString);
            while (await reader.ReadAsync())
            {
                var row = new List<KeyValuePair<string, dynamic?>>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row.Add(new KeyValuePair<string, dynamic?>(reader.GetName(i), reader.IsDBNull(i) ? null : reader.GetValue(i)));
                }
                results.Add(row);

                if (limit.HasValue && limit-- <= 0)
                    break;
            }

            return (results);
        }
        catch (Exception ex)
        {
            logger.LogError("Error occurred: {@Error}", ex.Message);
            throw;
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                await connection.CloseAsync();
                logger.LogInformation("Connection Closed\n");
            }
        }
    }

    public string GetConnectionString(ConnectionDetails connection)
    {
        return $"DataSource={connection.Server}AccessMode=ReadWriteCreate;";
    }
}