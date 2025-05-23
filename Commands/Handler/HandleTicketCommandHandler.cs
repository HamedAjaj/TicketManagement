using MediatR;
using System;
using TicketManagement.Commands.Model;
using TicketManagement.DBContext;

namespace TicketManagement.Commands.Handler
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

            ticket.Status = "Handled";
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
