using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using WebUNIFI.Pages.Clients;

namespace WebUNIFI.Pages.Products 
{
    public class ProductsModel : PageModel 
    {
        // Declaring a list to store ProductInfo objects
        public List<ProductInfo> listProducts = new List<ProductInfo>();

        public void OnGet() 
        {
            try 
            {
                // Defining the connection string to connect to the database
                String connectionString = "Data Source=BALTMORE\\SQLEXPRESS;Initial Catalog=AdventureWorksLT2019;User ID=sa;Password=********;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

                // Creating a connection object to connect to the database
                using (SqlConnection connection = new SqlConnection(connectionString)) 
                {
                    // Opening the connection to the database
                    connection.Open();
                    // Defining a SQL query to retrieve the most ordered product from the database
                    String sql = "SELECT TOP 1 p.ProductName, SUM(od.Quantity) AS TotalOrders FROM Products p JOIN OrderDetails od ON p.ProductID = od.ProductID GROUP BY p.ProductName ORDER BY TotalOrders DESC";

                    // Creating a command object to execute the SQL query
                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        // Executing the SQL query and creating a reader object to read the results
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            // Looping through the results of the SQL query
                            while (reader.Read())
                            {
                                ProductInfo productInfo = new ProductInfo(); // Creating a new ProductInfo object to store the retrieved data
                                productInfo.productName = reader.GetString(0); // Setting the product name property of the ProductInfo object to the first column of the result set
                                productInfo.totalOrders = reader.GetInt32(1);

                                listProducts.Add(productInfo); // Adding the ProductInfo object to the list of products
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                // Printing the exception message to the console
                Console.WriteLine("Exception: " + ex.ToString()); 
            }
        }
    }
    // Defining a class to store product information
    public class ProductInfo 
    {
        // Declaring a public property to store items
        public string productName; 
        public int totalOrders;
    }
}