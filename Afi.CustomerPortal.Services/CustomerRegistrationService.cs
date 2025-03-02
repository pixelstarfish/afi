using Afi.CustomerPortal.Entities.Domain;
using Afi.CustomerPortal.Entities.Dto;
using Afi.CustomerPortal.Services.DataContexts;
using AutoMapper;

namespace Afi.CustomerPortal.Services
{
    public class CustomerRegistrationService : ICustomerRegistrationService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public CustomerRegistrationService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public int Register(CustomerRegistration value)
        {
            // Map entity from dto to domain.
            var customer = _mapper.Map<Customer>(value);

            _dataContext.Customers.Add(customer);
            _dataContext.SaveChanges();

            return customer.Id;
        }
    }
}