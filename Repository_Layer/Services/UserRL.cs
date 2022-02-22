using Common_Layer.Models;
using Microsoft.Extensions.Configuration;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Repository_Layer.Services
{
     public class UserRL : IUserRL
    {
        public IConfiguration configuration { get; }
        public string ConnectionString = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=BookStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public UserRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public UserRegistration Registration(UserRegistration user)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                //user.UserPassword = PasswordEncryption(user.UserPassword);
                SqlCommand command = new SqlCommand("SpAddUserDetails", connection);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FullName", user.FullName);
                command.Parameters.AddWithValue("@MobileNumber", user.MobileNumber);
                command.Parameters.AddWithValue("@EmailId", user.EmailId);
                command.Parameters.AddWithValue("@Password", user.Password);
                
                connection.Open();
                var Reader = command.ExecuteReader();        
                
                if (Reader.Read())
                {
                   
                    return user;
                }
                return null;
            }
            catch (ArgumentNullException exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public UserLogin Login(UserLogin login)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand("spLogin",connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmailId",login.EmailId);
                cmd.Parameters.AddWithValue("@Password",login.Password);

                connection.Open();

            var result = cmd.ExecuteReader();

            if (result.Read())
            {
                connection.Close();
                return login;
            }

            else
            {
                connection.Close();
                return null;
            }
                
                
        }
    }
}
