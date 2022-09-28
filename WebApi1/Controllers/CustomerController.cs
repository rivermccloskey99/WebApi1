using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi1.Data;
using WebApi1.Dtos;
using WebApi1.Models;
using WebApi1.SyncDataServices.Http;

namespace WebApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepo _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;

        public CustomerController(ICustomerRepo repository, IMapper mapper, ICommandDataClient commandDataClient)
        {
            _repository = repository;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CustomerReadDto>> GetAllCustomers()
        {
            System.Console.WriteLine("--- Getting all customers");

            var customers = _repository.GetAllCustomers();

            return Ok(_mapper.Map<IEnumerable<CustomerReadDto>>(customers));
        }

        [HttpGet("{id}", Name = "GetCustomerById")]
        public ActionResult<CustomerReadDto> GetCustomerById(int id)
        {
            var customer = _repository.GetCustomerById(id);
            if (customer != null)
            {
                return Ok(_mapper.Map<CustomerReadDto>(customer));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<CustomerReadDto>> CreateCustomer(CustomerCreateDto customerCreateDto)
        {
            var customer = _mapper.Map<Customer>(customerCreateDto);
            _repository.CreateCustomer(customer);
            _repository.SaveChanges();

            var customerReadDto = _mapper.Map<CustomerReadDto>(customer);

            try
            {
                await _commandDataClient.SendCustomerToCommand(customerReadDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"-- Could not send POST request: {ex.Message}");
            }

            return CreatedAtRoute(nameof(GetCustomerById), new { Id = customer.Id }, customer);
        }


        [HttpDelete("{id}")]
        public ActionResult<CustomerReadDto> DeleteAtId(int id)
        {
            var customer = _repository.GetCustomerById(id);
            if (customer != null)
            {
                _repository.DeleteCustomerById(id);
                _repository.SaveChanges();
                return Ok(_mapper.Map<Customer>(customer));
            }
            else
            {
                return NotFound();
            }
        }

    }
}