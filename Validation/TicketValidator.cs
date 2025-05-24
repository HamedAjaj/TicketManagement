using FluentValidation;
using TicketManagement.Commands.Model;
using TicketManagement.DTOs;
using TicketManagement.Entities;
namespace TicketManagement.Validation
{

    public class TicketValidator : AbstractValidator<CreateTicketCommand>
    {
        
        public TicketValidator()
        { 
            RuleFor(ticket => ticket.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number must be a valid  format."); 

            RuleFor(ticket => ticket.Governorate)
                .NotEmpty().WithMessage("Governorate is required.")
                .MaximumLength(100).WithMessage("Governorate must not exceed 100 characters.");

            RuleFor(ticket => ticket.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(100).WithMessage("City must not exceed 100 characters.");

            RuleFor(ticket => ticket.District)
                .NotEmpty().WithMessage("District is required.")
                .MaximumLength(100).WithMessage("District must not exceed 100 characters.");
           
        }
    }

}
