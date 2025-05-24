using MediatR;
using System;
using TicketManagement.Application.Commands.Model;
using TicketManagement.Infrastructure.DBContext;

namespace TicketManagement.Application.Commands.Handler
{
    public class HandleTicketCommandHandler : IRequestHandler<HandleTicketCommand,Unit>
    {
        private readonly TicketDbContext _context;

        public HandleTicketCommandHandler(TicketDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(HandleTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _context.Tickets.FindAsync(request.TicketId);

            if (ticket == null)
                throw new Exception("Ticket not found");

            ticket.UpdateStatus("Handled");
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
