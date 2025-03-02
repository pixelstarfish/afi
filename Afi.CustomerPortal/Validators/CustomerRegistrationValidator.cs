using Afi.CustomerPortal.Entities.Dto;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Afi.CustomerPortal.Validators
{
    public class CustomerRegistrationValidator : AbstractValidator<CustomerRegistration>
    {
        public CustomerRegistrationValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name must be supplied.")
                .MinimumLength(3).WithMessage("First name must be between 3 and 50 characters.")
                .MaximumLength(50).WithMessage("First name must be between 3 and 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("First name must be supplied.")
                .MinimumLength(3).WithMessage("First name must be between 3 and 50 characters.")
                .MaximumLength(50).WithMessage("First name must be between 3 and 50 characters.");

            RuleFor(x => x.PolicyNumber)
                .NotEmpty().WithMessage("Policy number must be supplied.")
                .Must(MustBeValidPolicyNumber).WithMessage("You must supply a valid policy number.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().When(x => string.IsNullOrWhiteSpace(x.EmailAddress)).WithMessage("Date of birth is required if E-mail is empty.");

            RuleFor(x => x.DateOfBirth)
                .Must(MustBeOlderThan18).WithMessage("You must be at least 18 years old.")
                .When(y => y.DateOfBirth != null);

            RuleFor(x => x.EmailAddress)
                .NotEmpty().When(x => x.DateOfBirth == null).WithMessage("DE-mail is required if date of birth is empty.");

            RuleFor(x => x.EmailAddress)
                .Must(MustBeAValidEmailAddress).WithMessage("You must supply a valid e-mail address.")
                .When(y => !string.IsNullOrEmpty(y.EmailAddress));
        }

        private static bool MustBeValidPolicyNumber(string policyNumber)
        {
            return policyNumber != null && Regex.IsMatch(policyNumber, @"^[A-Z]{2}-\d{6}$");
        }

        private static bool MustBeOlderThan18(DateOnly? dateOfBirth)
        {
            if (dateOfBirth == null)
            {
                return false;
            }

            var today = DateOnly.FromDateTime(DateTime.Today);
            var age = today.Year - dateOfBirth.Value.Year;

            // Adjust age if birthday hasn't occurred this year.
            if (dateOfBirth > today.AddYears(-age))
            {
                age--;
            }

            return age >= 18;
        }

        private static bool MustBeAValidEmailAddress(string? emailAddress)
        {
            return emailAddress != null && Regex.IsMatch(emailAddress, @"^[a-zA-Z0-9]{4,}@[a-zA-Z0-9]{2,}(\.com|\.co\.uk)$");
        }
    }
}