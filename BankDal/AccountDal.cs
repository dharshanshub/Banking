using BankEntity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDal
{
    public class AccountDal
    {
        SqlConnection cn;
        public AccountDal()
        {
            cn = new SqlConnection();

            cn.ConnectionString = ConfigurationManager.ConnectionStrings["Bank"].ConnectionString;

        }
        public bool CreateNewUser(Account account,Customer customer )
        {
            try
            {
                
                string sql = $"insert into Accounts(CRN,OpenDate,Status,Balance) values ('{customer.CRN}',{account.OpenDate},'{account.Status}','{account.Balance}') ";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cn.Open();
               int i = cmd.ExecuteNonQuery();
                if(i== 0)
                {
                    return false;
                }
                else
                {
                    return true;
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
