using BankDal;
using BankEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankBal
{
    public  class TransactionBal
    {
        public bool FundTransfer( Transaction t, Account a )
        {
            TransactionDal dal = new TransactionDal();



            if (dal.FundTransfer(t, a))
            {
                return true;
            }
            else
            {
                return false;
            }

           




        }
    }
}
