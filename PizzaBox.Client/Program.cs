using System;
using PizzaBox.Domain.Models;
using PizzaBox.Domain.Singletons;

namespace PizzaBox.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            PlayWithStores();
        }

        public static void PlayWithStores()
        {
            // FreddyPizza s = new FreddyPizza();
            // s.Name = "some pizza store";
            var storeSingleton = new StoreSingleton();
            foreach (var store in storeSingleton.Stores)
            {
                Console.WriteLine(store);
            }
            // Console.WriteLine(storeSingleton.Stores[0].Name);
        }
    }
}
