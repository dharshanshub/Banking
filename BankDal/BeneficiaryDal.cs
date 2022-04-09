using BankEntity;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDal
{
    public class BeneficiaryDal : BaseDataAccess
    {

        public BeneficiaryDal(string connectionString) : base(connectionString) { }


        public bool AddBeneficiary(Beneficiary b)//updated
        {

            string sql = $"insert into Beneficiary values (@SenderAccNo, @ReceiverAccNo, @NickName, @BranchName, @IFSC)";
            OpenConnection();

            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@SenderAccNo", b.SenderAccNo);
            cmd.Parameters.AddWithValue("@ReceiverAccNo", b.ReceiverAccNo);
            cmd.Parameters.AddWithValue("@NickName", b.NickName);
            cmd.Parameters.AddWithValue("@BranchName", b.BranchName);
            cmd.Parameters.AddWithValue("@IFSC", b.IFSC);
            try
            {
                cmd.ExecuteNonQuery();
              
            }
            catch (Exception)
            {
                throw;
                return false;
            }
            finally
            {
                CloseConnection();
            }
            return true;
        }


        public bool DeleteBeneficiary(Beneficiary b) 
        {

            string sql = $"DELETE FROM Beneficiary WHERE Payee_Account_Number= @ReceiverAccNo";
            OpenConnection();
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@ReceiverAccNo", b.ReceiverAccNo);
            try
            {
                cmd.ExecuteNonQuery();
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

        public List<Beneficiary> GetAllBeneficiaries(Beneficiary HolderAccountNo)
        {

            try
            {
                string sql = " SELECT * from Beneficiary where Holder_Account_Number = @HolderAccountNo";
                OpenConnection();

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@HolderAccountNo", HolderAccountNo.SenderAccNo);


                SqlDataReader dr = cmd.ExecuteReader();
                List<Beneficiary> b_list = new List<Beneficiary>();

                while (dr.Read())
                {
                    Beneficiary b = new Beneficiary();

                    b.SenderAccNo = (long)dr[0];
                    b.ReceiverAccNo = (long)dr[1];
                    b.NickName = dr[2].ToString();
                    b.BranchName = dr[3].ToString();
                    b.IFSC = dr[4].ToString();
                    b_list.Add(b);
                }
                return b_list;

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