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

                string sql = $"insert into Transactions(AccNo,Sender_AccNo,BrCode,TrDate,TrAmount,TrType,Description) values ({a.AccNo},'{t.SenderAccNo}','{t.Branchcode}','{t.TransactionDate}','{t.TransactionAmount}','{t.TransactionType}','{t.Description}')";
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



























    }
}
