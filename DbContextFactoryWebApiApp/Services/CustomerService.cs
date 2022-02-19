using System.Collections.Generic;
using System.Linq;
using DbContextFactoryWebApiApp.DataAccess;
using DbContextFactoryWebApiApp.DataAccess.Models;

namespace DbContextFactoryWebApiApp.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CRMContext _crmContext;

        public CustomerService(CRMContext crmContext)
        {
            _crmContext = crmContext;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _crmContext.Customers.ToList();
        }
    }
}
