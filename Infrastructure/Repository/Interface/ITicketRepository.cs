using TicketManagement.Application.Pagination;
using TicketManagement.Domain.Entities;

namespace TicketManagement.Infrastructure.Repository.Interface
{
    public interface ITicketRepository
    {
        Task<Ticket> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task AddAsync(Ticket ticket, CancellationToken cancellationToken);
        Task<PaginatedList<Ticket>> GetPaginatedTicketsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
        List<Ticket> GetTicketsToHandle(DateTime cutoffTime);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
