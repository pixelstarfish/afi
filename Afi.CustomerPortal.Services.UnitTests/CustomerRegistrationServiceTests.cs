using Afi.CustomerPortal.Entities.Domain;
using Afi.CustomerPortal.Entities.Dto;
using Afi.CustomerPortal.Services.DataContexts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace Afi.CustomerPortal.Services.UnitTests
{
    public class CustomerRegistrationServiceTests
    {
        [Fact]
        public void ShouldCorrectlyMapSaveAndReturnCustomerId()
        {
            var registration = new CustomerRegistration
            {
                DateOfBirth = DateOnly.FromDateTime(DateTime.Today).AddYears(-20),
                EmailAddress = "aaaa@aaaa.com",
                FirstName = "First",
                LastName = "Last",
                PolicyNumber = "AA-666666"
            };

            var customer = new Customer { FirstName = registration.FirstName, LastName = registration.LastName };

            // Ensure DataContext SaveChanges is called and customer Id is returned.
            var dataContext = new Mock<DataContext>(new DbContextOptionsBuilder<DataContext>().Options);
            dataContext.Setup(x => x.Customers).ReturnsDbSet([]);
            dataContext.Setup(x => x.Customers.Add(customer));
            dataContext.Setup(x => x.SaveChanges())
                .Callback(() => customer.Id = 1);

            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.Map<Customer>(registration)).Returns(customer);

            var service = new CustomerRegistrationService(dataContext.Object, mapper.Object);

            int result = service.Register(registration);

            Assert.Equal(1, result);
        }
    }
}