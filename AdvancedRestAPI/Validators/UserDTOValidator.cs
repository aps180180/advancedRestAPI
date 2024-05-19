using AdvancedRestAPI.DTOs;
using FluentValidation;

namespace AdvancedRestAPI.Validators
{
    public class UserDTOValidator : AbstractValidator<UserDTO>
    {
        public UserDTOValidator()
        {
            RuleFor(user => user.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(10, 150).WithMessage("Name must be between 2 and 150 characters");

            RuleFor(user => user.Address)
            .NotEmpty().WithMessage("Address is required")
            .Length(10, 250).WithMessage("Address must be between 10 and 250 characters");

            RuleFor(user => user.Phone)
            .NotEmpty().WithMessage("Phone is required")
            .Matches(@"^\d{11}$").WithMessage("Phone must be a valid 11-digit number");

            RuleFor(user => user.BloodGroup)
            .NotEmpty().WithMessage("BloodGroup is required")
            .Length(2,3).WithMessage("BloodGroup must be between 2 and 3 characters");

        }
    }
}
