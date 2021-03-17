using PizzaBox.Domain.Models;
using PizzaBox.Domain.Singletons;
using Xunit;

namespace PizzaBox.Testing.Tests
{
    public class StoreTests
    {
        [Fact]
        public void Test_ChicagoStore()
        {
            // arrange
            var sut = new ChicagoStore();
            var store = StoreSingleton.Instance.GetAllStores().Find(store => store.Name == "Chicago's Pizza Store");

            var expected = "Chicago's Pizza Store";

            // act -- part that we want to test
            var actual = store.Name;

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_StoreCount()
        {
            // arrange
            // var sut = new ChicagoStore();
            var store = StoreSingleton.Instance.GetAllStores();

            var expected = 4;

            // act -- part that we want to test
            var actual = store.Count;

            // assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null)]
        public void Test_ChicagoStoreNotInFile(string expected)
        {
            // arrange
            var sut = new ChicagoStore();

            // act -- part that we want to test
            var actual = sut.Name;

            // assert
            Assert.Equal(expected, actual);
        }
    }
}