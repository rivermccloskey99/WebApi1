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
            Customer customer = null;

            // to be mocked
            CustomerRepo repo = new CustomerRepo();

            // Act


            // Assert

        }
    }
}