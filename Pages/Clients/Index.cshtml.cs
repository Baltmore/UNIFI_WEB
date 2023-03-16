using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace WebUNIFI.Pages.Clients
{
    public class IndexModel : PageModel
    {
        // This is a public list of ClientInfo objects that will store the client information retrieved from the database
        public List<ClientInfo> listClients = new List<ClientInfo>();
        // This method is called when the page is loaded
        public void OnGet()
        {
            try
            {
                // This is the connection string that specifies how to connect to the database
                String connectionString = "Data Source=BALTMORE\\SQLEXPRESS;Initial Catalog=AdventureWorksLT2019;User ID=sa;Password=********;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
                // This using statement creates a new SqlConnection object and automatically disposes of it when finished
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                   
                    connection.Open();
                    // This is the SQL query that will retrieve the client information from the database
                    String sql = "SELECT CustomerID, CompanyName, FirstName, LastName, AddressLine1, City, PostalCode, CountryRegionName FROM SalesLT.Customer INNER JOIN SalesLT.CustomerAddress ON SalesLT.Customer.CustomerID = SalesLT.CustomerAddress.CustomerID INNER JOIN SalesLT.Address ON SalesLT.CustomerAddress.AddressID = SalesLT.Address.AddressID INNER JOIN SalesLT.StateProvince ON SalesLT.Address.StateProvinceID = SalesLT.StateProvince.StateProvinceID INNER JOIN SalesLT.CountryRegion ON SalesLT.StateProvince.CountryRegionCode = SalesLT.CountryRegion.CountryRegionCode\", connection";
                    // This using statement creates a new SqlCommand object with the specified SQL query and connection, and automatically disposes of it when finished
                    using (SqlCommand command=new SqlCommand(sql, connection))
                    {
                        // This using statement creates a new SqlDataReader object that reads the results of the SQL query and automatically disposes of it when finished
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // This while loop reads each row of the query results and creates a new ClientInfo object with the retrieved data, which is then added to the listClients list
                            while (reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo(); // Creating a new ProductInfo object to store the retrieved data
                                clientInfo.CustomerID = "" + reader.GetInt32(0); // Setting the product name property of the ProductInfo object to the first column of the result set
                                clientInfo.NameStyle = reader.GetString(1);
                                clientInfo.Title = reader.GetString(2);
                                clientInfo.FirstName = reader.GetString(3);
                                clientInfo.MiddleName = reader.GetString(4);
                                clientInfo.LastName = reader.GetString(5);
                                clientInfo.Suffix = reader.GetString(6);
                                clientInfo.CompanyName = reader.GetString(7);
                                clientInfo.SalesPerson = reader.GetString(8);
                                clientInfo.EmailAddress = reader.GetString(9);
                                clientInfo.Phone = reader.GetString(10);
                                clientInfo.PasswordHash = reader.GetString(11);
                                clientInfo.PasswordSalt = reader.GetString(12);
                                clientInfo.rowguid= reader.GetString(13);
                                clientInfo.ModifiedDate = reader.GetDateTime(14).ToString();

                                listClients.Add(clientInfo);
                            }
                        }
                    }
                    
                }
            }
            catch(Exception ex)
            {
                // Printing the exception message to the console
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }
    // This is a public class that defines the properties of a ClientInfo object
    public class ClientInfo

    {
        // Declaring a public property to store items
        public String CustomerID;
        public String NameStyle;
        public String Title;
        public String FirstName;
        public String MiddleName;
        public String LastName;
        public String Suffix;
        public String CompanyName;
        public String SalesPerson;
        public String EmailAddress;
        public String Phone;
        public String PasswordHash;
        public String PasswordSalt;
        public String rowguid;
        public String ModifiedDate;



    }
}
