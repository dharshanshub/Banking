using BankEntity;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDal
{
    public class AccountDal:BaseDataAccess
    {

        public AccountDal(string connectionString) : base(connectionString) { }
        public bool CreateNewUser(Account account,Customer customer )
        {
            try
            {
                
                string sql = $"insert into Accounts(CRN,OpenDate,Status,Balance) values ('{customer.CRN}',{account.OpenDate},'{account.Status}','{account.Balance}') ";
                SqlCommand cmd = new SqlCommand();
                
               int i = cmd.ExecuteNonQuery(sql);
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
