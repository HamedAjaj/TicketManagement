using MediatR;
using System.ComponentModel.DataAnnotations;
using TicketManagement.PipeLineBehavior.Response;

namespace TicketManagement.Commands.Model
{
    public class CreateTicketCommand : IRequest<ResponseWrapper<object>>
    {
         
        public string PhoneNumber { get; set; }
        public string Governorate { get; set; }
        public string City { get; set; }
        public string District { get; set; }
    }
}
