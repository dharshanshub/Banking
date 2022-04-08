using BankDal;
using BankEntity;
using NUnit.Framework;
using System;

namespace TestProject
{
    namespace TestProject
    {
        public class Tests
        {
            //[SetUp]
            //public void Setup()
            //{
            //}



            [Test]

            public void CreateNewuserTest()
            {
                var p = new Customer
                {
                    CRN = 123,
                    Name = "dharshan",
                    Email = "dharshan@gmai.com",
                    BirthDate = "03/11/2000",
                    IbPassword = "dasdsd",
                    TransactionPwd = "asdasdasdas",
                    Address = "chennai",
                    MobileNo = "9884061001"
                };
                CustomerDal d = new CustomerDal();

                var er = d.CreateNewUser(p);
                    var ac = true;
                Assert.AreEqual(er, ac);


               

            }
        }
    }
}
