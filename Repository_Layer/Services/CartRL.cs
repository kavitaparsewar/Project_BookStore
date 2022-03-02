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
    public class CartRL : ICartRL
    {
        public IConfiguration configuration { get; }
        public string ConnectionString = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=BookStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public CartRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string AddCart(CartModel cartmodel)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("spAddCart", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CartId", cartmodel.CartId);
                    command.Parameters.AddWithValue("@BookId", cartmodel.BookId);
                    command.Parameters.AddWithValue("@UserId", cartmodel.UserId);
                    command.Parameters.AddWithValue("@CartQuantity", cartmodel.CartQuantity);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return "Book Added in to cart";
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

        public string UpdateCart(CartModel cartmodel)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("spUpdateCart", connection);

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CartId", cartmodel.CartId);
                    command.Parameters.AddWithValue("@CartQuantity", cartmodel.CartQuantity);

                    connection.Open();

                    int result = Convert.ToInt32(command.ExecuteScalar());

                    if (result == 1)
                    {
                        return "Updation Fail";
                    }
                    else
                    {
                        return "Updated Cart Details";
                    }
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

        public bool DeleteCart(long CartId)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand("spUpdateCart", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CartId", CartId);

                connection.Open();
                var result = command.ExecuteNonQuery();
                if (result == 1)
                {
                    return true;
                }
                return false;
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

        public List<CartModel> GetAllCart()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("spGetAllFromCart", connection);

                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    SqlDataReader sqlData = command.ExecuteReader();
                    if (sqlData.HasRows)
                    {
                        List<CartModel> allBook = new List<CartModel>();

                        while (sqlData.Read())
                        {
                            CartModel cartmodel = new CartModel();
                            cartmodel.BookId = Convert.ToInt32(sqlData["BookId"]);
                            cartmodel.CartId = Convert.ToInt32(sqlData["CartId"]);
                            cartmodel.UserId = Convert.ToInt32(sqlData["UserId"]);

                            cartmodel.CartQuantity = Convert.ToInt32(sqlData["CartQuantity"]);
                            allBook.Add(cartmodel);
                        }

                        return allBook;
                    }
                    else
                    {
                        return null;
                    }
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
