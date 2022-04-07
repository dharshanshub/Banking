using Banking.Infrastructure;
using Banking.Models;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banking.Controllers
{
    public class TransactionController : Controller
    {
        ITransactionService _service;
        private static Random random = new Random();

        public TransactionController(ITransactionService service) => _service = service;
        public IActionResult TrasnferFund(string returnUrl = "")
        {
            ViewBag.ReturnUrl = returnUrl;
            var model = new TransactionModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult TransferFund(TransactionModel model, string returnUrl = "/")
        {

         

                model.Date = System.DateTime.Now.ToString();
                model.TransactionId = RandomString(10);

                bool txn = _service.FundTransfer(model);
                if (txn)
                {
                    return View(model);
                }
                else
                {
                    ModelState.AddModelError("", "Transaction has not been completed an error occured");
                    return View(model);
                }


            
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
