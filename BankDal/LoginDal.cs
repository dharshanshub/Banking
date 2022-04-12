using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankEntity;
using Microsoft.Data.SqlClient;

namespace BankDal
{
    public class LoginDal : BaseDataAccess
    {
        public LoginDal(string connectionString) : base(connectionString) { }

        /* public  List<Login> UserLogin(Login login)
         {
             try
             {
                 //string sql = $"Update Customers set IBPwd =@IbPwd,Address=@Address,Email=@Email,MobileNo=@MobileNo where CRN=(select CRN  From Accounts where Accounts.AccNo=@AccNo)"; 
                 CreateConnection();
                 string sql = $"select IBPwd from Customers where CRN = (select CRN From Accounts where Accounts.AccNo=@AccNo)";
                 string sql1 = $"select AccNo from Customers where AccNo=@AccNo";
                 OpenConnection();

                 SqlCommand cmd = new SqlCommand(sql, connection);
                 SqlCommand cmd1 = new SqlCommand(sql1, connection);

                 cmd.Parameters.AddWithValue("@AccNo", login.AccNo);
                 cmd1.Parameters.AddWithValue("@AccNo", login.AccNo);

                 SqlDataReader dr = cmd.ExecuteReader();

                 List<Transaction> t_list = new List<Transaction>();

                 while (dr.Read())
                 {
                     Transaction t = new Transaction();

                     t.TransactionId = (long)dr[0];
                     t.SenderAccNo = (long)dr[1];
                     t.ReceiverAccNo = (long)dr[2];
                     t.Branchcode = dr[3].ToString();
                     t.TransactionDate = dr[4].ToString();
                     t.TransactionAmount = double.Parse(dr[5].ToString());
                     t.TransactionType = "Debit";
                     t.Description = dr[7].ToString();
                     t_list.Add(t);
                 }
                 dr.Close();
                 SqlDataReader dr1 = cmd1.ExecuteReader();
                 while (dr1.Read())
                 {
                     Transaction j = new Transaction();
                     j.TransactionId = (long)dr1[0];
                     j.SenderAccNo = (long)dr1[1];
                     j.ReceiverAccNo = (long)dr1[2];
                     j.Branchcode = dr1[3].ToString();
                     j.TransactionDate = dr1[4].ToString();
                     j.TransactionAmount = double.Parse(dr1[5].ToString());
                     j.TransactionType = "Credit";
                     j.Description = dr1[7].ToString();
                     t_list.Add(j);
                 }
                 dr1.Close();

                 return t_list;
             }
             catch (Exception)
             {
                 throw;
             }
             finally
             {
                 CloseConnection();
             }
         */
        public bool AdminLogin(LoginViewModel login)
        {
            CreateConnection();
            string sql = $"select * from Admin";
            OpenConnection();
            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader dr = cmd.ExecuteReader();
            List<Login> t_list = new List<Login>();
            while (dr.Read())
            {
                Login lg = new Login();

                lg.AccNo = (long)dr[0];
                lg.Password = dr[1].ToString();
               
                      t_list.Add(lg);
              

            }
            dr.Close();
            int log = 0;
            foreach (var t in t_list)
            {
                if (t.AccNo == login.AccountNo && t.Password == login.Password)
                {
                    log = 1;  
                }
               
                    
            }
            if(log == 1)
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

