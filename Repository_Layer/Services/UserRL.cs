using Common_Layer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        public string EncryptPassword(string password)
        {
            try
            {
                byte[] encode = new byte[password.Length];
                encode = Encoding.UTF8.GetBytes(password);
                string encPassword = Convert.ToBase64String(encode);
                return encPassword;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string DecryptPassword(string encryptpwd)
        {
            try
            {
                UTF8Encoding encodepwd = new UTF8Encoding();
                Decoder Decode = encodepwd.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
                int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string decryptpwd = new String(decoded_char);
                return decryptpwd;
            }
            catch (Exception)
            {

                throw;
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
        public string TokenForId(long Id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("Id", Id.ToString()) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateJwtToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("Email", email) }),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public string ForgetPassword(string EmailId)
        {
            try
            {
                // var chkemail = context.Users.FirstOrDefault(e => e.Email == email);

                if (EmailId != null)
                {
                    var token = GenerateJwtToken(EmailId);

                    new MSMQModel().MsmqSender(token);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ResetPassword(string EmailId, string password, string confirmPassword)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
               // login.UserPassword = EncryptPassword(login.UserPassword);

                SqlCommand command = new SqlCommand("spResetPassword", connection);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@EmailId",EmailId);
                command.Parameters.AddWithValue("@Password",EncryptPassword(password));

                connection.Open();
                var result = command.ExecuteNonQuery();
                if (result == 1)
                 { 
                   return true;
                  }
                else
                {
                    return false;
                }
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
    }
}
