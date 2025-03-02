using Afi.CustomerPortal.Entities.Dto;

namespace Afi.CustomerPortal.Services
{
    public interface ICustomerRegistrationService
    {
        int Register(CustomerRegistration value);
    }
}