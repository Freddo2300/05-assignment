using System.Linq;

using Chinook.Src.Model;

namespace Chinook.Src.Repositories.CustomerRepo
{
    internal interface ICustomer //: ICrudService<Customer, int>
    {
        Customer GetByName(string name);
        List<Customer> GetAll();
        Customer GetById(int id);
        List<Customer> GetCustomerPage(int limit, int offset);
        bool Add(Customer customer);
        bool Update(Customer customer);
        Dictionary<string, int> CustomerCountry();
        Dictionary<string, decimal> BigSpenders();
        Tuple<int, int> FavoriteGenre(Customer customer);
    }
}