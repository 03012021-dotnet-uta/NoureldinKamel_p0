using PizzaBox.Domain.Models;
using PizzaBox.Domain.Singletons;
using Xunit;

namespace PizzaBox.Testing.Tests
{
    public class DBTests
    {
        [Theory]
        [InlineData("user4", "qwerqwer", "name name")]
        [InlineData("user3", "1234qwer", "some name")]
        [InlineData("user1", "12345678", "first name")]
        // [InlineData("user5", "qwerqwer123", "fifth customer")]
        public void Test_Login(string username, string password, string name)
        {
            // arrange
            var sut = new PizzaBoxRepositoryLayer();
            Customer customer = null;

            // act -- part that we want to test
            var actual = sut.Login(username, password, out customer);

            // assert
            Assert.Equal(name, customer.Name);
        }


        [Theory]
        [InlineData("user5", "qwerqwer123", "fifth customer")]
        [InlineData("user6", "qwerqwer", "name name")]
        public void Test_Register(string username, string password, string name)
        {
            // string name = "fifth customer";
            // string password = "qwerqwer";
            // string username = "user5";
            // arrange
            var sut = new PizzaBoxRepositoryLayer();
            Customer customer = new Customer()
            {
                Name = name,
                Username = username,
                Password = password
            };

            // act -- part that we want to test
            bool b = sut.Register(customer);

            // assert
            Assert.True(b);
            DeleteCustomer(customer);
        }

        // [Theory]
        // [InlineData("user3", "1234qwer", "some name")]
        // [InlineData("user1", "12345678", "first name")]
        // [InlineData("user5", "qwerqwer123", "fifth customer")]
        // [InlineData("user6", "qwerqwer", "fifth customer")]
        // public void Test_NewLogin(string username, string password, string name)
        // {
        //     // arrange
        //     var sut = new PizzaBoxRepositoryLayer();
        //     Customer customer = null;

        //     // act -- part that we want to test
        //     var actual = sut.Login(username, password, out customer);

        //     // assert
        //     Assert.Equal(name, customer.Name);
        //     if (customer.Name == "fifth customer")
        //     {
        //         DeleteCustomer(customer);
        //     }
        // }

        public void DeleteCustomer(Customer customer)
        {
            new PizzaBoxRepositoryLayer().DeleteCustomer(customer);
        }

        // [Theory]
        // [InlineData(n]
        public void TestDBLoading()
        {
            Customer customer = new Customer();
            var crusts = CustomerOrderManager.GetManager(customer).GetAllCrusts();
        }
    }
}