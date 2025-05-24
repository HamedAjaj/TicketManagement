using Microsoft.EntityFrameworkCore;
using TicketManagement.Application.Pagination;
using TicketManagement.Domain.Entities;
using TicketManagement.Infrastructure.DBContext;
using TicketManagement.Infrastructure.Repository.Interface;

namespace TicketManagement.Infrastructure.Repository.Implementation
{
    public class TicketRepository : ITicketRepository
    {
        private readonly TicketDbContext _context;

        public TicketRepository(TicketDbContext context)
        {
            _context = context;
        }

        public async Task<Ticket> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Tickets.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task AddAsync(Ticket ticket, CancellationToken cancellationToken)
        {
            await _context.Tickets.AddAsync(ticket, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<PaginatedList<Ticket>> GetPaginatedTicketsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
        {             
            var source = _context.Tickets.Include(t => t.Address).AsQueryable();
            return await PaginatedList<Ticket>.CreateAsync(source.OrderBy(s=>s.CreationDateTime), pageIndex, pageSize, cancellationToken);
        }

        public List<Ticket> GetTicketsToHandle(DateTime cutoffTime)
        {
            var data =  _context.Tickets.Where(t => t.Status == "New" && t.CreationDateTime <= cutoffTime).ToList();
            return data;
        }


        public Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }

}
