using BankDal;
using BankEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankBal
{
    public class LoginBal:BaseDataAccess
    {
        public LoginBal(string connectionString) : base(connectionString) { }

        string connectionString = @"Data Source=LAPTOP-NKUJCDUA\SQLEXPRESS;database=Bank;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public bool AdminLogin(LoginViewModel model)
        {
            LoginDal dal = new LoginDal(connectionString);
           if(dal.AdminLogin(model))
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
