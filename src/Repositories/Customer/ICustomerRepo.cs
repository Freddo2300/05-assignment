using Chinook.Src.Model;

namespace Chinook.Src.Repositories.CustomerRepo
{
    internal interface ICustomer //: ICrudService<Customer, int>
    {
        Customer GetByName(string name);
        ICollection<Customer> GetAll();
        Customer GetById(int id);
        ICollection<Customer> GetCustomerPage(int limit, int offset);
    }
}