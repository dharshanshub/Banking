using BankEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BankMvcApp.Controllers
{
    public class BeneficiaryController : Controller
    {

        private readonly ILogger<BeneficiaryController> _logger;
        private IConfiguration configuration;
        public BeneficiaryController(ILogger<BeneficiaryController> logger, IConfiguration config)
        {
            _logger = logger;
            configuration = config;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddBeneficiary()
        {
            var model = new Beneficiary();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddBeneficiary(IFormCollection collection)
        {
            Beneficiary beneficiary = new Beneficiary();
            beneficiary.SenderAccNo = Convert.ToInt32(collection["SenderAccNo"]);
            beneficiary.ReceiverAccNo = Convert.ToInt32(collection["ReceiverAccNo"]);
            beneficiary.BranchName = collection["BranchName"];
            beneficiary.NickName = collection["NickName"];
            beneficiary.IFSC = collection["IFSC"];


            var cred = await this.SendDataToApi<Beneficiary, bool>(
                baseUri: configuration.GetConnectionString("BankAPIUrl"),
                requestUrl: "api/Beneficiary/AddBeneficiary", beneficiary);
            if (cred)
            {
                ViewBag.result = "Updation Successfull";
                return View();
            }
            else
            {
                ViewBag.result = "Updation Failed";
                return View();

            }


        }
        public IActionResult ShowAllBeneficiary()
        {
            var model = new Beneficiary();
            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> ShowAllBeneficiary(IFormCollection collection)
        {
            Beneficiary beneficiary = new Beneficiary();
            beneficiary.SenderAccNo = Convert.ToInt32(collection["SenderAccNo"]);





            var cred = await this.SendDataToApi<Beneficiary, IEnumerable<Beneficiary>>(
                  baseUri: configuration.GetConnectionString("BankAPIUrl"),
                  requestUrl: "api/Beneficiary/GetBeneficiary", beneficiary);
            var credstring = JsonConvert.SerializeObject(cred);
            TempData["cred"] = credstring;
            return RedirectToAction("Showbeneficiary", "Beneficiary");

        }
        public IActionResult Showbeneficiary()
        {
            var credstring = TempData["cred"].ToString();
            var cred = JsonConvert.DeserializeObject<IEnumerable<Beneficiary>>(credstring);



            return View(model: cred);
        }
        [HttpPost, Route("/{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var cred = await this.SendDataToApi<int, bool>(
                baseUri: configuration.GetConnectionString("BankAPIUrl"),
                requestUrl: "api/Beneficiary/DeleteBeneficiary", id);

            return View("index","Home");
        }



    }
        

    }

