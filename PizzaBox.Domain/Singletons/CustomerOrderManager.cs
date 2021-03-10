using System;
using System.Collections.Generic;
using System.Linq;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Models;
using PizzaBox.Storing;

namespace PizzaBox.Domain.Singletons
{

    public class CustomerOrderManager
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

        // public Customer AddPizzaToOrder(Order order)
        // {

        // }

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

        public AStore ChooseStore()
        {
            var availableStores = GetAvailableStores();
            PrintStores(availableStores);
            var choice = ReadIntInput();
            return availableStores[choice];
        }

        public APizza ChoosePizza()
        {
            List<APizza> showPizzas = GetAllPizzas();
            PrintPizza(showPizzas);
            var choice = ReadIntInput();
            return showPizzas[choice];
        }

        public void PrintPizza(List<APizza> showPizzas)
        {
            Console.WriteLine("Type a number before the pizza you want to choose");
            for (int i = 0; i < showPizzas.Count; i++)
            {
                Console.WriteLine("[" + i + "] : " + showPizzas[i].Name);
            }
        }

        public void StartOrderProcess()
        {
            if (CurrentCustomer.OrderedInLastTwoHours())
            {
                Console.WriteLine("Sorry, You can not order twice in less than 2 hours");
                return;
            }
            var storeChoice = ChooseStore();
            CurrentCustomer.CurrentOrder = new Order() { Store = storeChoice };
            var PizzaChoice = ChoosePizza();
            var realPizza = GetRealPizza(PizzaChoice);
            realPizza = CustomizePizza(realPizza);
        }

        public APizza CustomizePizza(APizza pizza)
        {
            bool loop = true;
            while (loop)
            {
                PrintPizzaCustomizeMenu();
                var actionChoice = ReadIntInput();
                switch (actionChoice)
                {
                    case 0: pizza = CustomizeTopping(pizza); break;
                    case 1: pizza = ChangeCrust(pizza); break;
                    case 2: pizza = ChangeSize(pizza); break;
                    case 3: loop = false; break;
                    default: break;
                }
                Console.WriteLine("your current pizza is: ");
                Console.WriteLine(pizza);
            }
            return pizza;
        }

        public APizza CustomizeTopping(APizza pizza)
        {
            bool loop = true;
            while (loop)
            {
                var toppings = GetAllToppings();
                PrintToppingMenu();
                var toppingAction = ReadIntInput();
                // PrintPizza(pizza);
                switch (toppingAction)
                {
                    case 0: AddTopping(pizza); break;
                    case 1: RemoveTopping(pizza); break;
                    case 2: loop = false; break;
                    default: break;
                }
            }
            return pizza;
        }

        public void PrintPizza(APizza pizza)
        {
            Console.WriteLine("your pizza is: ");
            Console.WriteLine(pizza);
        }

        //TODO: only show available toppings
        public APizza AddTopping(APizza pizza)
        {
            List<Topping> toppings = GetAllToppings();
            Console.WriteLine("available toppings are: ");

            for (int i = 0; i < toppings.Count; i++)
            {
                Console.WriteLine("[" + i + "]: " + toppings[i].Type + " -- $" + toppings[i].Price);
            }
            //todo: validate between range
            var input = ReadIntInput();

            pizza.AddTopping(toppings[input]);
            return pizza;
        }

        public APizza RemoveTopping(APizza pizza)
        {
            return pizza;
        }

        public void PrintToppingMenu()
        {
            Console.WriteLine("[0]: Add a topping");
            Console.WriteLine("[1]: Remove a topping");
            Console.WriteLine("[2]: Go Back");
        }

        public APizza PrintToppingMenu(APizza pizza)
        {
            return pizza;
        }

        public APizza ChangeCrust(APizza pizza)
        {
            return pizza;
        }

        public APizza ChangeSize(APizza pizza)
        {
            return pizza;
        }

        public void PrintPizzaCustomizeMenu()
        {
            Console.WriteLine("choose something to customize or change");
            Console.WriteLine("[0]: toppings");
            Console.WriteLine("[1]: crust");
            Console.WriteLine("[2]: size");
            Console.WriteLine("[3]: save pizza");
        }

        public APizza GetRealPizza(APizza showPizza)
        {
            showPizza.AddDefaultToppings();
            showPizza.AddDefaultCrust();
            showPizza.AddDefaultSize();
            return showPizza;
            // showPizzas.
            // foreach (var pizza in showPizzas)
            // {

            // }
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

        public List<APizza> GetAllPizzas()
        {
            FileStorage fs = new FileStorage();
            return fs.ReadListFromXml<APizza>(FileType.Pizza).ToList();
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