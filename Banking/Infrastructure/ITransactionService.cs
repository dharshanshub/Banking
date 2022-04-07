
using Banking.Models;
using System.Collections.Generic;

namespace Banking.Infrastructure
{
    public interface ITransactionService
    {
        bool FundTransfer(TransactionModel model);

    }
}
