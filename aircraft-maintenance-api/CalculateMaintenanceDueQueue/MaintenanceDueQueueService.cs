using Aircraft.Maintenance.Core.Abstractions;
using Aircraft.Maintenance.Core.Contracts;
using Aircraft.Maintenance.Core.Services;
using MassTransit;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CalculateMaintenanceDueQueue
{
    public class MaintenanceDueQueueService : BackgroundService
    {
        readonly IBusControl _bus;
        readonly IAircraftsService _aircraftsService;

        public MaintenanceDueQueueService()
        {
            _aircraftsService = new AircraftsService();
            _bus = Bus.Factory.CreateUsingRabbitMq(cfg => 
            {
                cfg.Host(new Uri("rabbitmq://localhost/"), h => { });

                cfg.ReceiveEndpoint("aircrafts-service", e =>
                {
                    e.Handler<CalculateMaintenanceDueRequest>(context =>
                    {
                        return context.RespondAsync<CalculateMaintenanceDueResponse>(_aircraftsService.CalculateNextDueDate(context.Message.AircraftId, context.Message.Payload.Tasks));
                    });
                });
            });
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return _bus.StartAsync(stoppingToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.WhenAll(base.StopAsync(cancellationToken), _bus.StopAsync(cancellationToken));
        }
    }
}
