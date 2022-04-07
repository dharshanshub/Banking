using Banking.Infrastructure;
using Banking.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Banking.Controllers
{
    public class AccountController : Controller
    {
        IUserService _service;
        public AccountController(IUserService service) => _service = service;
        public IActionResult Login(string returnUrl = "")
        {
            ViewBag.ReturnUrl = returnUrl;
            var model = new LoginViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = "/")
        {
           
            if (ModelState.IsValid)
            {
               List<User> user= _service.Authenticate(model);
              
                if (user.Count!=0)
                {
                    // set user as a session value
                    HttpContext.Session.SetString("User", JsonConvert.SerializeObject(_service.LoggedInUser));
                    string fullName = _service.LoggedInUser.FirstName;
                    var principal = new ClaimsPrincipal(
                        identity: new ClaimsIdentity(
                            claims: new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, fullName),
                            new Claim(ClaimTypes.NameIdentifier,_service.LoggedInUser.ToString())

                        },
                        authenticationType: CookieAuthenticationDefaults.AuthenticationScheme));
                    await HttpContext.SignInAsync(
                        principal: principal,
                        properties: new AuthenticationProperties { AllowRefresh = true, IsPersistent = false },
                        scheme: CookieAuthenticationDefaults.AuthenticationScheme
                        );
                    foreach (var e in user)
                    {
                        if (e.Status == "Active")
                        {
                            return RedirectToAction("Index", "Customer");
                        }
                        else 
                        {
                            return RedirectToAction("Freeze", "Customer");
                        }
                       
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Wrong AccountNumber or Password ");
                    return View(model);

                }
            }
            return View(model);
        }
        public IActionResult ProfileLogin(string returnUrl = "")
        {
            ViewBag.ReturnUrl = returnUrl;
            var customer = new Customer();
            return View(customer);
        }
        [HttpPost]
        public IActionResult ProfileLogin(Customer customer, string returnUrl = "/")
        {

           
                bool user = _service.TransactionAuthenticate(customer);

                if (user)
                {
                    return RedirectToAction("TrasnferFund", "Transaction");
                }
                else
                {
                    ModelState.AddModelError("", " Wrong Transaction Password ");
                    return View(customer);
                }
         
        }

    }
}
