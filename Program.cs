using Chinook.Src;
using Chinook.Src.Model;
using Chinook.Src.Repositories.CustomerRepo;
using Chinook.Src.Repositories;

namespace Chinook
{
    class Program
    {
        public static void Main(string[] args)
        {
            // App.Start();
            // Server=localhost;Database=Chinook;User=sa;Password=MyStrongPassword123
            // dotnet ef dbcontext scaffold "Server=localhost,1433\\Catalog=Chinook;Database=Chinook;User=sa;Password=MyStrongPassword123; Trust Server Certificate = True" Microsoft.EntityFrameworkCore.SqlServer
            ICustomer customerService = new CustomerService(new ChinookContext());
            ICollection<Customer> check = customerService.GetAll();

            foreach (Customer cust in check){
                System.Console.WriteLine($"{cust.CustomerId}, {cust.FirstName}");
            }
        }
    }
}
