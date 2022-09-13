using Moq;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi1.Data;
using WebApi1.Models;
using Xunit;
using FluentAssertions;

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

            // Act
            Action act = () => repo.CreateCustomer(nullCustomer);

            // Assert
            act.Should().Throw<ArgumentNullException>();
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
            var actualCustomer = _context.Customers.LastOrDefault();
            actualCustomer.Should().Be(validCustomer);

        }

        [Fact]
        public void Delete_an_existing_customer()
        {
            // Arrange
            var repo = new CustomerRepo(_context);
            var existingCustomer = new Customer()
            {
                Id = 1, FirstName = "River", LastName = "McCloskey", AccountNumber = 123, SortCode = "456-789", PostCode = "BT74 637", Balance = 6700000.43M
            };

            // Act
            repo.DeleteCustomerById(existingCustomer.Id);
            repo.SaveChanges();
            var customerCount = repo.GetAllCustomers().Count();

            // Assert
            customerCount.Should().Be(1);
        }

        [Fact]
        public void Delete_a_non_existing_customer_and_throw_exception()
        {
            // Arrange
            var repo = new CustomerRepo(_context);

            // Act
            Action act = () => repo.DeleteCustomerById(0);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Get_all_customers()
        {
            // Arrange
            var repo = new CustomerRepo(_context);

            // Act
            var customerCount = repo.GetAllCustomers().Count();

            // Assert
            customerCount.Should().Be(2);
        }

        [Fact]
        public void Try_to_get_empty_list_of_customers_and_throw_an_exception()
        {
            // Arrange
            var repo = new CustomerRepo(_context);

            // Act
            repo.DeleteCustomerById(1);
            repo.DeleteCustomerById(2);
            repo.SaveChanges();
            Action act = () => repo.GetAllCustomers();

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Get_a_customer_by_valid_id_and_return_the_customer()
        {
            // Arrange
            var repo = new CustomerRepo(_context);
            var expectedCustomer = new Customer() { FirstName = "River", LastName = "McCloskey", AccountNumber = 123, SortCode = "456-789", PostCode = "BT74 637", Balance = 6700000.43M };

            // Act
            var actualCustomer = repo.GetCustomerById(1);

            // Assert
            actualCustomer.FirstName.Should().Be(expectedCustomer.FirstName);
            actualCustomer.LastName.Should().Be(expectedCustomer.LastName);
            actualCustomer.AccountNumber.Should().Be(expectedCustomer.AccountNumber);
            actualCustomer.SortCode.Should().Be(expectedCustomer.SortCode);
            actualCustomer.PostCode.Should().Be(expectedCustomer.PostCode);
            actualCustomer.Balance.Should().Be(expectedCustomer.Balance);
        }

        [Fact]
        public void Get_a_customer_by_invalid_id_and_throw_an_exception()
        {
            // Arrange
            var repo = new CustomerRepo(_context);

            // Act
            Action act = () => repo.GetCustomerById(0);

            // Assert
            act.Should().Throw<ArgumentNullException>();

        }


        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}