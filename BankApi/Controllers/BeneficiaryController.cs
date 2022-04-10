using BankBal;
using BankEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiaryController : ControllerBase
    {
        BeneficiaryBal bal;
        public BeneficiaryController(IConfiguration configuration)
        {

            bal = new BeneficiaryBal(configuration.GetConnectionString("BankDbConnection"));
        }

        [HttpPost]

        public IActionResult AddBeneficiary(Beneficiary beneficiary)
        {
            if (bal.AddBeneficiary(beneficiary))
            {
                return Ok(true);
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpGet("{id:long}")]
        
        public IActionResult GetBeneficiary(long id)
        {
            return Ok(bal.ShowAllBeneficiaries(id));
        }
        [HttpDelete("{id:long}")]
      
        public IActionResult DeleteBeneficiary(long id)
        {
            return Ok(bal.DeleteBeneficiary(id));
        }
    }
}
