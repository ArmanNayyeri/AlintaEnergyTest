using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagement.Models.Entities
{
    public class CustomerManagementContext : DbContext
    {
        public CustomerManagementContext(DbContextOptions<CustomerManagementContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
