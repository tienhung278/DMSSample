using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Pinewood.DMSSample.Business.Models;

namespace Pinewood.DMSSample.Business.Repositories.Concretes;

public class CustomerRepositoryDb : ICustomerRepositoryDb
{
    public Customer? GetByName(string name)
    {
        Customer? customer = null;

        var connectionString =
            ConfigurationManager.ConnectionStrings["appDatabase"].ConnectionString;

        using var connection = new SqlConnection(connectionString);
        var command = new SqlCommand
        {
            Connection = connection,
            CommandType = CommandType.StoredProcedure,
            CommandText = "CRM_GetCustomerByName"
        };

        var parameter = new SqlParameter("@Name", SqlDbType.NVarChar) { Value = name };
        command.Parameters.Add(parameter);

        connection.Open();
        var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
        while (reader.Read())
            customer = new Customer(
                (int)reader["CustomerID"],
                (string)reader["Name"],
                (string)reader["Address"]
            );

        return customer;
    }
}