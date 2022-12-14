using System.Linq;
using WebApi1.Models;

namespace WebApi1.Data
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly AppDbContext _context;

        public CustomerRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateCustomer(Customer c)
        {
            if(c == null)
                throw new ArgumentNullException(nameof(c));

            _context.Customers.Add(c);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            var customerCount = _context.Customers.Any();
            if (customerCount == false) 
                throw new ArgumentNullException("No customers in database");

            return _context.Customers.ToList();
        }

        public Customer GetCustomerById(int id)
        {
            var customerToGet = _context.Customers.Find(id);

            if (customerToGet == null)
                throw new ArgumentNullException(nameof(customerToGet));

            return _context.Customers.FirstOrDefault(c => c.Id == id);
        }

        
        public void DeleteCustomerById(int id)
        {
            var customerToDelete = _context.Customers.Find(id);

            if(customerToDelete == null)
                throw new ArgumentNullException(nameof(customerToDelete));

            _context.Customers.Remove(customerToDelete);
        }
        

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

    }
}