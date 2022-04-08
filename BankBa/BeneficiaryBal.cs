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
        public bool AddBeneficiary(Beneficiary beneficiary)
        {
            BeneficiaryDal dal = new BeneficiaryDal();
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
           BeneficiaryDal dal = new BeneficiaryDal();
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
