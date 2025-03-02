using Afi.CustomerPortal.Services.DataContexts;
using Afi.CustomerPortal.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Afi.CustomerPortal.Services.Configuration
{
    public static class ServiceConfiguration
    {
        public static void ConfigureDataLayer(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure database.
            var connectionString = configuration.GetConnectionString("Default") ??
                throw new InvalidOperationException("'Default' connection string not found.");

            // The DbContext already implements the Repository and Unit of Work patterns, so do not abstract further.
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

            // Configure entity mapper.
            services.AddAutoMapper(typeof(MappingProfile));

            // Add services to container.
            services.AddTransient<ICustomerRegistrationService, CustomerRegistrationService>();
        }
    }
}