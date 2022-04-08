using BankEntity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDal
{
    public class BeneficiaryDal
    {
        SqlConnection cn;
        public BeneficiaryDal()
        {
            cn = new SqlConnection();


        }
        public bool AddBeneficiary(Beneficiary b)
        {
            try
            {
                string sql = $"insert into Beneficiary '({b.SenderAccNo}','{b.ReceiverAccNo}','{b.NickName}','{b.BranchName}', '{b.IFSC}')";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cn.Close();
            }

        }

        public bool DeleteBeneficiary(Beneficiary beneficiary)
        {
            try
            {
                string sql = $"DELETE FROM Beneficiary WHERE Payee_Account_Number={beneficiary.ReceiverAccNo}";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cn.Open();

                int i = cmd.ExecuteNonQuery();
                if (i == 1) { return true; }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }
    }
}
