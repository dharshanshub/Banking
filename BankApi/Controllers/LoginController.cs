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
            if (status == false)
                return Unauthorized();
            //Create the token as authentication has succeeded
            JwtSecurityTokenHandler tokenHandler = new();
            var key = System.Text.Encoding.UTF8.GetBytes(_appSettings.AppSecretKey);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new[]
                    {
                        new System.Security.Claims.Claim("Username", "admin"),
                        new System.Security.Claims.Claim("Rolename", "Admin"),
                    }),
                Expires = System.DateTime.Now.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    key: new SymmetricSecurityKey(key),
                    algorithm: SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            var authresponse = new AuthenticatedUser<long>(model.AccountNo, "admin", "Admin", token);
            return Ok(authresponse);
        }
    







        [HttpPost("AuthenticateUser")]
                public IActionResult AuthenticateCustomer(LoginViewModel model)
                {

                    var status = bal.CustomerLogin(model);
                if (status == false)
                return Unauthorized();
               //Create the token as authentication has succeeded
            JwtSecurityTokenHandler tokenHandler = new();
            var key = System.Text.Encoding.UTF8.GetBytes(_appSettings.AppSecretKey);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new[]
                    {
                        new System.Security.Claims.Claim("Username", "Customer"),
                        new System.Security.Claims.Claim("Rolename", "Customer"),
                    }),
                Expires = System.DateTime.Now.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    key: new SymmetricSecurityKey(key),
                    algorithm: SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            var authresponse = new AuthenticatedUser<long>(model.AccountNo, "Customer", "customer", token);
            return Ok(authresponse);

        }
    }
}


        