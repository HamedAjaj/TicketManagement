using MediatR;
using System.ComponentModel.DataAnnotations;
using TicketManagement.API.PipeLineBehavior.Response;

namespace TicketManagement.Application.Commands.Model
{
    public class CreateTicketCommand : IRequest<ResponseWrapper<object>>
    {
        public string PhoneNumber { get; }
        public string Governorate { get; }
        public string City { get; }
        public string District { get; }

        public CreateTicketCommand(string phoneNumber, string governorate, string city, string district)
        {
            PhoneNumber = phoneNumber;
            Governorate = governorate;
            City = city;
            District = district;
        }
    }
}
