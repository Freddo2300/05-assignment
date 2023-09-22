using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
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

        public Customer GetByName(string name)
        {
            var customer = _context.Customers.Where(p => p.FirstName == name).FirstOrDefault();
            return customer is null ? throw new CustomerException("Professor does not exist with that ID") : customer;
        }

        public ICollection<Customer> GetAll()
        {
            return _context.Customers.ToHashSet();
        }

        public Customer GetById(int id)
        {
            var customer = _context.Customers.Where(p => p.CustomerId == id).FirstOrDefault();
            return customer is null ? throw new CustomerException("Professor does not exist with that ID") : customer;
        }

        public ICollection<Customer> GetCustomerPage(int limit, int offset){
            return _context.Customers.Skip(offset).Take(limit).ToHashSet();
        }

        public ICollection<Customer> GetCustomersByCountry()
        {   
            return _context.Customers.ToHashSet();
        }

        public ICollection<CustomerInvoice> GetCustomersByNetSpend()
        {
            var query =
                from customer in _context.Customers
                join invoice in _context.Invoices on customer.CustomerId equals invoice.CustomerId into ci
                from subset in ci.DefaultIfEmpty()
                orderby subset.Total descending
                select new CustomerInvoice()
                {
                    CustomerId = customer.CustomerId,
                    FirstName = customer.FirstName,
                    Total = subset.Total
                };

            return query.ToHashSet();

            /*
            return _context.Customers
                .Where(
                    customer => _context.Invoices
                        .OrderByDescending(invoice => invoice.Total)
                        .Any(invoice => invoice.CustomerId == customer.CustomerId))
                .ToHashSet();
            */
        }

        public void Add(Customer customer) 
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();

            Console.WriteLine($"Successfully added customer {customer.CustomerId}");
        }

        public Customer Save(Customer customer) 
        {
            throw new NotImplementedException();
        }
        
        public void Update(Customer customer) 
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();

            Console.WriteLine($"Successfully updated customer {customer.CustomerId}");
        }

        public void Delete(Customer customer)
        {
            _context.Customers.Remove(customer);
            _context.SaveChanges();

            Console.WriteLine($"SKRRRRRRRRRRRR. wroom wroom");
        }
        
    }
}