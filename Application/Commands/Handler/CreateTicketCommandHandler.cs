using MediatR;
using System.Net.Sockets;
using System; 
using TicketManagement.Application.Commands.Model;
using TicketManagement.Infrastructure.DBContext;
using TicketManagement.API.PipeLineBehavior.Response;
using TicketManagement.Domain.Entities;
using TicketManagement.Domain.ValueObjects;
using TicketManagement.Infrastructure.Repository.Interface;

namespace TicketManagement.Application.Commands.Handler
{

    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, ResponseWrapper<object>>
    {
        private readonly ITicketRepository _ticketRepository;

        public CreateTicketCommandHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<ResponseWrapper<object>> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.PhoneNumber) ||
                    string.IsNullOrWhiteSpace(request.Governorate) ||
                    string.IsNullOrWhiteSpace(request.City) ||
                    string.IsNullOrWhiteSpace(request.District))
                {
                    return new ResponseWrapper<object>
                    {
                        Success = false,
                        Errors = new List<string> { "Invalid input data. All fields are required." }
                    };
                }

                // Use the domain entity to encapsulate business logic.
                var address = new Address(request.Governorate, request.City, request.District);
                var ticket = new Ticket(request.PhoneNumber, address);

                // Add ticket via repository.
                await _ticketRepository.AddAsync(ticket, cancellationToken);

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
