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

        [HttpPost("AddBeneficiary")]

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
        [HttpPost("GetBeneficiary")]
        
        public IActionResult GetBeneficiary(Beneficiary beneficiary)
        {
            long id;
            id = beneficiary.SenderAccNo;
            return Ok(bal.ShowAllBeneficiaries(id));
        }
        [HttpPost("DeleteBeneficiary/{id:int}")]
      
        public IActionResult DeleteBeneficiary(int id)
        {
            return Ok(bal.DeleteBeneficiary(id));
        }
    }
}
