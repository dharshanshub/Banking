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
    public class TransactionDal
    {
        SqlConnection cn;
        public TransactionDal()
        {
            cn = new SqlConnection();

            cn.ConnectionString = ConfigurationManager.ConnectionStrings["Bank"].ConnectionString;

        }
        public bool FundTransfer(Transaction t, Account a)
        {
            try
            {


                string sql = $"insert into Transactions(SenderAccNo,ReceiverAccNo,BrCode,TrDate,TrAmount,TrType,Description) values ({a.AccNo},'{t.ReceiverAccNo}','{t.Branchcode}','{t.TransactionDate}','{t.TransactionAmount}','{t.TransactionType}','{t.Description}')";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cn.Open();

                int i = cmd.ExecuteNonQuery();
                sql = $"update Accounts set Balance = Balance-{t.TransactionAmount} where AccNumber = {a.AccNo}";
                cmd = new SqlCommand(sql, cn);
                i += cmd.ExecuteNonQuery();

                sql = $"update Accounts set Balance =Balance+{t.TransactionAmount} where AccNumber = {t.ReceiverAccNo}";
                cmd = new SqlCommand(sql, cn);
                i += cmd.ExecuteNonQuery();

                if (i == 3)
                {
                    return true;
                }
                else
                {
                    return false;
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
        public List<Transaction> ViewStatement(Account Account)
        {
            try
            {
                string sql = $"select * from Transactions where AccNo = {Account.AccNo}and ReceiverAccNo={Account.AccNo}";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                List<Transaction> t_list = new List<Transaction>();

                while (dr.Read())
                {
                    Transaction t = new Transaction();

                    t.TransactionId = (long)dr[0];
                    t.ReceiverAccNo = (long)dr[2];
                    t.Branchcode = dr[3].ToString();
                    t.TransactionDate = dr[4].ToString();
                    t.TransactionAmount = (long)dr[5];
                    t.TransactionType = dr[6].ToString();
                    t.Description = dr[7].ToString();
                    t_list.Add(t);
                }

                return t_list;
            }catch (Exception ex)
            {
                throw;
            }
            finally { cn.Close(); }
        }
        public bool Withdraw(long AccountNo, long amount,string date)
        {
            try
            {
                string sql = $"update Accounts set Balance = Balance - {amount} where AccNo = {AccountNo}";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cn.Open();
                int i = cmd.ExecuteNonQuery();

                sql = $"insert into Transactions (AccNo,TrAmount,TrType,TrDate) values({AccountNo},{amount},'Debit',{date}))";
                cmd = new SqlCommand(sql, cn);
                i += cmd.ExecuteNonQuery();
                if (i == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            } catch (Exception ex)
            {
                throw;
            }
            finally { cn.Close(); }

        }
        public bool Deposit(long AccountNo, long amount,string date)
        {
            try
            {
                string sql = $"update Accounts set Balance = Balance + {amount} where AccNumber = {AccountNo}";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cn.Open();
                int i = cmd.ExecuteNonQuery();

                sql = $"insert into Transactions (AccNo,TrAmount,TrType,TrDate) values({AccountNo},{amount},'Credit',{date}))";
                cmd = new SqlCommand(sql, cn);
                i += cmd.ExecuteNonQuery();
                if (i == 2) { return true; } else { return false; }

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
