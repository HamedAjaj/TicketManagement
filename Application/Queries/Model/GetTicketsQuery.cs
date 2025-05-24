using MediatR;
using TicketManagement.API.DTOs;
using TicketManagement.Application.Pagination;
using TicketManagement.Domain.Entities; 

namespace TicketManagement.Application.Queries.Model
{
    public class  GetTicketsQuery : IRequest<PaginatedList<Ticket>>
    {
        public int PageIndex { get; }
        public int PageSize { get; }

        public GetTicketsQuery(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}
