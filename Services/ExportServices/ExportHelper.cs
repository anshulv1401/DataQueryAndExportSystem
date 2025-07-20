using DataQueryAndExportSystem.Databases;
using DataQueryAndExportSystem.Models;
using DuckDB.NET.Data;
using System.Collections.Generic;
using System.Data;
using System.Text;
using static DataQueryAndExportSystem.Enums.DataServiceEnums;
using static DuckDB.NET.Native.NativeMethods;

namespace DataQueryAndExportSystem.Services.ExportServices;

public class ExportHelper
{
    public Dictionary<ExportFormat, IExportService> exportServices { get; set; } = [];
    private readonly ILogger<DatabaseHelper> _logger;
    public ExportHelper(ILogger<DatabaseHelper> logger, 
        [FromKeyedServices(ExportFormat.CSV)] IExportService csvExportService,
        [FromKeyedServices(ExportFormat.XLSX)] IExportService xlExportService,
        [FromKeyedServices(ExportFormat.PDF)] IExportService pdfExportService,
        [FromKeyedServices(ExportFormat.JSON)] IExportService jsonExportService)
    {
        _logger = logger;
        exportServices.Add(ExportFormat.CSV, csvExportService);
        exportServices.Add(ExportFormat.XLSX, xlExportService);
        exportServices.Add(ExportFormat.PDF, pdfExportService);
        exportServices.Add(ExportFormat.JSON, jsonExportService);
    }


    public async Task<IList<ExportFileInfo>> ExportData(ExportFormat exportFormat, string query)
    {
        return await exportServices[exportFormat].Export(query);
    }
}