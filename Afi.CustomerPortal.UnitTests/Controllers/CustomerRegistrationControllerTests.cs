using Afi.CustomerPortal.Controllers;
using Afi.CustomerPortal.Entities.Dto;
using Afi.CustomerPortal.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace Afi.CustomerPortal.UnitTests.Controllers
{
    public class CustomerRegistrationControllerTests
    {
        [Fact]
        public void ShouldCallRegisterAndReturnCustomerId_OnSuccess()
        {
            var registration = CreateCustomerRegistration();

            var service = new Mock<ICustomerRegistrationService>();
            service.Setup(x => x.Register(registration)).Returns(1);

            var controller = new CustomerRegistrationController(service.Object);
            IResult result = controller.Post(registration);

            var created = Assert.IsType<Created<int>>(result);
            Assert.Equal(1, created.Value);
        }

        [Fact]
        public void ShouldCallRegisterAndReturnConflict_WhenReturnedCustomerIdZero()
        {
            var registration = CreateCustomerRegistration();

            var service = new Mock<ICustomerRegistrationService>();
            service.Setup(x => x.Register(registration)).Returns(0);

            var controller = new CustomerRegistrationController(service.Object);
            IResult result = controller.Post(registration);

            Assert.IsType<Conflict>(result);
        }

        private CustomerRegistration CreateCustomerRegistration()
        {
            return new CustomerRegistration
            {
                DateOfBirth = DateOnly.FromDateTime(DateTime.Today).AddYears(-20),
                EmailAddress = "aaaa@aaaa.com",
                FirstName = "First",
                LastName = "Last",
                PolicyNumber = "AA-666666"
            };
        }
    }
}