using BankDal;
using BankEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankBal
{
    public class BeneficiaryBal : BaseDataAccess
    {
        public BeneficiaryBal(string connectionString) : base(connectionString) { }

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

        public bool DeleteBeneficiary(long id)
        {
            BeneficiaryDal dal = new BeneficiaryDal(connectionString);
            Beneficiary b = new Beneficiary();
            b.ReceiverAccNo = id;
            if (dal.DeleteBeneficiary(b))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public List<Beneficiary> ShowAllBeneficiaries(long id)
        {
            BeneficiaryDal dal = new BeneficiaryDal(connectionString);
            Beneficiary b = new Beneficiary();
            b.SenderAccNo = id;

            List<Beneficiary> list = dal.GetAllBeneficiaries(b);
            return list;

        }

    }
}
