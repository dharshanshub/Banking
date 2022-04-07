using BankDal;
using BankEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankBal
{
    public  class CustomerBal
    {
        private static Random random = new Random();
        public Customer CreateNewUser(Customer customer)
        {
           CustomerDal  dal = new CustomerDal();
            Random rand = new Random();

            customer.CRN = rand.Next(200, 99999);
            customer.IbPassword = RandomString(10);
            customer.TransactionPwd = RandomString(10);

            dal.CreateNewUser(customer);
         
            return customer;
      
         


        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
