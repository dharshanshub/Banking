using BankDal;
using BankEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankBal
{
    public class AccountBal:BaseDataAccess
    {
        public AccountBal(string connectionString) : base(connectionString) { }
        string connectionString = @"Data Source=LAPTOP-NKUJCDUA\SQLEXPRESS;database=Bank;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public bool CreateNewUser(string id)
        {
            var dateAndTime = DateTime.Now;

            Account account = new Account();

            account.CRN = Int32.Parse(id);
            account.Status = "Active";
            account.OpenDate = dateAndTime.Date.ToString();
            account.Balance = 3000;
             
           
            AccountDal dal = new AccountDal(connectionString);
            if(dal.CreateNewUser(account))
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
