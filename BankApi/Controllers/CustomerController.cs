using BankBal;
using BankEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CustomerController : ControllerBase
    {
        CustomerBal bal;
        public CustomerController(IConfiguration configuration)
        {
           
            bal = new CustomerBal(configuration.GetConnectionString("BankDbConnection"));
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(bal.ShowAllCustomers());
        }
        [HttpPost("CreateNewUser")]
      
        public IActionResult CreateNewUser(Customer cus)
        {
            if(bal.CreateNewUser(cus))
            {
               List<Credential> lst =bal.GenrateCredentials(cus);
              //  IEnumerable<Credential> enumerable = lst as IEnumerable<Credential>;
                return Ok(lst);
            }
            else
            {
                return BadRequest();
            }

           
        }
        [HttpPost("UpdateUser")]
     
        public IActionResult UpdateUser(Customer cus)
        {
            return Ok(bal.UpdateUsers(cus));


        }




    }
}
