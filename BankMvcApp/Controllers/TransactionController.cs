using BankEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        public async Task<IActionResult> ViewStatement()
        {

            var model = await this.GetResponseFromApi<IEnumerable<Transaction>>(
                baseUri: configuration.GetConnectionString("BankAPIUrl"),
                requestUrl: "api/Transaction/ViewStatement/{id:long}");

            return View(model);
        }

    }
}
