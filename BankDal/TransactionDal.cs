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

        public bool FundTransfer(Transaction t, Account a)//done
        {
            CreateConnection();

            string sql1 = $"insert into Transactions(SenderAccNo,ReceiverAccNo,BrCode,TrDate,TrAmount,TrType,Description) values(@AccNo,@ReceiverAccNo,@BrCode,@TrDate,@TrAmount,@TrType,@Description)";
            string sql2 = $"update Accounts set Balance = Balance - @TrAmount where AccNumber = @AccNo";
            string sql3 = $"update Accounts set Balance =Balance + @TrAmount where AccNumber = @AccNo";

            OpenConnection();
            SqlTransaction trans = connection.BeginTransaction();
            SqlCommand cmd1 = new SqlCommand(sql1, connection);
            SqlCommand cmd2 = new SqlCommand(sql2, connection);
            SqlCommand cmd3 = new SqlCommand(sql3, connection);

            cmd1.Parameters.AddWithValue("@AccNo", a.AccNo);
            cmd1.Parameters.AddWithValue("@ReceiverAccNo", t.ReceiverAccNo);
            cmd1.Parameters.AddWithValue("@BrCode", t.Branchcode);
            cmd1.Parameters.AddWithValue("@TrDate", t.TransactionDate);
            cmd1.Parameters.AddWithValue("@TrAmount", t.TransactionAmount);
            cmd1.Parameters.AddWithValue("@TrType", t.TransactionType);
            cmd1.Parameters.AddWithValue("@Description", t.Description);
            cmd1.Transaction = trans;


            cmd2.Parameters.AddWithValue("@TrAmount", t.TransactionAmount);
            cmd2.Parameters.AddWithValue("@AccNo", a.AccNo);
            cmd2.Transaction = trans;


            cmd3.Parameters.AddWithValue("@TrAmount", t.TransactionAmount);
            cmd3.Parameters.AddWithValue("@AccNo", a.AccNo);
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
            }
            finally
            {
                CloseConnection();
            }
            return true;
        }
        public bool Withdraw(Transaction t, Account a)//done
        {
            CreateConnection();
            string sql1 = $"update Accounts set Balance = Balance - @TrAmount where AccNo = @AccNo";
            string sql2 = $"insert into Transactions (AccNo,TrAmount,TrType,TrDate) values(@AccNo, @TrAmount, 'Debit' ,@TrDate)";

            OpenConnection();

            SqlTransaction trans = connection.BeginTransaction();
            SqlCommand cmd1 = new SqlCommand(sql1, connection);
            SqlCommand cmd2 = new SqlCommand(sql2, connection);


            cmd1.Parameters.AddWithValue("@TrAmount", t.TransactionAmount);
            cmd1.Parameters.AddWithValue("@AccNo", a.AccNo);
            cmd2.Parameters.AddWithValue("@TrAmount", t.TransactionAmount);
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
            }
            finally
            {
                CloseConnection();
            }
            return true;

        }
        public bool Deposit(Transaction t, Account a)//Done
        {
            CreateConnection();
            string sql1 = $"update Accounts set Balance = Balance + @TrAmount where AccNo = @AccNo";
            string sql2 = $"insert into Transactions (AccNo,TrAmount,TrType,TrDate) values(@AccNo, @TrAmount, 'Credit' ,@TrDate)";

            OpenConnection();

            SqlTransaction trans = connection.BeginTransaction();
            SqlCommand cmd1 = new SqlCommand(sql1, connection);
            SqlCommand cmd2 = new SqlCommand(sql2, connection);


            cmd1.Parameters.AddWithValue("@TrAmount", t.TransactionAmount);
            cmd1.Parameters.AddWithValue("@AccNo", a.AccNo);
           
            cmd2.Parameters.AddWithValue("@TrAmount", t.TransactionAmount);
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
            }
            finally
            {
                CloseConnection();
            }
            return true;

        }
        public List<Transaction> ViewStatement(Account Account)//Done
        {
            try
            {
                CreateConnection();
                string sql = $"select * from Transactions where AccNo = @AccNo";
                string sql1 = $"select * from Transactions where ReceiverAccNo=@AccNo";
                OpenConnection();

                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlCommand cmd1 = new SqlCommand(sql1, connection);

                SqlDataReader dr = cmd.ExecuteReader();
                SqlDataReader dr1 = cmd1.ExecuteReader();
                List<Transaction> t_list = new List<Transaction>();

                while (dr.Read())
                {
                    Transaction t = new Transaction();

                    t.TransactionId = (long)dr[0];
                    t.ReceiverAccNo = (long)dr[2];
                    t.Branchcode = dr[3].ToString();
                    t.TransactionDate = dr[4].ToString();
                    t.TransactionAmount = (long)dr[5];
                    t.TransactionType = "Debit";
                    t.Description = dr[7].ToString();
                    t_list.Add(t);
                }
                while (dr1.Read())
                {
                    Transaction j = new Transaction();
                    j.TransactionId = (long)dr[0];
                    j.ReceiverAccNo = (long)dr[2];
                    j.Branchcode = dr[3].ToString();
                    j.TransactionDate = dr[4].ToString();
                    j.TransactionAmount = (long)dr[5];
                    j.TransactionType = "Credit";
                    j.Description = dr[7].ToString();
                    t_list.Add(j);
                }

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
