using aircraft_maintenance_api.Abstractions;
using aircraft_maintenance_api.Classes;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace aircraft_maintenance_worker.Classes
{
    public class CalculateNextDueTopic
    {
        public int AircraftId { get; set; }

        public Payload Payload { get; set; }
    }

    public class CalculateNextDueResult
    {
        public int AircraftId { get; set; }

        public IEnumerable<AircraftTask> Tasks { get; set; }
    }

    public class CalculateNextDueConsumer : IConsumer<CalculateNextDueTopic>
    {
        readonly IAircraftsService _aircraftsService;

        public CalculateNextDueConsumer(IAircraftsService aircraftsService)
        {
            _aircraftsService = aircraftsService;
        }

        public async Task Consume(ConsumeContext<CalculateNextDueTopic> context)
        {
            var result = _aircraftsService.CalculateNextDueDate(context.Message.AircraftId, context.Message.Payload.Tasks);

            await context.RespondAsync<CalculateNextDueResult>(new
            {
                AircraftId = result.Item1,
                Tasks = result.Item2
            }); 
        }
    }
}
