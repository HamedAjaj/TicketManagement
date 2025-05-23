using System.ComponentModel.DataAnnotations;

namespace TicketManagement.Entities
{
    public class Ticket
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreationDateTime { get; set; } = DateTime.UtcNow;
        [Phone]
        public string PhoneNumber { get; set; }
        public string Governorate { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Status { get; set; } = "New"; 
        public string? ColorCode { get; set; }
    }
}
