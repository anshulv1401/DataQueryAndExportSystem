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

        [HttpGet(template:nameof(Query), Name = "Query")]
        public IList<IList<KeyValuePair<string, dynamic?>>> Query([FromQuery] string query, [FromQuery] int pageNumber)
        {
            return _dataService.FetchData(query, pageNumber);
        }

        [HttpPost(template:nameof(Export), Name= "export")]
        public ExportStatus Export([FromQuery] string query, [FromQuery] ExportFormat exportFormat)
        {
            return _dataService.QueueExport(query, exportFormat);
        }

        [HttpGet(template: nameof(Export), Name = "export-status")]
        public ExportStatus Export([FromRoute] string jobId)
        {
            return _dataService.GetExportStatus(jobId);
        }
    }
}
