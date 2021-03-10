using System;
using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Singletons
{

    class CustomerOrderManager
    {
        private Customer CurrentCustomer;

        // public static CustomerOrderManager Instance
        // {
        //     get
        //     {
        //         if (_instance == null)
        //         {
        //             _instance = new CustomerOrderManager();
        //         }
        //         return _instance;
        //     }
        //     set
        //     {
        //         _instance = value;
        //     }
        // }

        public static CustomerOrderManager GetManager(Customer customer)
        {
            if (_instance == null)
            {
                _instance = new CustomerOrderManager(customer);
            }
            return _instance;
        }

        private CustomerOrderManager(Customer customer)
        {
            CurrentCustomer = customer;
        }

        private static CustomerOrderManager _instance;

        public Customer AddOrderToCustomer(Customer customer)
        {
            customer.CurrentOrder = new Order();
            return customer;
        }

        public Customer AddPizzaToOrder(Order order)
        {

        }

        private string ReadStringInput()
        {
            //TODO: validate input
            return Console.ReadLine();
        }

        public void PrintStores(List<AStore> stores)
        {
            // foreach (var store in stores)
            // {
            //     Console.WriteLine("")
            // }
            Console.WriteLine("Type a number before the store you want to choose");
            for (int i = 0; i < stores.Count; i++)
            {
                Console.WriteLine("[" + i + "] : " + stores[i]);
            }
        }

        public int ReadIntInput()
        {
            bool success = false;
            int i = 0;
            while (!success)
            {
                Console.WriteLine("Please enter a number");
                string line = Console.ReadLine();
                line = line.Trim();
                success = Int32.TryParse(line, out i);
            }
            return i;
        }

        public AStore ChooseStores()
        {
            var availableStores = GetAvailableStores();
            PrintStores(availableStores);
            var choice = ReadIntInput();
            return availableStores[choice];
        }

        public void StartOrderProcess(AStore store)
        {
            if (CurrentCustomer.OrderedInLastTwoHours())
            {
                Console.WriteLine("Sorry, You can not order twice in less than 2 hours");
                return;
            }
            var storeChoice = ChooseStores();
            CurrentCustomer.CurrentOrder = new Order() { Store = storeChoice };

        }

        public List<AStore> GetAvailableStores()
        {
            var stores = StoreSingleton.Instance.GetAllStores();
            foreach (var store in stores)
            {
                if (CurrentCustomer.HasOrderedStoreIn24Hrs(store))
                {
                    stores.Remove(store);
                }
            }
            return stores;
        }

        // public bool ChooseStore(AStore store)
        // {
        //     if (CurrentCustomer.HasOrderedStoreIn24Hrs(store))
        //     {
        //         return false;
        //     }
        //     CurrentOrder.Store = store;
        //     return true;
        // }

        public List<Topping> GetAllToppings()
        {
            System.Console.WriteLine("You can choose at least 2 toppings and up to 5");
            var dict = PriceManager.Instance.GetToppings();
            List<ToppingType> toppTypes = new List<ToppingType>(dict.Keys);
            List<Topping> topps = new List<Topping>();
            foreach (var pair in dict)
            {
                topps.Add(new Topping(pair.Key)
                {
                    Price = pair.Value
                });
            }
            return topps;
        }

        public List<Crust> GetAllCrusts()
        {
            var dict = PriceManager.Instance.GetCrusts();
            List<CrustType> crustTypes = new List<CrustType>(dict.Keys);
            List<Crust> crusts = new List<Crust>();
            foreach (var pair in dict)
            {
                crusts.Add(new Crust(pair.Key)
                {
                    Price = pair.Value
                });
            }
            return crusts;
        }

        public List<Size> GetAllSizes()
        {
            var dict = PriceManager.Instance.GetSizes();
            List<SizeType> sizeTypes = new List<SizeType>(dict.Keys);
            List<Size> sizes = new List<Size>();
            foreach (var pair in dict)
            {
                sizes.Add(new Size(pair.Key)
                {
                    Price = pair.Value
                });
            }
            return sizes;
        }

    }
}