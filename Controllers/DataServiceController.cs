using DataQueryAndExportSystem.Models;
using DataQueryAndExportSystem.Services;
using Microsoft.AspNetCore.Mvc;
using static DataQueryAndExportSystem.Enums.DataServiceEnums;

namespace DataQueryAndExportSystem.Controllers
{
    [ApiController]
    [Route("[controller]/v1")]
    public class DataServiceController : ControllerBase
    {
        private readonly ILogger<DataServiceController> _logger;
        private readonly IDataService _dataService;

        public DataServiceController(ILogger<DataServiceController> logger, IDataService dataService)
        {
            _logger = logger;
            _dataService = dataService;
        }

        [HttpGet(Name = "query")]
        public IList<IList<IDictionary<string, dynamic>>> Query([FromQuery] string query, [FromQuery] int pageNumber)
        {
            return _dataService.FetchData(query, pageNumber);
        }

        [HttpGet(Name = "export")]
        public ExportStatus Export([FromQuery] string query, [FromQuery] ExportFormat exportFormat)
        {
            return _dataService.QueueExport(query, exportFormat);
        }

        [HttpGet(Name = "export")]
        public ExportStatus Export([FromRoute] string jobId)
        {
            return _dataService.GetExportStatus(jobId);
        }
    }
}
