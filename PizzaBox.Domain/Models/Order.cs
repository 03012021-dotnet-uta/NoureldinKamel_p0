using System.Collections.Generic;
using System;
using PizzaBox.Domain.Abstracts;
using System.ComponentModel.DataAnnotations;

namespace PizzaBox.Domain.Models
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; } = Guid.NewGuid();

        public List<APizza> Pizzas { get; set; }

        public float TotalPrice { get; set; }

        public DateTime date { get; set; }

        public AStore Store { get; set; }

        private int MAX_PIZZA_COUNT = 50;
        private int MAX_ORDER_PRICE = 250;

        public bool IsActive { get; set; } = false;

        public Order()
        {
            Pizzas = new List<APizza>();
            TotalPrice = 0;
            date = DateTime.Now;
        }

        public List<APizza> GetPizzas()
        {
            return Pizzas;
        }

        public bool AddPizza(APizza pizza)
        {
            if (!(IsBelowMaxPrice(pizza.CalculateTotalPrice()) && IsBelowMaxPizzas(1) && IsPizzaToppingsOk(pizza)))
            {
                return false;
            }

            Pizzas.Add(pizza);

            TotalPrice += pizza.CalculateTotalPrice();
            date = DateTime.Now;
            return true;
        }

        public bool RemovePizza(APizza pizza)
        {
            if (Pizzas.Remove(pizza))
            {
                TotalPrice -= pizza.CalculateTotalPrice();
                return true;
            }
            return false;
        }


        /// <summary>
        /// return true if total price after adding is below MAX_ORDER_PRICE
        /// </summary>
        /// <param name="pizzaPrice"></param>
        /// <returns></returns>
        private bool IsBelowMaxPrice(float pizzaPrice)
        {
            if (TotalPrice + pizzaPrice > MAX_ORDER_PRICE)
            {
                Console.WriteLine("the total price must be below " + MAX_ORDER_PRICE);
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// return true if no of pizzas is below MAX_PIZZA_COUNT after adding
        /// </summary>
        /// <param name="countAdded"></param>
        /// <returns></returns>
        private bool IsBelowMaxPizzas(int countAdded)
        {
            if (Pizzas.Count + countAdded > MAX_PIZZA_COUNT)
            {
                Console.WriteLine("The maximum number of pizzas is " + MAX_PIZZA_COUNT);
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// returns true if toppings > 5 and < 2
        /// </summary>
        /// <param name="pizza"></param>
        /// <returns></returns>
        private bool IsPizzaToppingsOk(APizza pizza)
        {
            return pizza.IsPizzaToppingsOk();
        }

        // public float CalculateTotalCost()
        // {
        //     float total = 0;
        //     foreach (KeyValuePair<APizza, short> item in Pizzas)
        //     {
        //         total += item.Key.CalculateTotalPrice() * item.Value;
        //     }
        //     return total;
        // }

        public override string ToString()
        {
            string s = "\tOrder from " + Store + " containing:\n";
            // string s = "[$=======================================$]\nOrder contains:";
            // float total = 0;
            foreach (APizza item in Pizzas)
            {
                s += item + "\n\twith price: " + item.CalculateTotalPrice() + "\n";
                // total += item.Value + item.Key.CalculateTotalPrice();
            }
            s += "\tAnd its total is: $" + TotalPrice + "\n\tat time: " + date;
            // s += "\n$=======================================$]";
            return s;
        }

        public bool CanCheckout()
        {
            if (TotalPrice <= 0)
            {
                Console.WriteLine("{{!!}} you must add at least something {{!!}}");
                return false;
            }
            return true;
        }
    }
}