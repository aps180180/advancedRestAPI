using AdvancedRestAPI.DTOs;
using FluentValidation;

namespace AdvancedRestAPI.Validators
{
    public class CustomerDTOValidator : AbstractValidator<CustomerDTO>
    {
        public CustomerDTOValidator()
        {
            RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(10, 150).WithMessage("Name must be between 2 and 150 characters");

            RuleFor(c => c.Address)
            .NotEmpty().WithMessage("Address is required")
            .Length(10, 250).WithMessage("Address must be between 10 and 250 characters");

            RuleFor(c => c.Phone)
            .NotEmpty().WithMessage("Phone is required")
            .Matches(@"^\d{11}$").WithMessage("Phone must be a valid 11-digit number");

            RuleFor(c => c.BloodGroup)
            .NotEmpty().WithMessage("BloodGroup is required")
            .Length(2,3).WithMessage("BloodGroup must be between 2 and 3 characters");

        }
    }
}
