using System;
using System.Collections.Generic;
using System.Linq;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Models;
using PizzaBox.Storing;
// using System.Data.Objects.SqlClient;
namespace PizzaBox.Domain.Singletons
{

    public class StoreOrderManager
    {
        public static StoreOrderManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StoreOrderManager();
                }
                return _instance;
            }
        }

        private static StoreOrderManager _instance;


        private StoreOrderManager()
        {
        }

        public void StartStoreApp()
        {
            PrintStartingSequence();
            ExistOrOrder();
        }

        // - select options for order history, sales
        // - if order history
        // - select options for all store orders and orders associated to a user (filtering)
        // - if sales
        // - see pizza type, count, revenue by week or by month

        // - [required] each store should be able to view any and [all ✔️] of their placed orders
        // - [required] each store should be able to view any and all of their sales (weekly, monthly, quarterly)
        public void ExistOrOrder()
        {
            bool loop = true;
            bool end = false;
            while (loop)
            {
                var customerOrders = new PizzaBoxRepositoryLayer().GetAllFinishedOrders();
                PrintInstruction("choose an option");
                PrintOption(0, "exit");
                PrintOption(1, "view order history");
                PrintOption(2, "view revenue by a time period");
                PrintOption(3, "view revenue by pizza type");
                var input = ReadIntInput(0, 2);
                switch (input)
                {
                    case 1: ViewOrderHistory(customerOrders); break;
                    case 2: ViewRevenueTimely(customerOrders); break;
                    case 3: ViewRevenuePizza(customerOrders); break;
                    case 0: loop = false; break;
                }
                if (end)
                    break;
            }
        }

        private void ViewRevenueTimely(Dictionary<Customer, List<Order>> customerOrders)
        {
            bool d = false;
            bool m = false;
            var input = PrintTimeChoices();
            switch (input)
            {
                case 1: d = true; break;
                case 2: m = true; break;
                case 3: break;
                case 0: return;
            }
            // SqlFunctions.DateDiff();
            List<Order> orders = new List<Order>();
            foreach (var pair in customerOrders)
            {
                orders.AddRange(pair.Value);
            }
            var kofta = orders.GroupBy(order => new
            {
                OrderDate = d ? order.date.Day : m ? order.date.Month : order.date.Year,
                // pizzas = order.Pizzas
            }).Select(s => new
            {
                // pizza = s.Key.pizzas,
                price = s.Sum(x => x.TotalPrice),
                date = s.Key.OrderDate
            });
            Console.WriteLine(d ? "DAY\t" : m ? "MONTH\t" : "YEAR\t" + "REVENUE");
            foreach (var item in kofta)
            {
                Console.WriteLine(item.date + "\t$" + GetPrintPrice(item.price));
            }
        }

        private int PrintTimeChoices()
        {
            PrintInstruction("choose what you want to order by");
            PrintOption(1, "day");
            PrintOption(2, "month");
            PrintOption(3, "year");
            PrintOption(0, "go back");
            return ReadIntInput(0, 3);
        }

        private void ViewRevenuePizza(Dictionary<Customer, List<Order>> customerOrders)
        {
            List<APizza> pizzas = new List<APizza>();
            foreach (var pair in customerOrders)
            {
                pair.Value.ForEach(Order =>
                {
                    pizzas.AddRange(Order.Pizzas);
                });
            }
            var kofta = pizzas.GroupBy(pizza => new
            {
                // OrderDate = order.date.Day,
                // pizzas = order.Pizzas
                pizzaName = pizza.Name,
            }).Select(s => new
            {
                pizzaName = s.Key.pizzaName,
                price = s.Sum(x => x.CalculateTotalPrice()),
                count = s.Count()
            });
            Console.WriteLine("PIZZA\t\t" + "PRICE\t" + "NO OF PIZZAS");
            foreach (var item in kofta)
            {
                Console.WriteLine(item.pizzaName + "\t$" + GetPrintPrice(item.price) + "\t" + item.count);
            }
        }

        public void ViewOrderHistory(Dictionary<Customer, List<Order>> customerOrders)
        {
            // PrintInstruction("View order not yet implemented");
            if (customerOrders != null && customerOrders.Keys != null && customerOrders.Keys.Count > 0)
            {
                double TotalPrice = 0;
                Console.WriteLine("CUSTOMER\tSTORE\t\t\t\tORDERID\t\t\t\t\t\tPRICE");
                foreach (var customerOrdersPair in customerOrders)
                {
                    // Console.WriteLine("count: " + customerOrdersPair.Value.Count);
                    if (customerOrdersPair.Value.Count <= 0)
                    {
                        continue;
                    }
                    customerOrdersPair.Value.ForEach(order =>
                    {
                        Console.WriteLine(customerOrdersPair.Key.Username + "\t\t" + order.Store.Name + "\t\t" + order.OrderId + "\t\t" + GetPrintPrice(order.TotalPrice));
                        TotalPrice += order.TotalPrice;
                    });
                }
                Console.WriteLine("Total Revenue: $" + GetPrintPrice(TotalPrice));
            }
        }

        public string GetPrintPrice(double num)
        {
            return string.Format("{0:0.00}", num);
        }

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
    }
}