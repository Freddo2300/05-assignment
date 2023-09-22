using Chinook.Src.Model;

namespace Chinook.Src.Repositories.CustomerRepo
{
    internal interface ICustomer : ICrudService<Customer, int>
    {
        Customer GetByName(string name);
    }
}