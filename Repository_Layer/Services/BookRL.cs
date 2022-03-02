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
    public class BookRL : IBookRL
    {
        public IConfiguration configuration { get; }
        public string ConnectionString = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=BookStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public BookRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string AddBook(BookModel bookmodel)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("spAddBook", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BookId", bookmodel.BookId);
                    command.Parameters.AddWithValue("@BookName", bookmodel.BookName);
                    command.Parameters.AddWithValue("@AuthorName", bookmodel.AuthorName);
                    command.Parameters.AddWithValue("@TotalRating", bookmodel.TotalRating);
                    command.Parameters.AddWithValue("@RatedCount", bookmodel.RatedCount);
                    command.Parameters.AddWithValue("@DiscountPrice", bookmodel.DiscountPrice);
                    command.Parameters.AddWithValue("@OriginalPrice", bookmodel.OriginalPrice);
                    command.Parameters.AddWithValue("@Description", bookmodel.Description);
                    command.Parameters.AddWithValue("@BookImage", bookmodel.BookImage);
                    command.Parameters.AddWithValue("@Quantity", bookmodel.Quantity);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return "Book Added";
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
        public string UpdateBook(BookModel bookmodel)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("spUpdateBook", connection);

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@BookId", bookmodel.BookId);
                    command.Parameters.AddWithValue("@BookName", bookmodel.BookName);
                    command.Parameters.AddWithValue("@AuthorName", bookmodel.AuthorName);
                    command.Parameters.AddWithValue("@TotalRating", bookmodel.TotalRating);
                    command.Parameters.AddWithValue("@RatedCount", bookmodel.RatedCount);
                    command.Parameters.AddWithValue("@DiscountPrice", bookmodel.DiscountPrice);
                    command.Parameters.AddWithValue("@OriginalPrice", bookmodel.OriginalPrice);
                    command.Parameters.AddWithValue("@Description", bookmodel.Description);
                    command.Parameters.AddWithValue("@BookImage", bookmodel.BookImage);
                    command.Parameters.AddWithValue("@Quantity", bookmodel.Quantity);

                    connection.Open();

                    int result = Convert.ToInt32(command.ExecuteScalar());

                    if (result == 1)
                    {
                        return "Updation Fail";
                    }
                    else
                    {
                        return "Updated Book Details";
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

        public bool DeleteBook(long BookId)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand("spDeleteBook", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@BookId", BookId);

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

        public List<BookModel> GetAllBook()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("spGetAllBook", connection);

                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    SqlDataReader sqlData = command.ExecuteReader();
                    if (sqlData.HasRows)
                    {
                        List<BookModel> allBook = new List<BookModel>();

                        while (sqlData.Read())
                        {
                            BookModel bookmodel = new BookModel();
                            bookmodel.BookId = Convert.ToInt32(sqlData["BookId"]);
                            bookmodel.BookName = sqlData["BookName"].ToString();
                            bookmodel.AuthorName = sqlData["AuthorName"].ToString();
                            bookmodel.TotalRating = Convert.ToInt32(sqlData["TotalRating"]);
                            bookmodel.RatedCount = Convert.ToInt32(sqlData["RatedCount"]);
                            bookmodel.DiscountPrice = Convert.ToInt32(sqlData["DiscountPrice"]);
                            bookmodel.OriginalPrice = Convert.ToInt32(sqlData["OriginalPrice"]);
                            bookmodel.Description = sqlData["Description"].ToString();
                            bookmodel.BookImage = sqlData["BookImage"].ToString();
                            bookmodel.Quantity = Convert.ToInt32(sqlData["Quantity"]);
                            allBook.Add(bookmodel);
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
