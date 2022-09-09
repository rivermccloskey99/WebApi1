using Moq;
using System;
using WebApi1.Data;
using WebApi1.Models;
using Xunit;

namespace WebApi1.Tests
{
    public class CustomerRepoTests
    {
        [Fact]
        public void Create_a_null_customer_and_throw_exception()
        {
            // Arrange
            var repo = new CustomerRepo(null);
            Customer nullCustomer = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => repo.CreateCustomer(nullCustomer));
        }

        [Fact]
        public void Delete_a_non_existing_customer_and_throw_exception()
        {
            /*
            
            // Arrange
            int id = 1;
            Mock<AppDbContext> mockContext = new Mock<AppDbContext>();
            mockContext.Setup(x => x.Customers.Find(id)).Returns<Customer>(null);
            var repo = new CustomerRepo(mockContext.Object);

            // Assert
            Assert.Throws<ArgumentNullException>(() => repo.DeleteCustomerById(id));

            */


            // Arrange
            int id = 1;
            var context = new AppDbContext(null);
            var repo = new CustomerRepo(context);

            // Assert
            Assert.Throws<ArgumentNullException>(() => repo.DeleteCustomerById(id));

        }

    }
}