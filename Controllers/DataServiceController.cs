using DataQueryAndExportSystem.Models;
using DataQueryAndExportSystem.Services;
using Microsoft.AspNetCore.Mvc;
using static DataQueryAndExportSystem.Enums.DataServiceEnums;

namespace DataQueryAndExportSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataServiceController : ControllerBase
    {
        private readonly ILogger<DataServiceController> _logger;
        private readonly IDataService _dataService;

        public DataServiceController(ILogger<DataServiceController> logger, IDataService dataService)
        {
            _logger = logger;
            _dataService = dataService;
        }

        [HttpGet(template: nameof(Query), Name = "Query")]
        public async Task<IList<IList<KeyValuePair<string, dynamic?>>>> Query([FromQuery] string query, [FromQuery] int pageNumber)
        {
            return await _dataService.FetchData(query, pageNumber);
        }

        [HttpPost(template: nameof(Export), Name = "export")]
        public async Task<ExportStatus> Export([FromQuery] ExportFormat exportFormat, [FromQuery] string query)
        {
            return await _dataService.QueueExport(exportFormat, query);
        }

        [HttpGet(template: nameof(Export), Name = "export-status")]
        public async Task<ExportStatus> Export([FromRoute] string jobId)
        {
            return await _dataService.GetExportStatus(jobId);
        }
    }
}
