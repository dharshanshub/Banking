using BankDal;
using BankEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankBal
{
    public  class CustomerBal:BaseDataAccess
    {
        public CustomerBal(string connectionString) : base(connectionString) { }
    
        string connectionString = @"Data Source=LAPTOP-NKUJCDUA\SQLEXPRESS;database=Bank;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private static Random random = new Random();
        public bool  CreateNewUser(Customer customer)
        {
           CustomerDal  dal = new CustomerDal(connectionString);
       
            customer.IbPassword = RandomString(10);
          

           string id= dal.CreateNewUser(customer);
            AccountBal bal = new AccountBal(connectionString);
            if(bal.CreateNewUser(id))
            {
                return true;
            }
            else { return false; }
           
     
        }
        public List<Customer> ShowAllCustomers()
        {
            CustomerDal dal = new CustomerDal(connectionString);
            List<Customer> list= dal.GetALlUser();
            return list;

        }
        public bool UpdateUsers(Customer customer)
        {
            CustomerDal dal = new CustomerDal(connectionString);
          
        
            if (dal.UpdateUsers(customer))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public List<Credential> GenrateCredentials(Customer customer)
        {
            CustomerDal dal = new CustomerDal(connectionString);
            List<Credential> cust= dal.GenrateCredentials(customer);
         
            return cust;         

        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
