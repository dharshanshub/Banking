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

        public bool UserLogin(LoginViewModel login)
        {
            try
            {
                //string sql = $"Update Customers set IBPwd =@IbPwd,Address=@Address,Email=@Email,MobileNo=@MobileNo where CRN=(select CRN  From Accounts where Accounts.AccNo=@AccNo)"; 
                CreateConnection();
                string sql = $"select IBPwd from Customers where CRN = (select CRN From Accounts where Accounts.AccNo=@AccNo)";
                string sql1 = $"select AccNo from Accounts where AccNo=@AccNo";
                OpenConnection();

                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlCommand cmd1 = new SqlCommand(sql1, connection);

                cmd.Parameters.AddWithValue("@AccNo", login.AccountNo);
                cmd1.Parameters.AddWithValue("@AccNo", login.AccountNo);

                SqlDataReader dr = cmd.ExecuteReader();

                List<Login> t_list = new List<Login>();
                Login lg = new Login();
                while (dr.Read())
                {
                    
                    lg.Password = dr[0].ToString();

                
                    t_list.Add(lg);
                }
                dr.Close();
                SqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    lg.AccNo = (long)dr1[0];
                    t_list.Add(lg);
                }
                dr1.Close();

                int log = 0;
                foreach (var t in t_list)
                {
                    if (t.AccNo == login.AccountNo && t.Password == login.Password)
                    {
                        log = 1;
                    }


                }
                if (log == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }
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

