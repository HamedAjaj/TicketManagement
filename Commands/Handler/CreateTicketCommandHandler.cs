using MediatR;
using System.Net.Sockets;
using System;
using TicketManagement.Commands.Model;
using TicketManagement.DBContext;
using TicketManagement.Entities;
using TicketManagement.PipeLineBehavior.Response;

namespace TicketManagement.Commands.Handler
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, ResponseWrapper<object>>

    {
        private readonly TicketDbContext _context;
        public CreateTicketCommandHandler(TicketDbContext context) => _context = context;

        public async Task<ResponseWrapper<object>> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            try
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
                return new ResponseWrapper<object>
                {
                    Success = true,
                    Data = ticket.Id

                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<object>
                {
                    Success = false,
                    Errors = new List<string> { "An error occurred while creating the ticket.", ex.Message }
                };
            }
        }
    }

}
