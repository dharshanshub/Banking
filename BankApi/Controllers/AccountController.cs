using BankBal;
using BankEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BankApi.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
   
    public class AccountController : ControllerBase
    {
        AccountBal bal;
        public AccountController(IConfiguration configuration)
        {

            bal = new AccountBal(configuration.GetConnectionString("BankDbConnection"));
        }

        [HttpPost("FreezeAccount")]

        public IActionResult FreezeAccount(Account account)
        {


            return Ok(bal.FreezeAccount(account));


        }
    }
}
