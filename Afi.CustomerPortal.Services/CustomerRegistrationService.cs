using Afi.CustomerPortal.Entities.Domain;
using Afi.CustomerPortal.Entities.Dto;
using Afi.CustomerPortal.Services.DataContexts;
using AutoMapper;

namespace Afi.CustomerPortal.Services
{
    public class CustomerRegistrationService(DataContext dataContext, IMapper mapper) : ICustomerRegistrationService
    {
        private readonly DataContext _dataContext = dataContext;
        private readonly IMapper _mapper = mapper;

        public int Register(CustomerRegistration value)
        {
            // Map entity from dto to domain.
            var customer = _mapper.Map<Customer>(value);

            // E-mail address and policy number should be unique (in theory)
            if (!_dataContext.Customers.Any(x => x.EmailAddress == value.EmailAddress) && 
                !_dataContext.CustomerPolicies.Any(x => x.PolicyNumber == value.PolicyNumber))
            {
                _dataContext.Customers.Add(customer);
                _dataContext.SaveChanges();
            }

            return customer.Id;
        }
    }
}