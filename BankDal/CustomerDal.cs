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
    public  class CustomerDal
    {
        SqlConnection cn;
        public CustomerDal()
        {
            cn = new SqlConnection();

            cn.ConnectionString = ConfigurationManager.ConnectionStrings["Bank"].ConnectionString;

        }
        public bool CreateNewUser(Customer customer)
        {
            try
            {
                customer.CRN = 231;
                customer.Name = "dharshan";
                customer.TransactionPwd = "IBPwd123";
                customer.IbPassword = "dasdasdas";
                customer.Email = "sddfdsf";
                customer.Address = "chennai";
                customer.BirthDate = "3/11/2000";
                customer.MobileNo = "9884061001";
                string sql = $"insert into Customers(CRN,Name,TxnPwd,IBPwd,Email,Address,BirthDate,MobileNo) values ({customer.CRN},'{customer.Name}','{customer.TransactionPwd}','{customer.IbPassword}','{customer.Email}','{customer.Address}','{customer.BirthDate}','{customer.MobileNo}') ";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cn.Open();
               int i= cmd.ExecuteNonQuery();
                if (i == 0)
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
