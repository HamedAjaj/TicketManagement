using MediatR;

namespace TicketManagement.Application.Commands.Model
{
    public class HandleTicketCommand : IRequest<Unit>
    {
        public Guid TicketId { get; set; }
    }
}
