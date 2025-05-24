using System;
using TicketManagement.Infrastructure.DBContext;
using TicketManagement.Infrastructure.Repository.Interface;

namespace TicketManagement.Services
{
    public class AutoHandleService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public AutoHandleService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var ticketRepository = scope.ServiceProvider.GetRequiredService<ITicketRepository>();

                var cutoffTime = DateTime.UtcNow.AddMinutes(-60);
                var ticketsToHandle = ticketRepository.GetTicketsToHandle(cutoffTime);

                foreach (var ticket in ticketsToHandle)
                {
                    ticket.UpdateStatus("Handled");
                }

                await ticketRepository.SaveChangesAsync(stoppingToken);

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }


}
