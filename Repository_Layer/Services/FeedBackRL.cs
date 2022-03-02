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
    public class FeedBackRL : IFeedBackRL
    {
        public IConfiguration configuration { get; }
        public string ConnectionString = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=BookStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public FeedBackRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }



        public string AddFeedback(FeedBackModel feedbackmodel)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("spAddFeedback", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserId", feedbackmodel.UserId);
                    command.Parameters.AddWithValue("@BookId", feedbackmodel.BookId);
                    command.Parameters.AddWithValue("@Comment", feedbackmodel.Comment);
                    command.Parameters.AddWithValue("@Ratings", feedbackmodel.Ratings);
                    connection.Open();
                    int result = Convert.ToInt32(command.ExecuteScalar());
                    if (result == 2)
                    {
                        return "BookId not exists";
                    }
                    else if (result == 1)
                    {
                        return "Already given feedback for this Book";
                    }
                    else
                    {
                        return "Feedback Added";
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

        public List<FeedBackModel> AllFeedBacks(int BookId)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("spGetAllFeedback", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@BookId", BookId);

                    connection.Open();
                    SqlDataReader sqlData = command.ExecuteReader();

                    List<FeedBackModel> feedbackmodel = new List<FeedBackModel>();
                    if (sqlData.HasRows)
                    {
                        while (sqlData.Read())
                        {
                            FeedBackModel feedbackModel = new FeedBackModel();
                            UserRegistration user = new UserRegistration();

                            user.FullName = sqlData["FullName"].ToString();
                            feedbackModel.Comment = sqlData["Comment"].ToString();
                            feedbackModel.Ratings = Convert.ToInt32(sqlData["Ratings"]);
                            feedbackModel.UserId = Convert.ToInt32(sqlData["UserId"]);

                            feedbackModel.User = user;
                            feedbackmodel.Add(feedbackModel);
                        }
                        return feedbackmodel;
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
