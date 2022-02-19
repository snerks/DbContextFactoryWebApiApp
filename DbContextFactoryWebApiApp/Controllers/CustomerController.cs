using System.Collections.Generic;
using DbContextFactoryWebApiApp.DataAccess.Models;
using DbContextFactoryWebApiApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace DbContextFactoryWebApiApp.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            var customers = _customerService.GetAllCustomers();

            return customers;
        }
    }
}
