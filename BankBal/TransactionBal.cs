using BankDal;
using BankEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankBal
{
    public class TransactionBal:BaseDataAccess
    {
        public TransactionBal(string connectionString) : base(connectionString) { }
        string connectionString = @"Data Source=LAPTOP-NKUJCDUA\SQLEXPRESS;database=Bank;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public string FundTransfer(Transaction t)
        {
            TransactionDal dal = new TransactionDal(connectionString);

            var dateAndTime = DateTime.Now;

            t.TransactionDate = dateAndTime.Date.ToString();

            string id = dal.FundTransfer(t);
           return id;


        }
        public List<Transaction> ViewStatement(Account Account)
        {
            TransactionDal dal = new TransactionDal(connectionString);

            List<Transaction> list = dal.ViewStatement(Account);
            return list;
        }

        public bool Withdraw( Transaction transaction)
        {
            TransactionDal dal = new TransactionDal(connectionString);
            var dateAndTime = DateTime.Now;

         transaction.TransactionDate = dateAndTime.Date.ToString();
            if (dal.Withdraw(transaction))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Deposit( Transaction transaction)
        {
            TransactionDal dal = new TransactionDal(connectionString);
            var dateAndTime = DateTime.Now;

            transaction.TransactionDate = dateAndTime.Date.ToString();
            if (dal.Deposit(transaction))
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
