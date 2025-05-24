using System.ComponentModel.DataAnnotations;
using System.Net;
using TicketManagement.Domain.ValueObjects;

namespace TicketManagement.Domain.Entities
{
    public class Ticket
    {
        public Guid Id { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Status { get; private set; } = "New";
        public string ColorCode
        {
            get
            {
                var timeSinceCreation = DateTime.UtcNow - CreationDateTime;

                if (timeSinceCreation.TotalMinutes <= 15) return "Yellow";
                else if (timeSinceCreation.TotalMinutes <= 30) return "Green";
                else if (timeSinceCreation.TotalMinutes <= 45) return "Blue";
                else return "Red";
            }
        }

        public Address Address { get; private set; }
        public DateTime CreationDateTime { get; private set; } = DateTime.UtcNow;

        protected Ticket() { }

        public Ticket(string phoneNumber, Address address)
        {
            Id = Guid.NewGuid();
            PhoneNumber = phoneNumber;
            Address = address;
        }

        public void UpdateStatus(string newStatus)
        {
            if (string.IsNullOrWhiteSpace(newStatus)) throw new ArgumentException("Status cannot be empty.");
            Status = newStatus;
        }
    
    }


}
