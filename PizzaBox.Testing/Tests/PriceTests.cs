using PizzaBox.Domain.Models;
using PizzaBox.Domain.Singletons;
using Xunit;

namespace PizzaBox.Testing.Tests
{
    public class PriceTests
    {
        [Theory]
        [InlineData(3, CrustType.Pan)]
        [InlineData(2.5, CrustType.Thick)]
        [InlineData(2, CrustType.Thin)]
        [InlineData(5, CrustType.Stuffed)]
        public void Test_CrustTypePan(float expected, CrustType type)
        {
            // arrange
            var sut = new Crust(type);

            // var expected = 1.7F;

            // act -- part that we want to test
            // var actual = sut.getPrice(CrustType.Pan);
            var actual = sut.Price;

            // assert
            Assert.Equal(expected, actual);
        }


        [Theory]
        [InlineData(5.5, ToppingType.Chicken)]
        [InlineData(6, ToppingType.Meat)]
        [InlineData(3.5, ToppingType.Mozirilla)]
        [InlineData(1, ToppingType.Onion)]
        public void Test_ToppingType(float expected, ToppingType type)
        {
            // arrange
            var sut = new Topping(type);

            // var expected = 0.6F;

            // act -- part that we want to test
            var actual = sut.Price;

            // assert
            Assert.Equal(expected, actual);
        }


        [Theory]
        [InlineData(10, SizeType.Large)]
        [InlineData(7, SizeType.Medium)]
        [InlineData(5, SizeType.Small)]
        [InlineData(12.5, SizeType.Xlarge)]
        public void Test_SizeType(float expected, SizeType type)
        {
            // arrange
            var sut = new Size(type);

            // var expected = 7.0F;

            // act -- part that we want to test
            var actual = sut.Price;

            // assert
            Assert.Equal(expected, actual);
        }
    }
}