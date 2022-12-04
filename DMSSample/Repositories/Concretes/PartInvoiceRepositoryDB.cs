using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Pinewood.DMSSample.Business.Models;

namespace Pinewood.DMSSample.Business.Repositories.Concretes;

public class PartInvoiceRepositoryDb : IPartInvoiceRepositoryDb
{
    public void Add(PartInvoice invoice)
    {
        var connectionString =
            ConfigurationManager.ConnectionStrings["appDatabase"].ConnectionString;

        using var connection = new SqlConnection(connectionString);
        var command = new SqlCommand
        {
            Connection = connection,
            CommandType = CommandType.StoredProcedure,
            CommandText = "PMS_AddPartInvoice"
        };

        var stockCodeParameter = new SqlParameter("@StockCode", SqlDbType.VarChar, 50)
            { Value = invoice.StockCode };
        command.Parameters.Add(stockCodeParameter);
        var quantityParameter = new SqlParameter("@Quantity", SqlDbType.Int)
            { Value = invoice.Quantity };
        command.Parameters.Add(quantityParameter);
        var customerIdParameter = new SqlParameter("@CustomerID", SqlDbType.Int)
            { Value = invoice.CustomerId };
        command.Parameters.Add(customerIdParameter);

        connection.Open();
        command.ExecuteNonQuery();
    }
}