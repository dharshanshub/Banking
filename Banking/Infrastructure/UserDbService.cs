using Banking.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace Banking.Infrastructure
{
    public class UserDbService : IUserService
    {
        SqlConnection connection;
        const string connectionString = "Data Source=LAPTOP-NKUJCDUA\\SQLEXPRESS;Initial Catalog=Bank;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public UserDbService()
        {
            connection = new SqlConnection(connectionString);
        }
        private User _loggedInUser;
        public List<User> Authenticate(LoginViewModel model)
        {
            string sql = "SELECT * from Customer_Info WHERE AccNumber ='" + model.AccNumber + "' AND IBPassword='" + model.Password + "'";

            List<User> users = new List<User>();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = System.Data.CommandType.Text;
            try

            {
                if (connection.State != System.Data.ConnectionState.Open) connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new User
                    {
                        AccNumber = (long)reader.GetInt64(0),
                        Password = reader.GetString(2),

                        FirstName = reader.GetString(6),
                        Status = reader.GetString(10),







                    });



                    var user = users.FirstOrDefault(
                u => u.AccNumber == model.AccNumber && u.Password == model.Password
                );
                    _loggedInUser = user;
                    return users;

                }

                return users;


                if (!reader.IsClosed) reader.Close();
                if (connection.State != System.Data.ConnectionState.Closed) connection.Close();

            }
            catch (System.Exception ex)
            {
                throw;
            }



        }
        public User LoggedInUser { get => _loggedInUser; }
        public bool TransactionAuthenticate( Customer customer)
        {
            string sql = "SELECT * from Customer_Info WHERE AccNumber ='" + customer.AccNumber + "' AND TxnPassword='" + customer.TxnPassword + "'";

            List<User> users = new List<User>();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = System.Data.CommandType.Text;
            try

            {
                if (connection.State != System.Data.ConnectionState.Open) connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }

                if (!reader.IsClosed) reader.Close();
                if (connection.State != System.Data.ConnectionState.Closed) connection.Close();

            }
            catch (System.Exception ex)
            {
                throw;
            }

        }
    }
}

