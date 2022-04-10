using BankBal;
using BankEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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
        [HttpPost]
      
        public IActionResult Post(Customer cus)
        {
            if(bal.CreateNewUser(cus))
            {
                return Ok(bal.GenrateCredentials(cus));
            }
            else
            {
                return BadRequest();
            }

           
        }
        [HttpPut("{id:long}")]
     
        public IActionResult UpdateUser(Customer cus,long id)
        {
            return Ok(bal.UpdateUsers(cus, id));


        }




    }
}
