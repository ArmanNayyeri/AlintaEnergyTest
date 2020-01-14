using CustomerManagement.Models.Domain;
using System.Collections.Generic;

namespace CustomerManagement.Services
{
    public interface ICustomerService
    {
        bool Add(CustomerModel customer);
        CustomerModel GetById(int id);
        bool Delete(int id);
        bool Update(CustomerModel customer);
        IEnumerable<CustomerModel> Find(string term);
    }
}