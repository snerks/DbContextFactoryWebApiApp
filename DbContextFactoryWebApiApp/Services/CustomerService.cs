using System.Collections.Generic;
using System.Linq;
using DbContextFactoryWebApiApp.DataAccess;
using DbContextFactoryWebApiApp.DataAccess.Models;

namespace DbContextFactoryWebApiApp.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CRMContext crmContext;

        public CustomerService(CRMContext crmContext)
        {
            this.crmContext = crmContext;
            this.crmContext = crmContext;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return crmContext.Customers.ToList();
        }
    }
}
