
using DataQueryAndExportSystem.Databases;
using DataQueryAndExportSystem.Services;
using Microsoft.OpenApi.Models;
using Serilog;
using static DataQueryAndExportSystem.Enums.DataServiceEnums;

namespace DataQueryAndExportSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            // TODO logging configuration
            builder.Services.AddSingleton(Log.Logger);
            builder.Services.AddSingleton<DatabaseService>();
            builder.Services.AddSingleton<IDataService, DataService>();
            builder.Services.AddKeyedSingleton<IDatabaseAdapter, DuckDbDatabaseAdapter>(DatabaseType.DuckDb);
            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
