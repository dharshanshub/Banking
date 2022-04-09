using BankDal;
using BankEntity;
using NUnit.Framework;
using System;
using System.Collections.Generic;

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

                IbPassword = "broclesber",

                Address = "chennai",
                MobileNo = "9884061001"
            };
            var c = new Account
            {
                AccNo = 689341


            };
            CustomerDal dal = new CustomerDal(connectionString);
            
            bool er =dal.UpdateUsers(p, c);
            Assert.IsTrue(er);
        }
        //accountdal
      /*  
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

        }*/
        //
        /*public void FundTransferTest()
        {
            var t = new Transaction
            {
                ReceiverAccNo = 689342,
                Branchcode = "CHN001",
                TransactionAmount = 1660,
                TransactionDate = "09/08/2000",
                TransactionType = "Credit",
                Description="pay",
                

            };
            var a = new Account
            {
                AccNo = 689341,
            };
            TransactionDal dal = new TransactionDal(connectionString);
            bool er;
            if(dal.FundTransfer(t)) { er = true; } else { er = false; }
            Assert.IsTrue(er);*/
        //

        [Test]
        public void DepoitTest()
        {
            var t = new Transaction
            {
              
                Branchcode = "CHN001",
                TransactionAmount = 16660,
                TransactionDate = "09/08/2000",
             
            };
            var a = new Account
            {
                AccNo = 689341
            };
            TransactionDal dal = new TransactionDal(connectionString);
            bool er;
            if (dal.Deposit(t)) { er = true; } else { er = false; }
            Assert.IsTrue(er);
        }
        [Test]
        public void WithDrawTest()
        {
            var t = new Transaction
            {

                Branchcode = "CHN001",
                TransactionAmount = 1660,
                TransactionDate = "09/08/2000",

            };
            var a = new Account
            {
                AccNo = 689341
            };
            TransactionDal dal = new TransactionDal(connectionString);
            bool er;
            if (dal.Withdraw(t)) { er = true; } else { er = false; }
            Assert.IsTrue(er);
        }
        [Test]
        public void ViewStatement()
        {
            var a = new Account { AccNo = 689341 };
            TransactionDal dal = new TransactionDal(connectionString);
           List<Transaction> lst= dal.ViewStatement(a);
            Assert.IsNotNull(lst);
        }
        [Test]
        public void AddBeneficiaryTest()
        {
            var b = new Beneficiary
            {
                BranchName = "Chennai main",
                IFSC = "ZIGMA00702",
                NickName = "vijay",
                ReceiverAccNo = 689342,
                SenderAccNo = 689341,
                

            };
            BeneficiaryDal dal = new BeneficiaryDal(connectionString);
            dal.AddBeneficiary(b);
            dal.DeleteBeneficiary(b);

            var er = dal.GetAllBeneficiaries(b).Count;
            var ar = 0;
            Assert.AreEqual(er, ar);
        }
        [Test]
         public void GenrateCredentialsTest()
         {
            var a = new Customer { MobileNo="9884061001" };
            CustomerDal dal = new CustomerDal(connectionString);
            List<Customer1> lst = dal.GenrateCredentials(a);
            Assert.IsNotNull(lst);

        }


    }
}
