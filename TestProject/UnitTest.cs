using BankDal;
using BankEntity;
using NUnit.Framework;
using System;

namespace TestProject
{


    public class Tests
    {
        string connectionString = @"Data Source=LAPTOP-NKUJCDUA\SQLEXPRESS;database=Bank;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //[SetUp]
        //public void Setup()
        //{
        //}

        //customerdal

        [Test]

        public void CreateNewuserTest()
        {
            var p = new Customer
            {

                Name = "dharshan",
                Email = "dharshan@gmai.com",
                BirthDate = "03/11/2000",
                IbPassword = "dasdsd",
                TransactionPwd = "asdasdas",
                Address = "chennai",
                MobileNo = "9884061001"
            };
            CustomerDal d = new CustomerDal(connectionString);
            d.CreateNewUser(p);

            var er = 5;
            var ac = d.GetALlUser().Count;
            Assert.AreEqual(er, ac);
        }

        [Test]
        public void UpdateUsersTest()
        {
            var p = new Customer
            {


                Email = "hello@gmail.com",

                IbPassword = "dasdsd",

                Address = "chennai",
                MobileNo = "9884061001"
            };
            var c = new Account
            {
                AccNo = 689341


            };
            CustomerDal dal = new CustomerDal(connectionString);
            bool er;
            if(dal.UpdateUsers(p, c)) {  er= true; } else { er= false; }
            Assert.IsTrue(er);
        }
        //accountdal
        [Test]
        public void CreateNewUseraccTest()
        {
            var c = new Customer
            {
                CRN = 242
            };

            var p = new Account
            {

               
                
                 Balance = 3233,
                OpenDate="03/11/2000",
               Status = "Active",
                
                
            };
            AccountDal dal = new AccountDal(connectionString);
            dal.CreateNewUser(p, c);
            var er = 3;
            var ac=dal.GetALlAccounts().Count;
            Assert.AreEqual(er,ac);

        }



    }
}
