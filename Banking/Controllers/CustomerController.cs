using Microsoft.AspNetCore.Mvc;

namespace Banking.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
