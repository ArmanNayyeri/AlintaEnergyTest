using CustomerManagement.Models.Domain;
using CustomerManagement.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerManagement.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerManagementContext _customerManagementContext;

        public CustomerService(CustomerManagementContext customerManagementContext)
        {
            _customerManagementContext = customerManagementContext;
        }

        public bool Add(CustomerModel customer)
        {
            var customerEntity = new Customer
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                DateOfBirth = customer.DateOfBirth
            };

            _customerManagementContext.Customers.Add(customerEntity);

            return _customerManagementContext.SaveChanges() != 0;
        }

        public bool Delete(int id)
        {
            var customer = _customerManagementContext.Customers.FirstOrDefault(x => x.Id == id);
            if (customer == null) return false;

            _customerManagementContext.Customers.Remove(customer);

            return _customerManagementContext.SaveChanges() != 0;
        }

        public IEnumerable<CustomerModel> Find(string term)
        {
            return _customerManagementContext.Customers
                .Where(x => x.FirstName.Contains(term, StringComparison.CurrentCultureIgnoreCase) || x.LastName.Contains(term, StringComparison.CurrentCultureIgnoreCase))
                .Select(c => new CustomerModel
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    DateOfBirth = c.DateOfBirth
                })
                .ToList();
        }

        public CustomerModel GetById(int id)
        {
            var customer = _customerManagementContext.Customers.FirstOrDefault(x => x.Id == id);
            if (customer == null) return null;

            return new CustomerModel
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                DateOfBirth = customer.DateOfBirth
            };
        }

        public bool Update(CustomerModel customer)
        {
            var customerEntity = _customerManagementContext.Customers.FirstOrDefault(x => x.Id == customer.Id);
            if (customerEntity == null) return false;

            customerEntity.FirstName = customer.FirstName;
            customerEntity.LastName = customer.LastName;
            customerEntity.DateOfBirth = customer.DateOfBirth;

            return _customerManagementContext.SaveChanges() != 0;
        }
    }
}
