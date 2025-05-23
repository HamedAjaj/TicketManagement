using MediatR;
using TicketManagement.DTOs;
using TicketManagement.Entities;
using TicketManagement.Helpers;

namespace TicketManagement.Queries.Model
{
    public class  GetTicketsQuery : IRequest<PaginatedList<TicketDto>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
