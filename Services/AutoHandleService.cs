using System;
using TicketManagement.DBContext;

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
                var context = scope.ServiceProvider.GetRequiredService<TicketDbContext>();

                var ticketsToHandle = context.Tickets
                    .Where(t => t.Status == "New" && t.CreationDateTime <= DateTime.UtcNow.AddMinutes(-60))
                    .ToList();

                foreach (var ticket in ticketsToHandle)
                {
                    ticket.Status = "Handled";
                }

                await context.SaveChangesAsync(stoppingToken);
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }

}
