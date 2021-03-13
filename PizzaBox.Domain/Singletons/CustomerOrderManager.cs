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

        public void AddOrderToCustomer(AStore store)
        {
            CurrentCustomer.CurrentOrder = new Order() { Store = store };
        }

        // public Customer AddPizzaToOrder(Order order)
        // {

        // }


        public void StartStoreApp()
        {
            PrintStartingSequence();
            // var stores = GetAvailableStores();
            MainSignedOutOptions();
        }


        public void MainSignedOutOptions()
        {
            bool loop = true;
            bool end = false;
            CurrentCustomer = null;
            while (loop)
            {
                var input = ChooseMainMenu();
                switch (input)
                {
                    case 1: Login(out CurrentCustomer); break;
                    case 2: Register(out CurrentCustomer); break;
                    case 0: loop = false; break;
                }

                if (CurrentCustomer != null)
                    OrderOrSignout(out end);
                else
                    PrintInfo("login/registration failed");

                if (end)
                    break;
            }
        }

        public void OrderOrSignout(out bool end)
        {
            bool loop = true;
            end = false;
            while (loop)
            {
                PrintInstruction("choose an option");
                PrintOption(0, "signout");
                PrintOption(1, "start a new order or continue last one");
                PrintOption(2, "view order history");
                var input = ReadIntInput(0, 2);
                switch (input)
                {
                    case 1: StartOrderProcess(out end); break;
                    case 2: ViewOrderHistory(); break;
                    case 0: loop = false; break;
                }
                if (end)
                    break;
            }
        }


        /* #region Login and Register */
        private void Register(out Customer customer)
        {
            customer = null;
            PrintInstruction("enter you name");
            var name = ReadStringInput();
            PrintInstruction("enter you username");
            var username = ReadStringInput();
            var pp = new PizzaBoxRepositoryLayer();
            if (pp.CheckUserExists(username))
            {
                PrintInfo("account exists");
                return;
            }
            PrintInstruction("enter your password");
            var pass = ReadStringInput();
            customer = new Customer()
            {
                Name = name,
                Username = username,
            };
            customer.SetPass(pass);
            PrintInfo("Registration sucess");
        }

        public bool Login(out Customer customer)
        {
            PrintInstruction("enter your username");
            var username = ReadStringInput();
            PrintInstruction("enter your password");
            var pass = ReadStringInput();
            return new PizzaBoxRepositoryLayer().Login(username, pass, out customer);
        }

        /* #endregion */

        public void ViewOrderHistory()
        {
            PrintInstruction("View order new yet implemented");
        }

        public void StartOrderProcess(out bool end)
        {
            var loop = true;
            end = false;
            while (loop)
            {
                if (CurrentCustomer.CurrentOrder != null)
                {
                    PrintInfo("current order is");
                    Console.WriteLine(CurrentCustomer.CurrentOrder);
                    ShowOrderActionMenu(out end);
                }
                else
                {
                    var store = GetAvailableStores();
                    PrintStores(store);
                    var input = ReadIntInput(0, store.Count);
                    if (input == 0)
                    {
                        break;
                    }
                    var ChosenStore = store[--input];
                    AddOrderToCustomer(ChosenStore);
                    ShowOrderActionMenu(out end);
                }
                if (end)
                    break;
            }
        }

        public void ShowOrderActionMenu(out bool end)
        {
            end = false;
            var loop = true;
            while (loop)
            {
                PrintInfo("Your order is");
                Console.WriteLine(CurrentCustomer.CurrentOrder);
                PrintOrderActionMenu();
                var input = ReadIntInput(0, 4);
                end = false;
                switch (input)
                {
                    case 1: AddPizza(); break;
                    case 2: RemovePizzaMenu(); break;
                    case 3: ChooseAPizzaToCustomize(); break;
                    case 4: Checkout(out end); break;
                    case 0: loop = false; break;
                }
                if (end)
                    break;

            }
        }

        private void Checkout(out bool end)
        {
            if (!(CurrentCustomer.IsOrderOk() && CurrentCustomer.CanOrderCheckout()))
            {
                end = false;
                return;
            }
            PrintInstruction("choose an option");
            PrintInfo("Your order is");
            Console.WriteLine(CurrentCustomer.CurrentOrder);
            PrintOption(1, "checkout");
            PrintOption(0, "go back");
            var input = ReadIntInput(0, 1);
            end = false;
            if (input == 1)
            {
                Order o = CurrentCustomer.CurrentOrder;
                CurrentCustomer.Checkout();
                PrintThankYouMessage(o);
                end = true;
            }
        }


        /* #region Customize pizzas in order */
        private void ChooseAPizzaToCustomize()
        {
            var addedPizzas = CurrentCustomer.CurrentOrder.GetPizzas();
            PrintInstruction("choose a pizza to customize");
            PrintAddedPizzaMenu(addedPizzas);
            var input = ReadIntInput(0, addedPizzas.Count);
            if (input == 0)
            {
                return;
            }
            CustomizePizza(addedPizzas[--input]);
        }

        public void AddPizza()
        {
            List<APizza> showPizzas = GetAllPizzas();
            PrintShowPizzas(showPizzas);
            var input = ReadIntInput(0, showPizzas.Count);
            if (input == 0)
            {
                return;
            }
            // input--;
            // Console.WriteLine("showPizzas.Count: " + showPizzas.Count);
            // Console.WriteLine("input: " + input);
            var realPizza = GetRealPizza(showPizzas[--input]);
            bool added = CurrentCustomer.AddPizza(realPizza);
            do
            {
                CustomizePizza(realPizza);
                added = CurrentCustomer.AddPizza(realPizza);
            } while (!added);

        }

        public APizza CustomizePizza(APizza pizza)
        {
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("your current pizza is: ");
                Console.WriteLine(pizza);
                PrintPizzaCustomizeMenu();
                var actionChoice = ReadIntInput(0, 3);
                switch (actionChoice)
                {
                    case 1: pizza = CustomizeTopping(pizza); break;
                    case 2: pizza = ChangeCrust(pizza); break;
                    case 3: pizza = ChangeSize(pizza); break;
                    case 0: loop = false; break;
                    default: break;
                }
            }
            return pizza;
        }

        public void RemovePizzaMenu()
        {
            var addedPizzas = CurrentCustomer.CurrentOrder.GetPizzas();
            PrintInstruction("choose a pizza to remove");
            PrintAddedPizzaMenu(addedPizzas);
            var input = ReadIntInput(0, addedPizzas.Count);
            if (input == 0)
            {
                return;
            }
            CurrentCustomer.RemovePizza(addedPizzas[--input]);
        }

        public APizza GetRealPizza(APizza showPizza)
        {
            // if (showPizza.Name.Contains("Custom"))
            // {
            //     CustomizePizza(showPizza);
            // }
            showPizza.AddDefaultToppings();
            showPizza.AddDefaultCrust();
            showPizza.AddDefaultSize();
            // Console.WriteLine("show pizza: " + showPizza);
            return showPizza;
            // showPizzas.
            // foreach (var pizza in showPizzas)
            // {

            // }
        }
        /* #endregion */

        /* #region Change Crust and Size */
        public APizza ChangeCrust(APizza pizza)
        {
            var crustList = GetAllCrusts();
            PrintPizza(pizza);
            PrintInstruction("choose a crust");
            PrintTypeOptionList(crustList);
            var input = ReadIntInput(0, crustList.Count);
            if (input == 0)
            {
                return pizza;
            }
            pizza.SetCrust(crustList[--input]);
            return pizza;
        }

        public APizza ChangeSize(APizza pizza)
        {
            var sizeList = GetAllSizes();
            PrintPizza(pizza);
            PrintTypeOptionList(sizeList);
            var input = ReadIntInput(0, sizeList.Count);
            if (input == 0)
            {
                return pizza;
            }
            pizza.SetSize(sizeList[--input]);
            return pizza;
        }

        /* #endregion */

        /* #region Customize Toppings */
        public APizza CustomizeTopping(APizza pizza)
        {
            bool loop = true;
            while (loop)
            {
                var toppings = GetAllToppings();
                PrintPizza(pizza);
                PrintToppingMenu();
                var toppingAction = ReadIntInput(0, 2);
                switch (toppingAction)
                {
                    case 1: AddTopping(pizza); break;
                    case 2: RemoveTopping(pizza); break;
                    case 0: loop = false; break;
                }
                //todo: allow user to exit
                if (!pizza.IsPizzaToppingsOk())
                {
                    loop = true;
                }
            }
            return pizza;
        }

        //TODO: only show available toppings
        public APizza AddTopping(APizza pizza)
        {
            List<Topping> toppings = GetAllToppings();
            PrintPizza(pizza);
            PrintInstruction("available toppings are");
            PrintTypeOptionList(toppings);
            var input = ReadIntInput(0, toppings.Count);
            if (input == 0)
            {
                return pizza;
            }
            pizza.AddTopping(toppings[--input]);
            return pizza;
        }

        public APizza RemoveTopping(APizza pizza)
        {
            var toppingList = pizza.GetAddedToppings();
            PrintInstruction("added toppings are");
            PrintTypeOptionList(toppingList);
            var input = ReadIntInput(0, toppingList.Count);
            if (input == 0)
            {
                return pizza;
            }
            pizza.RemoveTopping(toppingList[--input]);
            return pizza;
        }

        /* #endregion */

        /* #region Input Validation */
        public int ReadIntInput(int min, int max)
        {
            bool success = false;
            int i = 0;
            while (!success)
            {
                PrintInstruction("enter a whole number between " + min + " and " + max);
                string line = Console.ReadLine();
                line = line.Trim();
                success = Int32.TryParse(line, out i);
                if (!(i <= max || i >= min))
                {
                    success = false;
                }
            }
            return i;
        }

        public string ReadStringInput(bool hide = false)
        {
            return Console.ReadLine();
        }

        /* #endregion */

        /* #region Console Prints */
        private void PrintWelcomeMessage()
        {
            PrintInfo("Welcome to the PizzaBox App");
        }

        private int ChooseMainMenu()
        {
            PrintWelcomeMessage();
            PrintInstruction("choose your next action");
            PrintInfo("You must have an account before ordering");
            PrintOption(1, "login");
            PrintOption(2, "register");
            PrintOption(0, "exit");
            return ReadIntInput(0, 2);
        }

        private void PrintThankYouMessage(Order order)
        {
            // int i = Console.CursorTop;
            Console.Clear();
            PrintInfo("Your order was successful");
            Console.WriteLine(order);
        }

        /// <summary>
        /// 0-4 options
        /// </summary>
        public void PrintOrderActionMenu()
        {
            PrintInstruction("choose what you want to do");
            PrintOption(1, "Add Pizza");
            PrintOption(2, "Remove Pizza");
            PrintOption(3, "Custmoize a pizza");
            PrintOption(4, "Checkout");
            PrintOption(0, "Go Back");
        }

        private void PrintAddedPizzaMenu(List<APizza> addedPizzas)
        {
            PrintInfo("you order is");
            Console.WriteLine(CurrentCustomer.CurrentOrder);
            for (int i = 0; i < addedPizzas.Count; i++)
            {
                PrintOption((i + 1), "" + addedPizzas[i]);
            }
            PrintOption(0, "go back");
        }

        public void PrintPizza(APizza pizza)
        {
            PrintInfo("your pizza is");
            Console.WriteLine(pizza);
        }

        /// <summary>
        /// 0-2 options
        /// </summary>
        public void PrintToppingMenu()
        {
            PrintInstruction("choose an action");
            PrintOption(1, "Add a topping");
            PrintOption(2, "Remove a topping");
            PrintOption(0, "Go Back");
        }

        /// <summary>
        /// 0-3 options
        /// </summary>
        public void PrintPizzaCustomizeMenu()
        {
            PrintInstruction("choose something to customize or change");
            PrintOption(1, "toppings");
            PrintOption(2, "crust");
            PrintOption(3, "size");
            PrintOption(0, "go back");
        }

        public void PrintTypeOptionList<T>(List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                PrintOption((i + 1), "" + list[i]);
            }
            PrintOption(0, "go back");
        }

        public void PrintStores(List<AStore> stores)
        {
            PrintInstruction("type a number before the store you want to choose");
            for (int i = 0; i < stores.Count; i++)
            {
                // Console.WriteLine("[" + i + "] : " + stores[i]);
                PrintOption((i + 1), "" + stores[i]);
            }
            PrintOption(0, "go back");
        }

        public void PrintShowPizzas(List<APizza> pizzas)
        {
            PrintInstruction("type a number before the pizza you want to choose");
            for (int i = 0; i < pizzas.Count; i++)
            {
                PrintOption((i + 1), "" + pizzas[i].Name);
            }
            PrintOption(0, "Go Back");
        }

        public void PrintStartingSequence()
        {
            Console.Clear();
            var preSymbols = 0;
            var postSymbols = 0;
            char baseS = '>';
            string pre = "";
            string post = "";
            for (int i = 0; i < 8; i++)
            {
                preSymbols = i + 1;
                postSymbols = 8 - i - 1;
                pre = new string(baseS, preSymbols);
                post = new string(baseS, postSymbols);
                Console.WriteLine(pre + "[Starting order process]" + post);
                System.Threading.Thread.Sleep(100);
                Console.SetCursorPosition(0, 0);
            }
        }

        private void PrintInfo(string v)
        {
            Console.WriteLine("~{" + v + "}~");
        }

        public void PrintInstruction(string inst)
        {
            Console.WriteLine(" >>> [ Please " + inst + " ] >>>");
        }

        public void PrintOption(int num, string option)
        {
            Console.WriteLine("[" + num + "]: " + option);
        }

        /* #endregion */

        /* #region get show lists */
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

        /* #endregion */
    }
}