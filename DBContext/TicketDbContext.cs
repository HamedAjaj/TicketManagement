using Microsoft.EntityFrameworkCore;
using TicketManagement.Entities;

namespace TicketManagement.DBContext
{ 
    public class TicketDbContext : DbContext
    {
        public TicketDbContext(DbContextOptions<TicketDbContext> options) : base(options)
        {
        }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
