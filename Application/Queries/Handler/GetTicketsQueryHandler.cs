using MediatR;
using System;
using TicketManagement.API.DTOs;
using TicketManagement.Application.Pagination;
using TicketManagement.Application.Queries.Model;
using TicketManagement.Domain.Entities;
using TicketManagement.Infrastructure.DBContext;
using TicketManagement.Infrastructure.Repository.Interface;

namespace TicketManagement.Application.Queries.Handler
{
    public class GetTicketsQueryHandler : IRequestHandler<GetTicketsQuery, PaginatedList<Ticket>>
    { 
        private readonly ITicketRepository _repository;
        public GetTicketsQueryHandler(ITicketRepository repository) => _repository = repository;

        public async Task<PaginatedList<Ticket>> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
        {
            var ticketsQuery = await _repository.GetPaginatedTicketsAsync(request.PageIndex, request.PageSize, cancellationToken);
            return ticketsQuery;
        
        }

    }
}
