using System.Collections.Generic;
using DbContextFactoryWebApiApp.DataAccess.Models;

namespace DbContextFactoryWebApiApp.Services
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAllCustomers();
    }
}
