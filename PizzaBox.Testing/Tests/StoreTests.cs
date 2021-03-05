using PizzaBox.Domain.Models;
using Xunit;

namespace PizzaBox.Testing.Tests
{
    public class StoreTests
    {
        [Fact]
        public void Test_ChicagoStore()
        {
            // arrange
            var sut = new ChicagoPizza();
            var expected = "Chicago's Pizza";

            // act -- part that we want to test
            var actual = sut.Name;

            // assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Chicago's Pizza")]
        [InlineData("")]
        public void Test_ChicagoStore1(string expected)
        {
            // arrange
            var sut = new ChicagoPizza();
            // expected = "Chicago Store";

            // act -- part that we want to test
            var actual = sut.Name;

            // assert
            Assert.Equal(expected, actual);
        }
    }
}