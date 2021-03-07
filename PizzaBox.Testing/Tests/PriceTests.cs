using PizzaBox.Domain.Models;
using PizzaBox.Domain.Singletons;
using Xunit;

namespace PizzaBox.Testing.Tests
{
    public class PriceTests
    {
        [Fact]
        public void Test_CrustTypePan()
        {
            // arrange
            var sut = PriceManager.Instance;

            var expected = 1.7F;

            // act -- part that we want to test
            var actual = sut.getPrice(CrustType.Pan);

            // assert
            Assert.Equal(expected, actual);
        }


        [Fact]
        public void Test_ToppingType()
        {
            // arrange
            var sut = PriceManager.Instance;

            var expected = 0.6F;

            // act -- part that we want to test
            var actual = sut.getPrice(ToppingType.Chicken);

            // assert
            Assert.Equal(expected, actual);
        }


        [Fact]
        public void Test_SizeType()
        {
            // arrange
            var sut = PriceManager.Instance;

            var expected = 7.0F;

            // act -- part that we want to test
            var actual = sut.getPrice(SizeType.Xlarge);

            // assert
            Assert.Equal(expected, actual);
        }
    }
}