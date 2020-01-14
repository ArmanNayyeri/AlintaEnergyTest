using CustomerManagement.Models.Entities;
using CustomerManagement.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerManagement
{
    public class DbFixture
    {
        public DbFixture()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient<ICustomerService, CustomerService>();
            serviceCollection.AddDbContext<CustomerManagementContext>((opt) => { opt.UseInMemoryDatabase("Customers"); }, ServiceLifetime.Transient);

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }
}
