using Aircraft.Maintenance.Core.Classes;
using Aircraft.Maintenance.Core.Contracts;
using Aircraft.Maintenance.Core.Services;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aircraft.Maintenance.Core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AircraftsController : ControllerBase
    {
        readonly IRequestClient<CalculateMaintenanceDueRequest> _requestClient;

        private readonly ILogger<AircraftsController> _logger;

        public AircraftsController(ILogger<AircraftsController> logger, IRequestClient<CalculateMaintenanceDueRequest> requestClient)
        {
            _logger = logger;
            _requestClient = requestClient;
        }


        [HttpPost]
        [Route("{aircraftId}/duelist")]
        public async Task<Tuple<int, IEnumerable<AircraftTask>>> CalculateNextDueDate([FromRoute] int aircraftId, [FromBody]Payload payload)
        {
            //_aircraftsService.SetDate(new DateTime(2018, 06, 19, 0, 0, 0));
            //return _aircraftsService.CalculateNextDueDate(aircraftId, payload.Tasks);
            var result = await _requestClient.GetResponse<CalculateMaintenanceDueResponse>(new { aircraftId, payload });
            return new Tuple<int, IEnumerable<AircraftTask>> (result.Message.AircraftId, result.Message.Tasks);
        }
    }
}
