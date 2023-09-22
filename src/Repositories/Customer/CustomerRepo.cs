using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using Chinook.Src.Model;
using Chinook.Src.Exceptions;
using System.Data.SqlClient;

namespace Chinook.Src.Repositories.CustomerRepo
{
    internal class CustomerService : ICustomer
    {
        private SqlConnectionStringBuilder builder;

        public CustomerService()
        {
            builder = new SqlConnectionStringBuilder
            {
                DataSource = "localhost,1433",
                InitialCatalog = "Chinook",
                UserID = "sa",
                Password = "MyStrongPassword123",
                TrustServerCertificate = true,
            };
        }

        public List<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>();
            string connectionString = builder.ConnectionString;
            string sql = @"SELECT * FROM Customer";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer customer = new Customer
                            {
                                CustomerId = reader.GetInt32(0),
                                FirstName = !reader.IsDBNull(1) ? reader.GetString(1) : "",
                                LastName = !reader.IsDBNull(2) ? reader.GetString(2) : "",
                                Country = !reader.IsDBNull(7) ? reader.GetString(7) : "",
                                PostalCode = !reader.IsDBNull(8) ? reader.GetString(8) : "",
                                Phone = !reader.IsDBNull(9) ? reader.GetString(9) : "",
                                Email = !reader.IsDBNull(11) ? reader.GetString(11) : ""
                            };
                            customers.Add(customer);
                        }
                    }
                }
            }
            return customers;
        }

        public Customer GetByName(string name)
        {
            Customer customer = new();
            string connectionString = builder.ConnectionString;
            string sql = $"SELECT * FROM Customer WHERE FirstName = @FirstName";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@FirstName", name);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                customer = new Customer
                                {
                                    CustomerId = reader.GetInt32(0),
                                    FirstName = !reader.IsDBNull(1) ? reader.GetString(1) : "",
                                    LastName = !reader.IsDBNull(2) ? reader.GetString(2) : "",
                                    Country = !reader.IsDBNull(7) ? reader.GetString(7) : "",
                                    PostalCode = !reader.IsDBNull(8) ? reader.GetString(8) : "",
                                    Phone = !reader.IsDBNull(9) ? reader.GetString(9) : "",
                                    Email = !reader.IsDBNull(11) ? reader.GetString(11) : ""
                                };
                                break;
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                System.Console.WriteLine(sqlEx);
            }

            return customer;
        }

        public Customer GetById(int id)
        {
            Customer customer = new();
            string connectionString = builder.ConnectionString;
            string sql = $"SELECT * FROM Customer WHERE CustomerID = @CustomerID";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@CustomerID", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                customer = new Customer
                                {
                                    CustomerId = reader.GetInt32(0),
                                    FirstName = !reader.IsDBNull(1) ? reader.GetString(1) : "",
                                    LastName = !reader.IsDBNull(2) ? reader.GetString(2) : "",
                                    Country = !reader.IsDBNull(7) ? reader.GetString(7) : "",
                                    PostalCode = !reader.IsDBNull(8) ? reader.GetString(8) : "",
                                    Phone = !reader.IsDBNull(9) ? reader.GetString(9) : "",
                                    Email = !reader.IsDBNull(11) ? reader.GetString(11) : ""
                                };
                                break;
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                System.Console.WriteLine(sqlEx);
            }

            return customer;
        }

        public List<Customer> GetCustomerPage(int limit, int offset)
        {
            List<Customer> customers = new List<Customer>();
            string connectionString = builder.ConnectionString;
            string sql = @"SELECT * FROM Customer 
                           ORDER BY CustomerID 
                           OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@Limit", limit);
                    command.Parameters.AddWithValue("@Offset", offset);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer customer = new Customer
                            {
                                CustomerId = reader.GetInt32(0),
                                FirstName = !reader.IsDBNull(1) ? reader.GetString(1) : "",
                                LastName = !reader.IsDBNull(2) ? reader.GetString(2) : "",
                                Country = !reader.IsDBNull(7) ? reader.GetString(7) : "",
                                PostalCode = !reader.IsDBNull(8) ? reader.GetString(8) : "",
                                Phone = !reader.IsDBNull(9) ? reader.GetString(9) : "",
                                Email = !reader.IsDBNull(11) ? reader.GetString(11) : ""
                            };
                            customers.Add(customer);
                        }
                    }
                }
            }
            return customers;
        }

        public bool Add(Customer customer)
        {
            bool success = false;
            string connectionString = builder.ConnectionString;
            string sql = @"INSERT INTO Customer (FirstName, LastName, Country, PostalCode, Phone, Email)
                           VALUES(@Firstname, @Lastname, @Country, @Postalcode, @Phone, @Email)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    //command.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                    command.Parameters.AddWithValue("@Firstname", customer.FirstName);
                    command.Parameters.AddWithValue("@Lastname", customer.LastName);
                    command.Parameters.AddWithValue("@Country", customer.Country);
                    command.Parameters.AddWithValue("@Postalcode", customer.PostalCode);
                    command.Parameters.AddWithValue("@Phone", customer.Phone);
                    command.Parameters.AddWithValue("@Email", customer.Email);
                    success = command.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            return success;
        }

        public bool Update(Customer customer)
        {
            bool success = false;
            string connectionString = builder.ConnectionString;
            string sql = @"UPDATE Customer 
                           SET FirstName = @Firstname, 
                               LastName = @Lastname, 
                               Country = @Country, 
                               PostalCode = @Postalcode, 
                               Phone = @Phone, 
                               Email = @Email
                           WHERE CustomerId = @CustomerId";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                    command.Parameters.AddWithValue("@Firstname", customer.FirstName);
                    command.Parameters.AddWithValue("@Lastname", customer.LastName);
                    command.Parameters.AddWithValue("@Country", customer.Country);
                    command.Parameters.AddWithValue("@Postalcode", customer.PostalCode);
                    command.Parameters.AddWithValue("@Phone", customer.Phone);
                    command.Parameters.AddWithValue("@Email", customer.Email);
                    success = command.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            return success;
        }

        public Dictionary<string, int> CustomerCountry()
        {
            Dictionary<string, int> countryCount = new();
            string connectionString = builder.ConnectionString;
            string sql = @"SELECT Country, count(Country) FROM Customer
                          GROUP by Country
                          ORDER BY count(Country) Desc";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            countryCount.Add(
                                !reader.IsDBNull(0) ? reader.GetString(0) : "", 
                                !reader.IsDBNull(1) ? reader.GetInt32(1) : 0);
                        }
                    }
                }
            }
            return countryCount;
        }
        public Dictionary<string, decimal> BigSpenders()
        {
            Dictionary<string, decimal> countryCount = new();
            List<Customer> customers = new List<Customer>();
            string connectionString = builder.ConnectionString;
            string sql = @"SELECT Customer.Firstname, SUM(Invoice.Total) From Customer
                           INNER JOIN Invoice
                           ON Customer.CustomerId = Invoice.CustomerId
                           GROUP BY Customer.Firstname
                           ORDER BY SUM(Invoice.Total) DESC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            countryCount.Add(reader.GetString(0), reader.GetDecimal(1));
                        }
                    }
                }
            }
            return countryCount;
        }

        public Tuple<int, int> FavoriteGenre(Customer customer)
        {
            Tuple<int, int> FavGenre = new Tuple<int, int>(0, 0);
            string connectionString = builder.ConnectionString;
            string sql = @"WITH GenreCounts AS (
                            SELECT
                                Customer.CustomerId AS CustomerId,
                                Genre.GenreId AS GenreId,
                                COUNT(*) AS genCount
                            FROM
                                Customer
                            INNER JOIN Invoice ON Customer.CustomerId = Invoice.CustomerId
                            INNER JOIN InvoiceLine ON Invoice.InvoiceId = InvoiceLine.InvoiceId
                            INNER JOIN Track ON InvoiceLine.TrackId = Track.TrackId
                            INNER JOIN Genre ON Track.GenreId = Genre.GenreId
                            GROUP BY Customer.CustomerId, Genre.GenreId
                            ),
                            RankedGenres AS (
                                SELECT
                                    CustomerId,
                                    GenreId,
                                    genCount,
                                    RANK() OVER (PARTITION BY CustomerId ORDER BY genCount DESC) AS rnk
                                FROM GenreCounts
                            )
                            SELECT CustomerId, GenreId--, genCount
                            FROM RankedGenres
                            WHERE rnk = 1 AND CustomerId = @CustomerId
                            ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FavGenre = new Tuple<int, int>(reader.GetInt32(0), reader.GetInt32(1));
                        }
                    }
                }
            }
            return FavGenre;
        }
    }
}