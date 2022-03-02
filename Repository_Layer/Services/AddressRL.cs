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
    public class AddressRL : IAddressRL
    {
        public IConfiguration configuration { get; }
        public string ConnectionString = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=BookStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public AddressRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string AddAddress(AddressModel addressmodel)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("spAddAddress", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserId", addressmodel.UserId);
                    command.Parameters.AddWithValue("@FullAddress", addressmodel.FullAddress);
                    command.Parameters.AddWithValue("@City", addressmodel.City);
                    command.Parameters.AddWithValue("@state", addressmodel.State);
                    command.Parameters.AddWithValue("@AddTId", addressmodel.AddTId);

                    connection.Open();
                    int result = Convert.ToInt32(command.ExecuteScalar());
                    if (result == 1)
                    {
                        return "UserId not exists";
                    }
                    else
                    {
                        return "Address Added succssfully";
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
        public string UpdateAddress(AddressModel addressmodel)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("spUpdateAddress", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@AddId", addressmodel.AddId);
                command.Parameters.AddWithValue("@FullAddress", addressmodel.FullAddress);
                command.Parameters.AddWithValue("@City", addressmodel.City);
                command.Parameters.AddWithValue("@state", addressmodel.State);
                command.Parameters.AddWithValue("@AddTId", addressmodel.AddTId);

                    connection.Open();
                    int result = Convert.ToInt32(command.ExecuteScalar());
                    if (result == 1)
                    {
                        return "Address not exists";
                    }
                    else
                    {
                        return "Address updated succssfully";
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


        public List<AddressModel> GetAllAddress()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("GetAllAddress", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    
                    connection.Open();

                    SqlDataReader sqlData = command.ExecuteReader();
                    if (sqlData.HasRows)
                    {
                        List<AddressModel> alladdress = new List<AddressModel>();

                        while (sqlData.Read())
                        {
                            AddressModel addressmodel = new AddressModel();

                            addressmodel.AddId = Convert.ToInt32(sqlData["AddId"]);
                            addressmodel.FullAddress = sqlData["FullAddress"].ToString();
                            addressmodel.City = sqlData["City"].ToString();
                            addressmodel.State = sqlData["state"].ToString();
                            addressmodel.AddTId = Convert.ToInt32(sqlData["AddTId"]);
                            alladdress.Add(addressmodel);
                        }

                        return alladdress;
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
