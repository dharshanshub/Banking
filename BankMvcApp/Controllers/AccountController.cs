using BankEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BankMvcApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private IConfiguration configuration;
        public AccountController(ILogger<AccountController> logger, IConfiguration config)
        {
            _logger = logger;
            configuration = config;
        }
        public IActionResult FreezeAccount()
        {
            var model = new Account();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> FreezeAccount(IFormCollection collection)
        {
            Account account = new Account();
            account.AccNo = long.Parse(collection["AccNo"]);



            var cred = await this.SendDataToApi<Account, bool>(
                baseUri: configuration.GetConnectionString("BankAPIUrl"),
                requestUrl: "api/Account/FreezeAccount", account);
            if (cred)
            {
                return RedirectToAction("Freezed", "Account");
            }
            else
            {
                return Content("failed");
            }


        }
        public IActionResult Freezed()
        {
            return View();
        }
    }
}
