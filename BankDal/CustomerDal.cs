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
        public bool UpdateUsers(Customer customer, Account account)
        {
            try
            {
                string sql = $"Update Customers set IBPwd = '{ customer.IbPassword }', Address = '{customer.Address}', Email = '{customer.Email}', MobileNo = '{customer.MobileNo}' where Accounts.AccNo = {account.AccNo} where exists(select Accounts.AccNo from Accounts where AccNo = {account.AccNo})";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cn.Open();
                int i = cmd.ExecuteNonQuery();


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
