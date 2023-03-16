using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using WebUNIFI.Pages.Products;

namespace WebUNIFI.Pages.Orders
{
    
    public class OrdersModel : PageModel
    {
        // Declaring variables
        public List<OrderInfo> listOrders = new List<OrderInfo>();
        public OrderInfo orderInfo = new OrderInfo();
        public String errorMessage = "";

       
        public void OnGet()
        {
            try
            {
                // Creating connection string to connect to database
                String connectionString = "Data Source=BALTMORE\\SQLEXPRESS;Initial Catalog=AdventureWorksLT2019;User ID=sa;Password=********;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

                // Creating SQL connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Opening SQL connection
                    connection.Open();

                    // Defining SQL query to retrieve order and product information
                    String sql = "SELECT p.ProductID, p.Name AS ProductName, p.Color, o.OrderID, o.OrderDate, o.Quantity, o.Name AS CustomerName, o.Email, o.Phone FROM SalesLT.Product AS p INNER JOIN SalesLT.SalesOrderDetail AS sod ON p.ProductID = sod.ProductID INNER JOIN SalesLT.SalesOrderHeader AS o ON sod.SalesOrderID = o.SalesOrderID;";

                    // Creating SQL command object
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // Retrieving data from database using SqlDataReader
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Iterating through the retrieved data and adding it to listOrders
                            while (reader.Read())
                            {
                                OrderInfo orderInfo = new OrderInfo();
                                orderInfo.productName = reader.GetString(0);
                                orderInfo.productDescription = reader.GetString(1);
                                listOrders.Add(orderInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Printing exception if error occurs
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    // Defining class OrderInfo
    public class OrderInfo
    {
        // Declaring variables
        public string productName;
        public string productDescription;
    }
}
