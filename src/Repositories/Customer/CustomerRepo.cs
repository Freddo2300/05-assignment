using Chinook.Src.Model;
using Chinook.Src.Exceptions;



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
            var customer = _context.Customers.Where(p => p.FirstName == name).FirstOrDefault();
            return customer is null ? throw new CustomerException("Professor does not exist with that ID") : customer;
        }

        public ICollection<Customer> GetAll(){
            return _context.Customers.ToHashSet();
        }

        public Customer GetById(int id){
            var customer = _context.Customers.Where(p => p.CustomerId == id).FirstOrDefault();
            return customer is null ? throw new CustomerException("Professor does not exist with that ID") : customer;
        }

        public ICollection<Customer> GetCustomerPage(int limit, int offset){
            return _context.Customers.Skip(offset).Take(limit).ToHashSet();
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