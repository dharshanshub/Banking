using Microsoft.AspNetCore.Mvc;

namespace BankMvcApp.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
