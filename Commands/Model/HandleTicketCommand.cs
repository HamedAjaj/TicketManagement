using MediatR;

namespace TicketManagement.Commands.Model
{
    public class HandleTicketCommand : IRequest<Unit>
    {
        public Guid TicketId { get; set; }
    }
}
