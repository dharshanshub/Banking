﻿using Microsoft.AspNetCore.Mvc;

namespace BankMvcApp.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
