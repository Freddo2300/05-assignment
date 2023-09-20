using Dapper;
using MySqlConnector;

namespace Chinook
{
    class Program
    {
        public static void Main(string[] args)
        {
            string connectionString
                = @"
                Server=localhost; 
                User ID=fred; 
                Password=|EUr0m4n96; 
                Database=chinook";

            using var connection = new MySqlConnection(connectionString);

            var users = connection.Query<string>(@"
                    SELECT user_name
                    FROM users
                    ORDER BY user_id ASC
                    ");

            foreach (string name in users)
            {
                Console.WriteLine($"NAME: {name}");
            }

        }
    }
}
