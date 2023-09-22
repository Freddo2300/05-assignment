using Chinook.Src.Model;

namespace Chinook.Src.Repositories.CustomerRepo
{
    internal class CustomerService : ICustomer
    {
        private readonly ChinookContext _context;

        public CustomerService(ChinookContext context)
        {
            _context = context;
        }

        public Customer GetByName(string name){
            throw new NotImplementedException();
        }

        public ICollection<Customer> GetAll(){
            return _context.Customers.ToHashSet();
        }

        public Customer GetById(int id){
            throw new NotImplementedException();
        }

        public Customer Save(Customer customer) {
            throw new NotImplementedException();
        }
        
        public Customer Update(Customer customer) {
            throw new NotImplementedException();
        }

        public void Delete(int id){
            throw new NotImplementedException();
        }
        
    }
}