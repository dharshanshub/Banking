using BankEntity;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BankDal
{
    public class AccountDal : BaseDataAccess
    {
        public AccountDal(string connectionString) : base(connectionString) { }
        public bool CreateNewUser(Account account)//Create_User_Accounts_Table
        {
            string sql = $"insert into Accounts(CRN,OpenDate,Status,Balance) values (@CRN,@OpenDate,@Status,@Balance)";
            try
            {
                OpenConnection();
                ExecuteNonQuery(
                    sql, CommandType.Text,
                            new[] { new SqlParameter("@CRN", account.CRN), new SqlParameter("@OpenDate", account.OpenDate),
                                new SqlParameter("@Status",account.Status), new SqlParameter("@Balance", account.Balance)});
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally
            {
                
                CloseConnection();
               
            }
            return true;
        }
        public List<Account> GetALlAccounts()
        {

            int id;
            string sql = $"select * from Accounts";

            OpenConnection();

            SqlCommand cmd = new SqlCommand(sql, connection);
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                List<Account> t_list = new List<Account>();

                while (dr.Read())
                {
                    Account cus = new Account();
                    cus.CRN = (int)dr[1];
                    cus.AccNo = (long)dr[0];
                    cus.BranchCode = (string)dr[2];
                    cus. OpenDate= dr[3].ToString();
                    cus.Status = (string)dr[4];
                    cus.Balance = double.Parse(dr[5].ToString());
                    


                    t_list.Add(cus);
                }

                return t_list;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally { CloseConnection(); }



        }
        public bool FreezeAccount(Account account)
        {
            string sql = $"Update Accounts set Status='Freeze' where AccNo=@AccNo";
            //string sql = $"Update Customers set IBPwd =@IbPwd,Address=@Address,Email=@Email,MobileNo=@MobileNo where CRN=(select CRN  From Accounts where Accounts.AccNo=@AccNo)"; 

            OpenConnection();
            SqlCommand cmd = new SqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@AccNo", account.AccNo);
      
           
            try
            {
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
              
                return false;
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return true;
        }
    }
}


        
