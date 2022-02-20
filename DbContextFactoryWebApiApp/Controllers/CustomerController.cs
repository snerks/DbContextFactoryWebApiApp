﻿using System.Collections.Generic;
using System.Linq;
using DbContextFactoryWebApiApp.Dtos;
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
        public IEnumerable<CustomerDto> Get()
        {
            var customers = _customerService.GetAllCustomers();

            return customers.Select(c => new CustomerDto
            {
                Id = c.CustomerId,
                Name = c.Name,
                RegistrationDate = c.RegistrationDate,
                Website = c.Website
            });
        }
    }
}
