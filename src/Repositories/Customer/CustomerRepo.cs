namespace Chinook.Repositories.Customers
{
    internal class CustomerService : ICustomer
    {
        private readonly CustomerDbContext _context;

        public CustomerService(CustomerDbContext context)
        {
            _context = context;
        }
    }
}