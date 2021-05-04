using aircraft_maintenance_api.Abstractions;
using aircraft_maintenance_api.Classes;
using aircraft_maintenance_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aircraft_maintenance_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AircraftsController : ControllerBase
    {
        private readonly AircraftsService _aircraftsService;

        private readonly ILogger<AircraftsController> _logger;

        public AircraftsController(ILogger<AircraftsController> logger, AircraftsService aircraftsService)
        {
            _logger = logger;
            _aircraftsService = aircraftsService;
        }


        [HttpPost]
        [Route("{aircraftId}/duelist")]
        public Tuple<int, IEnumerable<AircraftTask>> CalculateNextDueDate([FromRoute] int aircraftId, [FromBody]Payload payload)
        {
            return _aircraftsService.CalculateNextDueDate(aircraftId, payload.Tasks);
        }
    }
}
