using Chinook.Src;
using Chinook.Src.Repositories.CustomerRepo;
using Chinook.Src.Model;

namespace Chinook
{
    class Program
    {
        public static void Main(string[] args)
        {
            //App.Start();
            CustomerService customerService = new();
            // var test = customerService.GetAll();

            // foreach (Customer cust in test){
            //     System.Console.WriteLine(cust.CustomerId);
            // }
            Customer check = customerService.GetById(2);
            System.Console.WriteLine(customerService.FavoriteGenre(check));
            // System.Console.WriteLine($"{check.CustomerId} {check.FirstName}");
            // List<Customer> test = customerService.GetCustomerPage(5, 10);
            // foreach (Customer cust in test){
            //     System.Console.WriteLine($"{cust.CustomerId} {cust.FirstName}");
            // }

            // Dictionary<string, decimal> check = customerService.BigSpenders();
            // foreach (KeyValuePair<string, decimal> entry in check)
            // {
            //     System.Console.WriteLine($"{entry.Key}: {entry.Value} ");
            // }
        }
    }
}
