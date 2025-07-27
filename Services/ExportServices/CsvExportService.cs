using DataQueryAndExportSystem.Databases;
using DataQueryAndExportSystem.Models;
using GemBox.Spreadsheet;

namespace DataQueryAndExportSystem.Services.ExportServices
{
    public class CsvExportService : IExportService
    {
        private readonly DatabaseHelper _databaseService;
        private readonly ILogger<CsvExportService> _logger;
        private readonly ApplicationConfiguration _applicationConfiguration;
        public CsvExportService(ILogger<CsvExportService> logger, DatabaseHelper databaseService, ApplicationConfiguration applicationConfiguration)
        {
            _databaseService = databaseService;
            _logger = logger;
            _applicationConfiguration = applicationConfiguration;
        }

        public async Task<List<ExportFileInfo>> Export(string query)
        {
            List<ExportFileInfo> files = [];
            string isoTimestamp = DateTime.UtcNow.ToString("o");  // e.g., 2025-07-27T06:00:00.0000000Z

            var fileName = $"export_{DateTime.UtcNow:yyyyMMdd_HHmmssfff}";
            var data = await _databaseService.FetchData(query);

            if (data != null && data.Count > 0)
            {
                var workbook = new ExcelFile();
                var sheet = workbook.Worksheets.Add(fileName);

                PopulateExcelSheetWithData(sheet, data);

                var savedFileName = fileName + ".csv";
                Directory.CreateDirectory(_applicationConfiguration.ExportFolderPath);
                var finalPath = Path.Combine(_applicationConfiguration.ExportFolderPath, savedFileName);

                _logger.LogInformation("File generating for CSV");

                string finalContent = GenerateCsvContent(workbook, "CSV-Windows-UTF-8");

                await File.WriteAllTextAsync(finalPath, finalContent);


                files.Add(new ExportFileInfo
                {
                    FileName = savedFileName,
                    FullPath = finalPath,
                    Size = new FileInfo(finalPath).Length
                });
            }

            return files;
        }

        private static void PopulateExcelSheetWithData(ExcelWorksheet sheet, IList<IList<KeyValuePair<string, dynamic?>>> data)
        {
            for (int row = 0; row < data.Count; row++)
            {
                if (row == 0)
                {
                    for (int column = 0; column < data[0].Count; column++)
                    {
                        sheet.Cells[1, column + 1].Value = data[row][column].Key;
                    }
                }

                for (int column = 0; column < data[row].Count; column++)
                {
                    var value = data[row][column].Value;

                    // Convert DateOnly to DateTime (GemBox supports DateTime)
                    if (value is DateOnly dateOnly)
                    {
                        sheet.Cells[row + 2, column + 1].Value = dateOnly.ToDateTime(TimeOnly.MinValue);
                    }
                    else
                    {
                        sheet.Cells[row + 2, column + 1].Value = value;
                    }
                }
            }
        }

        private string GenerateCsvContent(ExcelFile workbook, string fileType)
        {
            var ms = new MemoryStream();

            workbook.Save(ms, new CsvSaveOptions(CsvType.CommaDelimited) { QuoteMode = CsvQuoteMode.Always });
            ms.Position = 0;

            var finalContent = new StreamReader(ms).ReadToEnd();

            return finalContent;
        }
    }
}
