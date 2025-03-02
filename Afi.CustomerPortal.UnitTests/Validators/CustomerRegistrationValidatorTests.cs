using Afi.CustomerPortal.Entities.Dto;
using Afi.CustomerPortal.Validators;
using FluentValidation.TestHelper;

namespace Afi.CustomerPortal.UnitTests.Validators
{
    public class CustomerRegistrationValidatorTests
    {
        private readonly CustomerRegistrationValidator _validator;

        public CustomerRegistrationValidatorTests()
        {
            _validator = new CustomerRegistrationValidator();
        }

        [Theory]
        [InlineData("XX-999999")]
        [InlineData("AZ-123456")]
        public void ShouldPassWithValidPolicyNumber(string policyNumber)
        {
            var entry = CreateValidCustomerRegistration();
            entry.PolicyNumber = policyNumber;

            var result = _validator.TestValidate(entry);

            result.ShouldNotHaveValidationErrorFor(x => x.PolicyNumber);
        }

        [Theory]
        [InlineData("A")]
        [InlineData("AZ-12345A")]
        public void ShouldFailWithInvalidPolicyNumber(string policyNumber)
        {
            var entry = CreateValidCustomerRegistration();
            entry.PolicyNumber = policyNumber;

            var result = _validator.TestValidate(entry);

            result.ShouldHaveValidationErrorFor(x => x.PolicyNumber);
        }

        [Fact]
        public void ShouldPassWhenOlderThanAged18()
        {
            var entry = CreateValidCustomerRegistration();
            entry.DateOfBirth = DateOnly.FromDateTime(DateTime.Today).AddYears(-10);

            var result = _validator.TestValidate(entry);

            result.ShouldHaveValidationErrorFor(x => x.DateOfBirth);
        }

        [Fact]
        public void ShouldFailWhenYoungerThanAged18()
        {
            var entry = CreateValidCustomerRegistration();

            var result = _validator.TestValidate(entry);

            result.ShouldNotHaveValidationErrorFor(x => x.DateOfBirth);
        }

        [Theory]
        [InlineData("aaaa@aa.com")]
        [InlineData("aaaa@aa.co.uk")]
        [InlineData("aaaaaa@aa.com")]
        [InlineData("aaaaaa@aa.co.uk")]
        public void ShouldPassWithValidEmailAddress(string emailAddress)
        {
            var entry = CreateValidCustomerRegistration();
            entry.EmailAddress = emailAddress;

            var result = _validator.TestValidate(entry);

            result.ShouldNotHaveValidationErrorFor(x => x.EmailAddress);
        }

        [Theory]
        [InlineData("A")]
        [InlineData("a@a.com")]
        public void ShouldFailWithInvalidEmailAddress(string emailAddress)
        {
            var entry = CreateValidCustomerRegistration();
            entry.EmailAddress = emailAddress;

            var result = _validator.TestValidate(entry);

            result.ShouldHaveValidationErrorFor(x => x.EmailAddress);
        }

        private CustomerRegistration CreateValidCustomerRegistration()
        {
            return new CustomerRegistration
            {
                DateOfBirth = DateOnly.FromDateTime(DateTime.Today).AddYears(-20),
                EmailAddress = "aaaa@aaaa.com",
                FirstName = "First",
                LastName = "Last",
                PolicyNumber = "XX-999999"
            };
        }
    }
}