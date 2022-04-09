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
        string connectionString = @"Data Source=LAPTOP-NKUJCDUA\SQLEXPRESS;Initial Catlog=Bank;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private static Random random = new Random();
        public string  CreateNewUser(Customer customer, Account account)
        {
           CustomerDal  dal = new CustomerDal(connectionString);
          /*  Random rand = new Random();

            customer.CRN = rand.Next(200, 99999);*/
            customer.IbPassword = RandomString(10);
            customer.TransactionPwd = RandomString(10);

           string id= dal.CreateNewUser(customer);
            
            AccountDal dal1 = new AccountDal(connectionString);
            account.CRN = Int32.Parse(id);

            account.Status = "Active";
            var dateAndTime = DateTime.Now;

            account.OpenDate = dateAndTime.Date.ToString();
            dal1.CreateNewUser(account,customer);



            return id; 
      
         


        }
        public bool UpdateUsers(Customer customer, Account account)
        {
            CustomerDal dal = new CustomerDal(connectionString);
            if (dal.UpdateUsers(customer, account))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

      /*  public List<Customer> GenrateCredentials(Customer customer)
        {
            CustomerDal dal = new CustomerDal(connectionString);
            List<Customer> cust= dal.GenrateCredentials(customer);
           
            
         
            return cust;
            
                  

        }*/
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
