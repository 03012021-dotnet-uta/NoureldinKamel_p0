using PizzaBox.Domain.Models;
using PizzaBox.Domain.Singletons;
using Xunit;

namespace PizzaBox.Testing.Tests
{
    public class OrderTests
    {
        // [Fact]
        public void Test_OrderCreation()
        {
            // arrange
            // var sut = PriceManager.Instance;
            Customer customer = null;
            new PizzaBoxRepositoryLayer().Login("user1", "12345678", out customer);



            // var expected = 1.7F;

            // act -- part that we want to test
            // var actual = sut.getPrice(CrustType.Pan);

            // assert
            // Assert.Equal(expected, actual);
            Order o = new Order();

        }
    }
}