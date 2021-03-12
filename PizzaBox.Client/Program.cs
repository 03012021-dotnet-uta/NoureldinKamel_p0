using System;
using System.Collections.Generic;
using System.Linq;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Models;
using PizzaBox.Domain.Singletons;
using PizzaBox.Storing;
// using PizzaBox.Storing;

namespace PizzaBox.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            // PlayWithStores();
            // TestOrder(); 
            // TestPointer();
            // TestTime();
            // SavePizzaTypes();
            // TestSavedPizzas();
            TestUserStory();
            // TestStoreStory();
            // TestStoreStory();
        }

        private static void TestStoreStory()
        {
            StoreSingleton.Instance.GetAllStores();
        }

        public static void TestSavedPizzas()
        {
            // FileStorage fs = new FileStorage();
            // List<APizza> pizzas = fs.ReadListFromXml<APizza>(FileType.Pizza).ToList();
            // foreach (var item in pizzas)
            // {
            //     Console.WriteLine(item.Name);
            // }
        }

        public static void SavePizzaTypes()
        {
            // FileStorage fs = new FileStorage();
            // float sp = PriceManager.Instance.getPrice(SizeType.Large);
            // float cp = PriceManager.Instance.getPrice(CrustType.Thick);
            // var pizzas = new List<APizza>()
            // {
            //     new MeatPizza(new Size(SizeType.Large){Price = sp}, new Crust(CrustType.Thick){Price = cp}){Name = "Meat Pizza"},
            //     new VeganPizza(new Size(SizeType.Large){Price = sp}, new Crust(CrustType.Thick){Price = cp}){Name = "Vegan Pizza"},
            //     new VeggiePizza(new Size(SizeType.Large){Price = sp}, new Crust(CrustType.Thick){Price = cp}){Name = "Veggie Pizza"},
            //     new CustomPizza(new Size(SizeType.Large){Price = sp}, new Crust(CrustType.Thick){Price = cp}){Name = "Custom Pizza"}
            // };
            // fs.WriteToXml<APizza>(FileType.Pizza, pizzas);
        }

        public static void TestTime()
        {
            // DateTime t1 = new DateTime(2001, 3, 10, 22, 30, 0);
            // 12:08:50
            DateTime t2 = new DateTime(2021, 3, 9, 0, 8, 0);
            Console.WriteLine("now: " + DateTime.Now);
            Console.WriteLine("t2: " + t2);
            TimeSpan timeSpan = DateTime.Now.Subtract(t2);
            TimeSpan timespan2 = new TimeSpan(1, 0, 0, 0);
            Console.WriteLine("now - t2: " + timeSpan.TotalHours);
            Console.WriteLine("2 hour timespan: " + timespan2);
            Console.WriteLine("is now-2 greater than 2hrs? " + (Math.Abs(timeSpan.TotalHours) > timespan2.TotalHours));
        }

        public void testStoreSingleton()
        {
            var storeSingleton = StoreSingleton.Instance;
            foreach (var store in storeSingleton.Stores)
            {
                Console.WriteLine(store);
            }
        }

        public static void TestUserStory()
        {
            // var order = new Order();
            // var pizza1 = new MeatPizza(SizeType.Large);
            // var pizza2 = new MeatPizza(SizeType.Large);
            // order.AddPizza(pizza1);
            // order.AddPizza(pizza2);
            // // var dict = order.ViewPizzas();
            // Console.WriteLine(order);
            Customer c = new Customer();
            CustomerOrderManager.GetManager(c).StartStoreApp();
        }

        public static void TestPointer()
        {
            // APizza p = new MeatPizza(SizeType.Large);
            // List<APizza> l = new List<APizza>();
            // l.Add(p);
            // p = null;
            // Console.WriteLine(l[0]);
        }

        public static void PlayWithStores()
        {
            // FreddyPizza s = new FreddyPizza();
            // s.Name = "some pizza store";
            // storeSingleton.WriteStoresToXml(storeSingleton.Stores);
            // var stores = storeSingleton.ReadStoresFromXml();
            // Console.WriteLine(storeSingleton.Stores[0].Name);

            // Dictionary<FileType, float> testdic = new Dictionary<FileType, float>();
            // testdic.Add(FileType.Crusts, 1.2F);
            // testdic.Add(FileType.Toppings, 1.5F);
            // testdic.Add(FileType.Sizes, 1.9F);
            // testdic.Add(FileType.Stores, 2.1F);

            // Console.WriteLine("crusts: " + testdic[FileType.Crusts]);
            // Console.WriteLine("toppings: " + testdic[FileType.Toppings]);

            // var priceManager = PriceManager.Instance;
            // Console.WriteLine("price of pan crust: " + priceManager.getPrice(CrustType.Pan));
            // Console.WriteLine("price of large pizza: " + priceManager.getPrice(SizeType.Large));
            // Console.WriteLine("price of chicken topping: " + priceManager.getPrice(ToppingType.Chicken));

            // var pizza = new MeatPizza(SizeType.Large);
            // Console.WriteLine(pizza);
            // Console.WriteLine("pizza price is: " + pizza.CalculateTotalPrice());
            // var order = new Order();
            // order.AddPizza(pizza);
            // Console.WriteLine("\nadding first pizza");
            // Console.WriteLine(order.ToString());
            // // Console.WriteLine("And its total is: " + order.CalculateTotalCost());
            // order.AddPizza(pizza);
            // Console.WriteLine("\nadding second pizza");
            // Console.WriteLine(order.ToString());
            // Console.WriteLine("And its total is: " + order.CalculateTotalCost());
        }
    }
}
