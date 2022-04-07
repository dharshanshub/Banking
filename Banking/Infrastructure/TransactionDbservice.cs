using Banking.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace Banking.Infrastructure
{
    public class TransactionDbservice : ITransactionService
    {
        SqlConnection connection;
        const string connectionString = "Data Source=LAPTOP-NKUJCDUA\\SQLEXPRESS;Initial Catalog=Bank;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public TransactionDbservice()
        {
            connection = new SqlConnection(connectionString);
        }

        public bool FundTransfer(TransactionModel model)
        {
            try
            {
                string sql = $"insert into Transactions values({model.Sender},{model.Receiver},'{model.TransactionId}','{model.Date}',{model.Amount})";
                SqlCommand cmd = new SqlCommand(sql, connection);
                 connection.Open();
      
           

                 cmd.ExecuteNonQuery();


                sql = $"update Customer_Info set Balance =Balance-{model.Amount} where AccNumber = {model.Sender}";
                cmd = new SqlCommand(sql, connection);
                cmd.ExecuteNonQuery();

                sql = $"update Customer_Info set Balance =Balance+{model.Amount} where AccNumber = {model.Receiver}";
                cmd = new SqlCommand(sql, connection);
                cmd.ExecuteNonQuery();


                return true;
            }
            
            catch (SqlException ex)
            {
                return false;
                throw;
               
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }



        }
    }
}
