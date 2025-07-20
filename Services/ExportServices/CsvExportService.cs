using DataQueryAndExportSystem.Databases;
using DataQueryAndExportSystem.Models;
using GemBox.Spreadsheet;
using System.Text;
using static DataQueryAndExportSystem.Enums.DataServiceEnums;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            var fileName = $"export_{DateTime.UtcNow:yyyyMMdd_HHmmss}";
            var data = await _databaseService.FetchData(query);

            if (data != null && data.Count > 0)
            {
                var workbook = new ExcelFile();
                var sheet = workbook.Worksheets.Add(fileName);

                PopulateExcelSheetWithData(sheet, data);

                var savedFileName = fileName + ".csv";
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
                    sheet.Cells[row + 2, column + 1].Value = data[row][column].Value;
                }
            }
        }

        private string GenerateCsvContent(ExcelFile workbook, string fileType)
        {
            var ms = new MemoryStream();

            if (string.Compare(fileType, "CSV-Windows-UTF-8", true) == 0)
            {
                _logger.LogInformation("File Generating for CSV-Windows with Encoding UTF-8");
                workbook.Save(ms, new CsvSaveOptions(CsvType.CommaDelimited) { QuoteMode = CsvQuoteMode.Always });
            }
            else if (string.Compare(fileType, "CSV-Mac-UTF-8", true) == 0)
            {
                _logger.LogInformation("File Generating for CSV-Mac with Encoding UTF-8");
                workbook.Save(ms, new CsvSaveOptions(CsvType.CommaDelimited) { QuoteMode = CsvQuoteMode.Always });
            }
            else if (string.Compare(fileType, "CSV-Unix-UTF-8", true) == 0)
            {
                _logger.LogInformation("File Generating for CSV-Unix with Encoding UTF-8");
                workbook.Save(ms, new CsvSaveOptions(CsvType.CommaDelimited) { QuoteMode = CsvQuoteMode.Always });
            }
            else if (string.Compare(fileType, "CSV-Windows-ANSI", true) == 0)
            {
                _logger.LogInformation("File Generating for CSV-Windows with Encoding ANSI");
                workbook.Save(ms, new CsvSaveOptions(CsvType.CommaDelimited) { QuoteMode = CsvQuoteMode.Always, Encoding = Encoding.GetEncoding(1252) });
            }
            else if (string.Compare(fileType, "CSV-Mac-ANSI", true) == 0)
            {
                _logger.LogInformation("File Generating for CSV-Mac with Encoding ANSI");
                workbook.Save(ms, new CsvSaveOptions(CsvType.CommaDelimited) { QuoteMode = CsvQuoteMode.Always, Encoding = Encoding.GetEncoding(1252) });
            }
            else if (string.Compare(fileType, "CSV-Unix-ANSI", true) == 0)
            {
                _logger.LogInformation("File Generating for CSV-Unix with Encoding ANSI");
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                workbook.Save(ms, new CsvSaveOptions(CsvType.CommaDelimited) { Encoding = Encoding.GetEncoding(1252) });
            }

            ms.Position = 0;
            var finalContent = new StreamReader(ms).ReadToEnd();

            if (fileType.Contains("Mac", StringComparison.OrdinalIgnoreCase))
                finalContent = finalContent.Replace("\n", "");
            else if (fileType.Contains("Unix", StringComparison.OrdinalIgnoreCase))
                finalContent = finalContent.Replace("\r", "");

            return finalContent;
        }
    }
}
