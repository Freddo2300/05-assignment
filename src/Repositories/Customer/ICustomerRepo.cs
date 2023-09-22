using System.Linq;

using Chinook.Src.Model;

namespace Chinook.Src.Repositories.CustomerRepo
{
    internal interface ICustomer //: ICrudService<Customer, int>
    {
        Customer GetByName(string name);

        ICollection<Customer> GetAll();

        Customer GetById(int id);

        ICollection<Customer> GetCustomerPage(int limit, int offset);

        ICollection<Customer> GetCustomersByCountry();

        ICollection<CustomerInvoice> GetCustomersByNetSpend();

        void Add(Customer customer);

        void Update(Customer customer);

        void Delete(Customer customer);
    }
}