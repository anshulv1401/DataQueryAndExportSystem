
using DataQueryAndExportSystem.Databases;
using DataQueryAndExportSystem.Models;
using DataQueryAndExportSystem.Services;
using DataQueryAndExportSystem.Services.ExportServices;
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

            builder.Configuration.AddEnvironmentVariables();
            var applicationConfig = builder.Configuration.Get<ApplicationConfiguration>();
            builder.Services.AddSingleton(applicationConfig ?? throw new InvalidOperationException());

            // TODO logging configuration
            builder.Services.AddSingleton(Log.Logger);
            
            builder.Services.AddSingleton<IDataService, DataService>();
            builder.Services.AddKeyedSingleton<IDatabaseAdapter, DuckDbDatabaseAdapter>(DatabaseType.DuckDb);
            builder.Services.AddSingleton<DatabaseHelper>();
            builder.Services.AddKeyedSingleton<IExportService, CsvExportService>(ExportFormat.CSV);
            builder.Services.AddKeyedSingleton<IExportService, ExcelExportService>(ExportFormat.XLSX);
            builder.Services.AddKeyedSingleton<IExportService, PdfExportService>(ExportFormat.PDF);
            builder.Services.AddKeyedSingleton<IExportService, JsonExportService>(ExportFormat.JSON);
            builder.Services.AddSingleton<ExportHelper>();

            GemBox.Spreadsheet.SpreadsheetInfo.SetLicense("FREE‑LIMITED‑KEY");
            //GemBox.Pdf.SpreadsheetInfo.SetLicense("FREE‑LIMITED‑KEY");

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
