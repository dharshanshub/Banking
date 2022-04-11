using BankEntity;
using Microsoft.AspNetCore.Http;
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
      
        public IActionResult CreateNewUser()
        {
            var model = new Customer();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewUser(IFormCollection collection)
        {
            Customer customer = new Customer();
            customer.Name = collection["Name"];
            customer.Email = collection["Email"];
            customer.Address = collection["Address"];
            customer.BirthDate = collection["BirthDate"];
            customer.MobileNo = collection["MobileNo"];


            var cred = await this.SendDataToApi<Customer,IEnumerable<Credential>>(
                baseUri: configuration.GetConnectionString("BankAPIUrl"),
                requestUrl: "api/Customer/CreateNewUser",customer);
            return  RedirectToAction("Credentials","Customer",cred);
                

        }
        public async Task<IActionResult> Credentials(IEnumerable<Credential> cred)
        {
         
            
            return View(cred);
        }


    }
}


