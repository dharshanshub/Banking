using BankEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BankMvcApp.Controllers
{
    public class CustomerController : Controller
    {

        private readonly ILogger<CustomerController> _logger;
        private IConfiguration configuration;
        public CustomerController(ILogger<CustomerController> logger, IConfiguration config)
        {
            _logger = logger;
            configuration = config;
        }

        public async Task<IActionResult> GetAllUsers()
        {

            var model = await this.GetResponseFromApi<IEnumerable<Customer>>(
                baseUri: configuration.GetConnectionString("BankAPIUrl"),
                requestUrl: "api/Customer");

            return View(model);
        }
        public async Task<IActionResult> CreateCustomer()
        {
            return View();

        }

    }
}


