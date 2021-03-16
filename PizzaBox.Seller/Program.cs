using System;
using PizzaBox.Domain.Singletons;

namespace PizzaBox.Seller
{
    class Program
    {
        static void Main(string[] args)
        {
            StoreOrderManager.Instance.StartStoreApp();
        }
    }
}
