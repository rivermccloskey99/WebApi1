using WebApi1.Models;

namespace WebApi1.Data
{
    public interface ICustomerRepo
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomerById(int id);
        void CreateCustomer(Customer c);
        void DeleteCustomerById(int id);

        bool SaveChanges();
    }
}