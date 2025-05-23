using FluentValidation;
using TicketManagement.Entities;
namespace TicketManagement.DBContext.Validation
{

    public class TicketValidator : AbstractValidator<Ticket>
    {
        // not used yet
        public TicketValidator()
        { 
            RuleFor(ticket => ticket.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number must be a valid E.164 format."); 

            RuleFor(ticket => ticket.Governorate)
                .NotEmpty().WithMessage("Governorate is required.")
                .MaximumLength(100).WithMessage("Governorate must not exceed 100 characters.");

            RuleFor(ticket => ticket.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(100).WithMessage("City must not exceed 100 characters.");

            RuleFor(ticket => ticket.District)
                .NotEmpty().WithMessage("District is required.")
                .MaximumLength(100).WithMessage("District must not exceed 100 characters.");

            RuleFor(ticket => ticket.Status)
                .NotEmpty().WithMessage("Status is required.")
                .Must(status => new[] { "New", "InProgress", "Completed" }.Contains(status))
                .WithMessage("Status must be 'New', 'InProgress', or 'Completed'.");

            RuleFor(ticket => ticket.ColorCode)
                .Must(color => color == null || new[] { "Yellow", "Green", "Blue", "Red" }.Contains(color))
                .WithMessage("ColorCode must be null or one of the predefined values: Yellow, Green, Blue, Red.");
        }
    }

}
