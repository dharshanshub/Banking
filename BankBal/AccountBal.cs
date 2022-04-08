using BankDal;
using BankEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankBal
{
    public class AccountBal
    {
        public bool CreateNewUser(Customer customer, Account account)
        {
            AccountDal dal = new AccountDal();

            account.Status = "Active";
            var dateAndTime = DateTime.Now;

            account.OpenDate = dateAndTime.Date.ToString();


            if (dal.CreateNewUser(account, customer))

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
