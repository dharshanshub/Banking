﻿using BankBal;
using BankEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        TransactionBal bal;
        public TransactionController(IConfiguration configuration)
        {

            bal = new TransactionBal(configuration.GetConnectionString("BankDbConnection"));
        }

        [HttpPost]
        public IActionResult FundTransfer(Transaction transaction)
        {
          string id = bal.FundTransfer(transaction);

            return Ok(id);


        }
    }
}