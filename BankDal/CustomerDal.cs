using BankEntity;
using System;
using System.Collections.Generic;
using System.Configuration;

using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDal
{
    public class CustomerDal :BaseDataAccess
    {
        public CustomerDal(string connectionString) : base(connectionString) { }

        public string CreateNewUser(Customer customer)//Create_User_Customers_Table
        {

            string id;
            string sql = $"insert into Customers(Name,TxnPwd,IBPwd,Email,Address,BirthDate,MobileNo) values (@Name,@TxnPwd,@IBPwd,@Email,@Address,@BirthDate,@MobileNo)";

            OpenConnection();

            SqlTransaction trans = connection.BeginTransaction();

            SqlCommand cmd = new SqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@Name", customer.Name);
            cmd.Parameters.AddWithValue("@TxnPwd", customer.TransactionPwd);
            cmd.Parameters.AddWithValue("@IBPwd", customer.IbPassword);
            cmd.Parameters.AddWithValue("@Email", customer.Email);
            cmd.Parameters.AddWithValue("@Address", customer.Address);
            cmd.Parameters.AddWithValue("@BirthDate", customer.BirthDate);
            cmd.Parameters.AddWithValue("@MobileNo", customer.MobileNo);

            cmd.Transaction = trans;

            SqlCommand cmd2 = connection.CreateCommand();
            cmd2.Transaction = trans;
            cmd2.CommandText = $"select @@identity";

            try
            {
                cmd.ExecuteNonQuery();
                id = cmd2.ExecuteScalar().ToString();
                trans.Commit();
                customer.CRN = Int32.Parse(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return id;
            //asser.Greaterthan(id,0) 
        }
        public List<Customer> GetALlUser()
        {

            int id;
            string sql = $"select * from customers";

            OpenConnection();

            SqlCommand cmd = new SqlCommand(sql, connection);
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                List<Customer> t_list = new List<Customer>();

                while (dr.Read())
                {
                    Customer cus = new Customer();
                    cus.CRN = (int)dr[0];
                    cus.Name = (string)dr[1];
                    cus.BranchCode = (string)dr[2];
                    cus.TransactionPwd = (string)dr[3];
                    cus.IbPassword = (string)dr[4];
                    cus.Email = (string)dr[5];
                    cus.Address = (string)dr[6];
                    cus.BirthDate = dr[7].ToString();
                    cus.MobileNo = (string)dr[8];


                    t_list.Add(cus);
                }

                return t_list;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally { CloseConnection(); }









            //asser.Greaterthan(id,0) 
        }
     /*   public List<Customer> GenrateCredentials(Customer customer)
        {
            try { 
            string sql = $"Select CRN,TxnPwd,IBPwd,BrCode from Customers where MobileNo = @mobno";


            OpenConnection();

            SqlCommand cmd = new SqlCommand(sql, connection);


            cmd.Parameters.AddWithValue("@mobno", customer.MobileNo);



            SqlDataReader dr = cmd.ExecuteReader();

            List<Customer> cust = new List<Customer>();

            while (dr.Read())
            {
                Customer cus = new Customer();
                cus.CRN = (int)dr[0];
                cus.BranchCode = (string)dr[1];
                cus.TransactionPwd = (string)dr[3];
                cus.IbPassword = (string)dr[4];
                cust.Add(cus);
            }
                dr.Close();
                return cust;
            }catch (Exception ex)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
            

        }*/

        public bool UpdateUsers(Customer customer, Account account)
        {
          
            string sql = $"Update Customers set IBPwd =@IbPwd,Address=@Address,Email=@Email,MobileNo=@MobileNo where CRN=(select CRN  From Accounts where Accounts.AccNo=@AccNo)"; 

            OpenConnection();
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@IbPwd", customer.IbPassword);
            cmd.Parameters.AddWithValue("@Address", customer.Address);
            cmd.Parameters.AddWithValue("@Email", customer.Email);
            cmd.Parameters.AddWithValue("@MobileNo", customer.MobileNo);
            cmd.Parameters.AddWithValue("@AccNo", account.AccNo);

            try
            {
               cmd.ExecuteNonQuery();
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

