using BankDal;
using BankEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankBal
{
    public class BeneficiaryBal
    {
        string connectionString = @"Data Source=LAPTOP-NKUJCDUA\SQLEXPRESS;database=Bank;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public bool AddBeneficiary(Beneficiary beneficiary)
        {
            BeneficiaryDal dal = new BeneficiaryDal(connectionString);
            if (dal.AddBeneficiary(beneficiary))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DeleteBeneficiary(Beneficiary beneficiary)
        {
           BeneficiaryDal dal = new BeneficiaryDal(connectionString);
            if (dal.DeleteBeneficiary(beneficiary))
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
