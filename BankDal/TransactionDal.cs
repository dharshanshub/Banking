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

    public class TransactionDal : BaseDataAccess
    {
        public TransactionDal(string connectionString) : base(connectionString) { }

        public bool FundTransfer(Transaction t)
        {
            string id="";

           /* CreateConnection();*/
          
            string sql1 = $"insert into Transactions(SenderAccNo,ReceiverAccNo,BrCode,TrDate,TrAmount,TrType,Description) values(@AccNo,@ReceiverAccNo,@BrCode,@TrDate,@TrAmount,@TrType,@Description)";
            string sql2 = $"update Accounts set Balance = Balance - @TrAmount where AccNo = @AccNO";
            string sql3 = $"update Accounts set Balance =Balance + @TrAmount where AccNo = @ReceiverAccNo";

            OpenConnection();
            SqlTransaction trans = connection.BeginTransaction();
            SqlCommand cmd1 = new SqlCommand(sql1, connection);
            SqlCommand cmd2 = new SqlCommand(sql2, connection);
            SqlCommand cmd3 = new SqlCommand(sql3, connection);

            cmd1.Parameters.AddWithValue("@AccNo", t.SenderAccNo);
            cmd1.Parameters.AddWithValue("@ReceiverAccNo", t.ReceiverAccNo);
            cmd1.Parameters.AddWithValue("@BrCode", t.Branchcode);
            cmd1.Parameters.AddWithValue("@TrDate", t.TransactionDate);
            cmd1.Parameters.AddWithValue("@TrAmount", t.TransactionAmount);
            cmd1.Parameters.AddWithValue("@TrType", t.TransactionType);
            cmd1.Parameters.AddWithValue("@Description", t.Description);
            cmd1.Transaction = trans;


            cmd2.Parameters.AddWithValue("@TrAmount", t.TransactionAmount);
            cmd2.Parameters.AddWithValue("@AccNo", t.SenderAccNo);
            cmd2.Transaction = trans;


            cmd3.Parameters.AddWithValue("@TrAmount", t.TransactionAmount);
            cmd3.Parameters.AddWithValue("@ReceiverAccNo", t.ReceiverAccNo);
            cmd3.Transaction = trans;

            SqlCommand cmd4 = connection.CreateCommand();
            cmd4.Transaction = trans;
            cmd4.CommandText = $"select @@identity";

            try
            {
                
                cmd2.ExecuteNonQuery();
                cmd3.ExecuteNonQuery();
                cmd1.ExecuteNonQuery();
               id = cmd4.ExecuteScalar().ToString();
                trans.Commit();
            }
            catch (Exception)
            {
                
                trans.Rollback();
                return false;
               
            }
            finally
            {
                CloseConnection();
            
            }
            return true;

        }
        public bool Withdraw(Transaction t)
        {
            CreateConnection();
            string sql1 = $"update Accounts set Balance = Balance - @TrAmount where AccNo = @AccNo";
            string sql2 = $"insert into Transactions (SenderAccNo, ReceiverAccNo,TrAmount,TrType,TrDate) values(689344, @AccNo, @TrAmount, 'Debit' ,@TrDate)";

            OpenConnection();

            SqlTransaction trans = connection.BeginTransaction();
            SqlCommand cmd1 = new SqlCommand(sql1, connection);
            SqlCommand cmd2 = new SqlCommand(sql2, connection);


            cmd1.Parameters.AddWithValue("@TrAmount", t.TransactionAmount);
            cmd1.Parameters.AddWithValue("@AccNo", t.ReceiverAccNo);
            cmd2.Parameters.AddWithValue("@TrAmount", t.TransactionAmount);
            cmd2.Parameters.AddWithValue("@AccNo", t.ReceiverAccNo);
            cmd2.Parameters.AddWithValue("@TrDate", t.TransactionDate);

            cmd1.Transaction = trans;
            cmd2.Transaction = trans;


            try
            {
                cmd1.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception)
            {
                trans.Rollback();
                return false;
            }
            finally
            {
                CloseConnection();
            }
            return true;

        }
        public bool Deposit(Transaction t)//Done
        {
            CreateConnection();
            string sql1 = $"update Accounts set Balance = Balance + @TrAmount where AccNo = @AccNo";
            string sql2 = $"insert into Transactions (SenderAccNo, ReceiverAccNo,TrAmount,TrType,TrDate) values(689344,@AccNo, @TrAmount, 'Credit' ,@TrDate)";
            string sql3 = $"update Accounts set Balance = Balance - @TrAmount where AccNo = 689344";

            OpenConnection();

            SqlTransaction trans = connection.BeginTransaction();
            SqlCommand cmd1 = new SqlCommand(sql1, connection);
            SqlCommand cmd2 = new SqlCommand(sql2, connection);
            SqlCommand cmd3 = new SqlCommand(sql3, connection);


            cmd1.Parameters.AddWithValue("@TrAmount", t.TransactionAmount);
            cmd1.Parameters.AddWithValue("@AccNo", t.ReceiverAccNo);
            cmd2.Parameters.AddWithValue("@AccNo", t.ReceiverAccNo);
            cmd2.Parameters.AddWithValue("@TrAmount", t.TransactionAmount);
            cmd2.Parameters.AddWithValue("@TrDate", t.TransactionDate);
            cmd3.Parameters.AddWithValue("@TrAmount", t.TransactionAmount);

            cmd1.Transaction = trans;
            cmd2.Transaction = trans;
            cmd3.Transaction = trans;


            try
            {
                cmd1.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                cmd3.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception)
            {
                trans.Rollback();
                return false;
            }
            finally
            {
                CloseConnection();
            }
            return true;

        }
        public List<Transaction> ViewStatement(Transaction transaction)//Done
        {
            try
            {
                CreateConnection();
                string sql = $"select * from Transactions where SenderAccNo = @AccNo";
                string sql1 = $"select * from Transactions where ReceiverAccNo=@AccNo";
                OpenConnection();

                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlCommand cmd1 = new SqlCommand(sql1, connection);

                cmd.Parameters.AddWithValue("@AccNo", transaction.SenderAccNo);
                cmd1.Parameters.AddWithValue("@AccNo", transaction.SenderAccNo);

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
        }
    }
}
