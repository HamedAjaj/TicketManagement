using Microsoft.EntityFrameworkCore;
using TicketManagement.Domain.Entities;
using TicketManagement.Domain.ValueObjects;

namespace TicketManagement.Infrastructure.DBContext
{
    public class TicketDbContext : DbContext
    {
        public TicketDbContext(DbContextOptions<TicketDbContext> options) : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.OwnsOne(t => t.Address, address =>
                {
                    address.Property(a => a.Governorate).IsRequired();
                    address.Property(a => a.City).IsRequired();
                    address.Property(a => a.District).IsRequired();
                });
            });
        }
    }

}
