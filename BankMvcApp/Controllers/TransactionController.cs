using BankEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BankMvcApp.Controllers
{
 
    public class TransactionController : Controller
    {
        private readonly ILogger<TransactionController> _logger;
        private IConfiguration configuration;
        public TransactionController(ILogger<TransactionController> logger, IConfiguration config)
        {
            _logger = logger;
            configuration = config;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Deposit()
        {
            var model = new Transaction();
            return View(model);

        }
        [HttpPost]

        public async Task<IActionResult> Deposit(IFormCollection collection)
        {
            Transaction trans = new Transaction();

            trans.ReceiverAccNo = long.Parse(collection["ReceiverAccNo"]);
            trans.TransactionAmount = double.Parse(collection["TransactionAmount"]);
            trans.Description = collection["Description"];

            var transaction = await this.SendDataToApi<Transaction, bool>(
                baseUri: configuration.GetConnectionString("BankAPIUrl"),
                requestUrl: "api/Transaction/Deposit", trans);
            if (transaction is true)
            {

                ViewBag.result = "Transaction Successfull";
                return View();
            }
            else
            {


                ViewBag.result = "Transaction Failed";
                return View();
            }
        }
        public IActionResult WithDraw()
        {
            var model = new Transaction();
            return View(model);

        }
        [HttpPost]

        public async Task<IActionResult> WithDraw(IFormCollection collection)
        {
            Transaction trans = new Transaction();

            trans.ReceiverAccNo = long.Parse(collection["ReceiverAccNo"]);
            trans.TransactionAmount = double.Parse(collection["TransactionAmount"]);
            trans.Description = collection["Description"];

            var transaction = await this.SendDataToApi<Transaction, bool>(
                baseUri: configuration.GetConnectionString("BankAPIUrl"),
                requestUrl: "api/Transaction/WithDraw", trans);
            if (transaction is true)
            {

                ViewBag.result = "Transaction Successfull";
                return View();
            }
            else
            {


                ViewBag.result = "Transaction Failed";
                return View();
            }
        }
        public IActionResult FundTransfer()
        {
            var model = new Transaction();
            return View(model);

        }
        [HttpPost]

        public async Task<IActionResult> FundTransfer(IFormCollection collection)
        {
            Transaction trans = new Transaction();

            trans.SenderAccNo = long.Parse(collection["SenderAccNo"]);
            trans.ReceiverAccNo = long.Parse(collection["ReceiverAccNo"]);
            trans.TransactionAmount = double.Parse(collection["TransactionAmount"]);
            trans.Branchcode = collection["Branchcode"].ToString();
            trans.Description = collection["Description"];

            var transaction = await this.SendDataToApi<Transaction,bool >(
                baseUri: configuration.GetConnectionString("BankAPIUrl"),
                requestUrl: "api/Transaction/FundTransfer", trans);
           
            if (transaction )
            {

                ViewBag.result = "Transaction Successfull";
                return View();
            }
            else
            {


                ViewBag.result = "Transaction Failed";
                return View();
            }
        }
      
        public IActionResult ViewStatement()
        {
            var model = new Transaction();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ViewStatement(IFormCollection collection)
        {
            Transaction transaction = new Transaction();
            transaction.SenderAccNo = long.Parse(collection["SenderAccNo"]);
       


            var cred = await this.SendDataToApi<Transaction, IEnumerable<Transaction>>(
                baseUri: configuration.GetConnectionString("BankAPIUrl"),
                requestUrl: "api/Transaction/ViewStatement", transaction);
            var credstring = JsonConvert.SerializeObject(cred);
            TempData["cred"] = credstring;
            return RedirectToAction("Statement", "Transaction");


        }
        public IActionResult Statement()
        {
            var credstring = TempData["cred"].ToString();
            var cred = JsonConvert.DeserializeObject<IEnumerable<Transaction>>(credstring);



            return View(model: cred);
        }







    }
}
