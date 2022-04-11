using BankEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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


            var cred = await this.SendDataToApi<Customer, IEnumerable<Credential>>(
                baseUri: configuration.GetConnectionString("BankAPIUrl"),
                requestUrl: "api/Customer/CreateNewUser", customer);
            var credstring = JsonConvert.SerializeObject(cred);
            TempData["cred"] = credstring;
            return RedirectToAction("Credentials", "Customer");


        }
        public async Task<IActionResult> Credentials()
        {

            var credstring = TempData["cred"].ToString();
            var cred = JsonConvert.DeserializeObject<IEnumerable<Credential>>(credstring);



            return View(model: cred);

        }
        public IActionResult EditCustomer()
        {
            var model = new Customer();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditCustomer(IFormCollection collection)
        {
            Customer customer = new Customer();
            customer.IbPassword = collection["IbPassword"];
            customer.Email = collection["Email"];
            customer.Address = collection["Address"];
            customer.CRN = int.Parse(collection["CRN"]);
            customer.MobileNo = collection["MobileNo"];


            var cred = await this.SendDataToApi<Customer, bool>(
                baseUri: configuration.GetConnectionString("BankAPIUrl"),
                requestUrl: "api/Customer/UpdateUser", customer);

            if (cred)
            {

                return RedirectToAction("UpdatedSucessfully");
            }
            else
            {

                return Content("updation failed");
            }


        }
        public IActionResult UpdatedSucessfully()
        {
            return View();
        }
    }
}


