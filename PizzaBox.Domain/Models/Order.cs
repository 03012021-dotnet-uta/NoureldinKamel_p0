using System.Collections.Generic;
using System;

namespace PizzaBox.Domain.Abstracts
{
    public class Order
    {
        private List<APizza> Pizzas { get; set; }

        public float TotalPrice { get; set; }

        public DateTime date { get; set; }
        private int MAX_PIZZA_COUNT = 50;
        private int MAX_ORDER_PRICE = 250;

        public Order()
        {
            Pizzas = new List<APizza>();
            TotalPrice = 0;
            date = DateTime.Now;
        }

        public List<APizza> ViewPizzas()
        {
            return new List<APizza>(Pizzas);
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
            return Pizzas.Remove(pizza);
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
            if (pizza.ToppingList.Count > 5 || pizza.ToppingList.Count < 2)
            {
                return false;
            }
            return true;
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
            string s = "[$=====================$]\nOrder contains:";
            // float total = 0;
            foreach (APizza item in Pizzas)
            {
                s += item + "\nwith price: " + item.CalculateTotalPrice();
                // total += item.Value + item.Key.CalculateTotalPrice();
            }
            s += "\nAnd its total is: $" + TotalPrice + "\nat time: " + date;
            return s;
        }
    }
}