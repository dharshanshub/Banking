using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDal
{
    public class BaseDataAccess
    {
        public const string DefaultConnectionString = @"Data Source=LAPTOP-NKUJCDUA\SQLEXPRESS;database=Bank;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        string connectionString;
        protected SqlConnection connection;
        public BaseDataAccess(string connStr)
        {
            if (string.IsNullOrEmpty(connStr))
                connectionString = DefaultConnectionString;
            else
                connectionString = connStr;

        }
        protected void CreateConnection()
        {
            if (connection == null)
                connection = new SqlConnection(connectionString);
        }
        protected void OpenConnection()
        {
            CreateConnection();
            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();
        }
        protected void CloseConnection()
        {
            if (connection is not null)
            {
                if (connection.State != System.Data.ConnectionState.Closed)
                    connection.Close();
            }
        }
        public SqlDataReader ExecuteReader(string sql, CommandType commandType, params SqlParameter[] parameters)
        {
            CreateConnection();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = commandType;
            foreach (var param in parameters)
            {
                cmd.Parameters.Add(param);
            }
            OpenConnection();
            return cmd.ExecuteReader();
        }
        public void ExecuteNonQuery(string sql, CommandType commandType, params SqlParameter[] parameters)
        {
            CreateConnection();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = commandType;
            foreach (var param in parameters)
            {
                cmd.Parameters.Add(param);
            }
            OpenConnection();
            cmd.ExecuteNonQuery();
            CloseConnection();
        }
    }
}
