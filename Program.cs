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
            ICustomer customerService = new CustomerService(new ChinookContext());
            // ICollection<Customer> customers = customerService.GetAll();
            // Customer single_customer = customerService.GetById(1);


            // foreach (Customer cust in customers){
            //     System.Console.WriteLine($"{cust.CustomerId} {cust.FirstName}");
            // }
            // System.Console.WriteLine($"{single_customer.CustomerId} {single_customer.FirstName}");
            // Customer named_customer = customerService.GetByName("Puja");
            // System.Console.WriteLine($"{named_customer.CustomerId} {named_customer.LastName}");
            
            ICollection<Customer> customers = customerService.GetCustomerPage(5, 10);

            foreach (Customer cust in customers){
                System.Console.WriteLine($"{cust.CustomerId} {cust.FirstName}");
            }
        }
    }
}
