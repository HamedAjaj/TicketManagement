using MediatR;
using System.Net.Sockets;
using System;
using TicketManagement.Commands.Model;
using TicketManagement.DBContext;
using TicketManagement.Entities;

namespace TicketManagement.Commands.Handler
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, Guid>
    {
        private readonly TicketDbContext _context;
        public CreateTicketCommandHandler(TicketDbContext context) => _context = context;

        public async Task<Guid> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = new Ticket
            {
                PhoneNumber = request.PhoneNumber,
                Governorate = request.Governorate,
                City = request.City,
                District = request.District
            };
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync(cancellationToken);
            return ticket.Id;
        }
    }

}
