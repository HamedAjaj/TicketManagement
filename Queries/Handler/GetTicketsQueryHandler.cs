using MediatR;
using System;
using TicketManagement.DBContext;
using TicketManagement.DTOs;
using TicketManagement.Entities;
using TicketManagement.Helpers;
using TicketManagement.Queries.Model;

namespace TicketManagement.Queries.Handler
{
    public class GetTicketsQueryHandler : IRequestHandler<GetTicketsQuery, PaginatedList<TicketDto>>
    {
        private readonly TicketDbContext _context;
        public GetTicketsQueryHandler(TicketDbContext context) => _context = context;

        public async Task<PaginatedList<TicketDto>> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
        {
            // Fetch only the necessary fields  
            var ticketsQuery = _context.Tickets
                .OrderBy(t => t.CreationDateTime);

            //  Fetch with pagination
            var paginatedData = await PaginatedList<Ticket>.CreateAsync(ticketsQuery, request.Page, request.PageSize, cancellationToken);
             
            var ticketDtos = paginatedData.Select(t => new TicketDto
            {
                Id = t.Id,
                CreationDateTime = t.CreationDateTime,
                PhoneNumber = t.PhoneNumber,
                Governorate = t.Governorate,
                City = t.City,
                District = t.District,
                Status = t.Status,
                ColorCode = GetColorCode(t.CreationDateTime) // In-memory  
            }).ToList();

            return new PaginatedList<TicketDto>(ticketDtos, paginatedData.TotalCount, request.Page, request.PageSize);
        }



        private string GetColorCode(DateTime CreationDateTime)
        {
            var timeSinceCreation = DateTime.UtcNow - CreationDateTime;

            if (timeSinceCreation.TotalMinutes <= 15) return "Yellow";
            if (timeSinceCreation.TotalMinutes <= 30) return "Green";
            if (timeSinceCreation.TotalMinutes <= 45) return "Blue";
            return "Red";
        }


    }
}
