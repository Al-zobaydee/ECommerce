﻿using ECommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersProvider provider;

        public CustomersController(ICustomersProvider provider)
        {
            this.provider = provider;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await provider.GetCustomersAsync();
            if (result.IsSuccess)
                return Ok(result.Customers);

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomersAsync(int id)
        {
            var result = await provider.GetCustomerAsync(id);
            if (result.IsSuccess)
                return Ok(result.Customer);

            return NotFound();
        }
    }
}
