using Moq;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi1.Data;
using WebApi1.Models;
using Xunit;

namespace WebApi1.Tests
{
    public class CustomerRepoTests : IDisposable
    {
        private readonly AppDbContext _context;

        public CustomerRepoTests()
        {
            var opt = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new AppDbContext(opt);
            _context.Database.EnsureCreated();

            _context.Customers.AddRange(
                new Customer() { FirstName = "River", LastName = "McCloskey", AccountNumber = 123, SortCode = "456-789", PostCode = "BT74 637", Balance = 6700000.43M },
                new Customer() { FirstName = "Rory", LastName = "McGrath", AccountNumber = 234, SortCode = "654-789", HouseNumber = "8080", StreetName = "Large Street", TownCity = "Belfast", PostCode = "BT62 2HS", Balance = 5700000.43M }
            );
            _context.SaveChanges();
        }

        [Fact]
        public void Create_a_null_customer_and_throw_exception()
        {
            // Arrange
            var repo = new CustomerRepo(_context);
            Customer nullCustomer = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => repo.CreateCustomer(nullCustomer));
        }

        [Fact]
        public void Create_a_valid_customer()
        {
            // Arrange
            var repo = new CustomerRepo(_context);
            var validCustomer = new Customer()
            {
                FirstName = "Test", LastName = "Test", AccountNumber = 321, SortCode = "456-789", PostCode = "BT74 637",
                Balance = 100000M
            };

            // Act
            repo.CreateCustomer(validCustomer);
            repo.SaveChanges();

            // Assert
            Assert.Equal(validCustomer, _context.Customers.LastOrDefault());
        }

        [Fact]
        public void Delete_an_existing_customer()
        {
            // Arrange

            // Act

            // Assert

        }

        [Fact]
        public void Delete_a_non_existing_customer_and_throw_exception()
        {
            // Arrange
            var repo = new CustomerRepo(_context);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => repo.DeleteCustomerById(0));
        }

        [Fact]
        public void Get_all_customers()
        {
            // Arrange
            var repo = new CustomerRepo(_context);

            // Act
            var customerCount = repo.GetAllCustomers().Count();

            // Assert
            Assert.Equal(2, customerCount);
        }

        [Fact]
        public void Get_a_customer_by_valid_id_and_return_the_customer()
        {
            // Arrange
            var repo = new CustomerRepo(_context);
            var expectedCustomer = new Customer() { FirstName = "River", LastName = "McCloskey", AccountNumber = 123, SortCode = "456-789", PostCode = "BT74 637", Balance = 6700000.43M };

            // Act
            var customer = repo.GetCustomerById(1);

            // Assert
            Assert.Equal(expectedCustomer.FirstName, customer.FirstName);
            Assert.Equal(expectedCustomer.LastName, customer.LastName);
            Assert.Equal(expectedCustomer.AccountNumber, customer.AccountNumber);
            Assert.Equal(expectedCustomer.SortCode, customer.SortCode);
            Assert.Equal(expectedCustomer.PostCode, customer.PostCode);
            Assert.Equal(expectedCustomer.Balance, customer.Balance);
        }

        [Fact]
        public void Get_a_customer_by_invalid_id_and_throw_an_exception()
        {
            // Arrange

            // Act

            // Assert

        }


        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}