using BankApi.Authentication;
using BankBal;
using BankDal;
using BankEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace BankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class LoginController : ControllerBase
    {
        LoginBal bal;
        private readonly AppSettings _appSettings;
        public LoginController(IOptions<AppSettings> settings, IConfiguration configuration)
        {
            _appSettings = settings.Value;
            bal = new LoginBal(configuration.GetConnectionString("BankDbConnection"));
        }
       
        
        [HttpPost("AuthenticateAdmin")]

        public IActionResult AuthenticateAdmin(LoginViewModel model)
        {
             
            var status = bal.AdminLogin(model);
           
            return Ok(status);
        }
    


        [HttpPost("AuthenticateUser")]
                public IActionResult AuthenticateCustomer(LoginViewModel model)
                {

                    var status = bal.CustomerLogin(model);
               
               
                   return Ok(status);

                }
    }
}


        