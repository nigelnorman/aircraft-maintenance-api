using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace CalculateMaintenanceDueQueue
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new HostBuilder().ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<MaintenanceDueQueueService>();
            });

            await builder.RunConsoleAsync();
        }
    }
}
