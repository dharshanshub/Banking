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
        public List<Transaction> ViewStatement(Account Account)
        {
            TransactionDal dal = new TransactionDal();

            List<Transaction> list = dal.ViewStatement(Account);
            return list;
        }

        public bool Withdraw(long AccountNo, long amount)
        {
            TransactionDal dal = new TransactionDal();
            var dateAndTime = DateTime.Now;

            var date = dateAndTime.Date.ToString();
            if (dal.Withdraw(AccountNo, amount,date))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Deposit(long AccountNo, long amount)
        {
            TransactionDal dal = new TransactionDal();
            var dateAndTime = DateTime.Now;

             var date = dateAndTime.Date.ToString();
            if (dal.Deposit(AccountNo, amount,date))
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
