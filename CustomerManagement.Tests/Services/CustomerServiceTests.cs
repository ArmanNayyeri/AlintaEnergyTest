using Xunit;
using CustomerManagement.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerManagement.Services.Tests
{
    public class CustomerServiceTests :IClassFixture<DbFixture>
    {
        private readonly ServiceProvider _serviceProvider;

        public CustomerServiceTests(DbFixture dbFixture)
        {
            _serviceProvider = dbFixture.ServiceProvider;
        }

        [Fact]
        public void AddTest_ShouldNotFail()
        {
            var customerService = _serviceProvider.GetService<ICustomerService>();

            Assert.True(customerService.Add(new Models.Domain.CustomerModel
            {
                FirstName = "FirstName",
                LastName = "LastName",
                DateOfBirth = DateTime.Now
            }), "Add method failed");
        }

        [Fact]
        public void AddTest_ShouldBeAdded()
        {
            var customerService = _serviceProvider.GetService<ICustomerService>();

            Assert.True(customerService.Add(new Models.Domain.CustomerModel
            {
                FirstName = "FirstName",
                LastName = "LastName",
                DateOfBirth = DateTime.Now
            }), "Add method failed");

            var customer = customerService.GetById(1);
            Assert.NotNull(customer);
        }

        [Fact]
        public void AddTest_ShouldAddCorrectValues()
        {
            var customerService = _serviceProvider.GetService<ICustomerService>();
            var dateOfBirth = DateTime.Now;
            Assert.True(customerService.Add(new Models.Domain.CustomerModel
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                DateOfBirth = dateOfBirth
            }), "Add method failed");

            var customer = customerService.Find("TestLastName").FirstOrDefault();

            Assert.Equal("TestFirstName", customer.FirstName);
            Assert.Equal("TestLastName", customer.LastName);
            Assert.Equal(dateOfBirth, customer.DateOfBirth);
        }

        [Fact()]
        public void DeleteTest_ShouldBeDeleted()
        {
            var customerService = _serviceProvider.GetService<ICustomerService>();
            Assert.True(customerService.Add(new Models.Domain.CustomerModel
            {
                FirstName = "FirstName",
                LastName = "LastName",
                DateOfBirth = DateTime.Now
            }));

            Assert.True(customerService.Delete(1), "Customer delete failed");
            Assert.Null(customerService.GetById(1));
        }

        [Fact()]
        public void FindTest_ShouldBeFound()
        {
            var customerService = _serviceProvider.GetService<ICustomerService>();
            Assert.True(customerService.Add(new Models.Domain.CustomerModel
            {
                FirstName = "SearchFirst",
                LastName = "FindLast",
                DateOfBirth = DateTime.Now
            }));

            var customers = customerService.Find("earch");
            Assert.True(customers.Any());

            customers = customerService.Find("ndLa");
            Assert.True(customers.Any());
        }

        [Fact()]
        public void UpdateTest_ShouldBeUpdated()
        {
            var customerService = _serviceProvider.GetService<ICustomerService>();
            Assert.True(customerService.Add(new Models.Domain.CustomerModel
            {
                FirstName = "testUpdateFirstName",
                LastName = "testUpdateLastName",
                DateOfBirth = DateTime.Now
            }));

            var customer = customerService.Find("testUpdateFirstName").FirstOrDefault();
            customer.FirstName = "testUpdateFirstName2";
            customerService.Update(customer);
            var updatedCustomer = customerService.Find("testUpdateFirstName").FirstOrDefault();

            Assert.Equal("testUpdateFirstName2", updatedCustomer.FirstName);
            Assert.Equal("testUpdateLastName", updatedCustomer.LastName);
        }
    }
}