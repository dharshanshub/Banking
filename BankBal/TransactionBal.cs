using BankDal;
using BankEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankBal
{
    public class TransactionBal
    {
        string connectionString = @"Data Source=LAPTOP-NKUJCDUA\SQLEXPRESS;database=Bank;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public bool FundTransfer(Transaction t, Account a)
        {
            TransactionDal dal = new TransactionDal(connectionString);



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
            TransactionDal dal = new TransactionDal(connectionString);

            List<Transaction> list = dal.ViewStatement(Account);
            return list;
        }

        public bool Withdraw(Account  account, Transaction transaction)
        {
            TransactionDal dal = new TransactionDal(connectionString);
            var dateAndTime = DateTime.Now;

         transaction.TransactionDate = dateAndTime.Date.ToString();
            if (dal.Withdraw(transaction,account))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Deposit(Account account, Transaction transaction)
        {
            TransactionDal dal = new TransactionDal(connectionString);
            var dateAndTime = DateTime.Now;

            transaction.TransactionDate = dateAndTime.Date.ToString();
            if (dal.Deposit(transaction,account))
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
