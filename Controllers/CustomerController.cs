using Microsoft.AspNetCore.Mvc;
using WebApi1.Data;
using WebApi1.Models;

namespace WebApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepo _repository;

        public CustomerController(ICustomerRepo repository)
        {
            _repository = repository;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetAllCustomers()
        {
            System.Console.WriteLine("--- Getting all customers");

            var customers = _repository.GetAllCustomers();

            return Ok(customers);
        }

        [HttpGet("{id}", Name = "GetCustomerById")]
        public ActionResult<Customer> GetCustomerById(int id)
        {
            var customer = _repository.GetCustomerById(id);
            if(customer != null)
            {
                return Ok(customer);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult<Customer> CreateCustomer(Customer customer)
        {
            _repository.CreateCustomer(customer);
            _repository.SaveChanges();

            return CreatedAtRoute(nameof(GetCustomerById), new { Id = customer.Id}, customer);
        }

        
        [HttpDelete("{id}")]
        public ActionResult<Customer> DeleteAtId(int id)
        {
            var customer = _repository.GetCustomerById(id);
            if(customer != null)
            {
                _repository.DeleteCustomerById(id);
                _repository.SaveChanges();
                return Ok(customer);
            }
            else
            {
                return NotFound();
            }
        }
        
    }
}