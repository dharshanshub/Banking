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
            var model = await this.SendDataToApi<LoginViewModel, AuthenticatedUser<long>>(
              baseUri: configuration.GetConnectionString("BankApiUrl"),
             requestUrl: $"api/Login/AuthenticateAdmin", lg);

            HttpContext.Session.SetString("Token", model.Token);
            HttpContext.Session.SetString("RoleName", model.RoleName);
            HttpContext.Session.SetString("Username", model.Name);
            return View(model);




        }



       
    }
}
