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
    public class OrderRL : IOrderRL
    {
        public IConfiguration configuration { get; }
        public string ConnectionString = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=BookStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public OrderRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string AddOrders(OrderModel ordermodel)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("spAddOrders", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserId", ordermodel.UserId);
                    command.Parameters.AddWithValue("@AddId", ordermodel.AddId);
                    command.Parameters.AddWithValue("@BookId", ordermodel.BookId);
                    command.Parameters.AddWithValue("@QuantityToBuy", ordermodel.QuantityToBuy);

                    connection.Open();
                    int result = Convert.ToInt32(command.ExecuteScalar());

                    if (result == 2)
                    {
                        return "BookId not exists";
                    }
                    else if (result == 1)
                    {
                        return "Userid not exists";
                    }
                    else
                    {
                        return "Ordered successfully";
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }


        public List<OrderModel> GetOrder(int UserId)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("spGetAllOrders", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserId", UserId);
                    connection.Open();


                    SqlDataReader sqlData = command.ExecuteReader();
                    List<OrderModel> order = new List<OrderModel>();
                    if (sqlData.HasRows)
                    {
                        while (sqlData.Read())
                        {
                            OrderModel ordermodel = new OrderModel();
                            BookModel bookmodel = new BookModel();


                            bookmodel.BookName = sqlData["BookName"].ToString();
                            bookmodel.AuthorName = sqlData["AuthorName"].ToString();
                            bookmodel.DiscountPrice = Convert.ToInt32(sqlData["DiscountPrice"]);
                            bookmodel.OriginalPrice = Convert.ToInt32(sqlData["OriginalPrice"]);
                            bookmodel.BookImage = sqlData["Image"].ToString();
                            ordermodel.OrderId = Convert.ToInt32(sqlData["OrderId"]);
                            ordermodel.bookmodel = bookmodel;
                            order.Add(ordermodel);
                        }
                        return order;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

    }
}
