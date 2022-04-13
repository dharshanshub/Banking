using BankEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BankMvcApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private IConfiguration configuration;
        public LoginController(ILogger<LoginController> logger, IConfiguration config)
        {
            _logger = logger;
            configuration = config;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(IFormCollection collection)
        {
            LoginViewModel lg = new LoginViewModel();
            lg.AccountNo = long.Parse(collection["AccountNo"]);
            lg.Password = collection["Password"];
            var model = await this.SendDataToApi<LoginViewModel,bool>(
              baseUri: configuration.GetConnectionString("BankApiUrl"),
             requestUrl: $"api/Login/AuthenticateAdmin", lg);
         



            return RedirectToAction("Index", "Admin");
       
            

        }

        public IActionResult UserLogin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UserLogin(IFormCollection collection)
        {
            LoginViewModel lg = new LoginViewModel();
            lg.AccountNo = long.Parse(collection["AccountNo"]);
            lg.Password = collection["Password"];
            var model = await this.SendDataToApi<LoginViewModel, bool> (
              baseUri: configuration.GetConnectionString("BankApiUrl"),
             requestUrl: $"api/Login/AuthenticateUser", lg);

        

            
                return RedirectToAction("Index", "User");
            
            

        }




    }
}
