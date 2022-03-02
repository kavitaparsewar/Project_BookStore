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
    public class WishListRL : IWishListRL
    {
        public IConfiguration configuration { get; }
        public string ConnectionString = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=BookStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public WishListRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string AddWishList(WishListModel wishlistmodel)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("AddWishList", connection);
                    command.CommandType = CommandType.StoredProcedure;
                  
                    command.Parameters.AddWithValue("@UserId", wishlistmodel.UserId);
                    command.Parameters.AddWithValue("@BookId", wishlistmodel.BookId);

                    connection.Open();
                    int result = Convert.ToInt32(command.ExecuteScalar());
                    if (result == 2)
                    {
                        return "BookId not Existsss";
                    }
                    else if(result == 1)
                    {
                        return "Book already added into wishList";
                    }
                    else
                    {
                        return "Book Added successfully";
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
        public bool DeleteWishList(long WishListId)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand("spDeleteFromWishList", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@WishListId", WishListId);

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

        public List<WishListModel> GetAllWishList(int UserId)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("spGetAllFromWishList", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", UserId);
                    connection.Open();

                    SqlDataReader sqlData = command.ExecuteReader();
                    if (sqlData.HasRows)
                    {
                        List<WishListModel> wishlist = new List<WishListModel>();

                        while (sqlData.Read())
                        {
                            WishListModel wish = new WishListModel();
                            BookModel bookmodel = new BookModel();
                            bookmodel.BookName = sqlData["BookName"].ToString();
                            bookmodel.AuthorName = sqlData["AuthorName"].ToString();
                            bookmodel.DiscountPrice = Convert.ToInt32(sqlData["DiscountPrice"]);
                            bookmodel.OriginalPrice = Convert.ToInt32(sqlData["OriginalPrice"]);
                            bookmodel.BookImage = sqlData["BookImage"].ToString();
                            wish.UserId = Convert.ToInt32(sqlData["UserId"]);
                            wish.BookId = Convert.ToInt32(sqlData["BookId"]);
                            wish.bookmodel = bookmodel;
                            wishlist.Add(wish);
                        }

                        return wishlist;
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
