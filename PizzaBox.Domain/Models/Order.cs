using System.Collections.Generic;
using System;

namespace PizzaBox.Domain.Abstracts
{
    public class Order
    {
        private Dictionary<APizza, short> Pizzas { get; set; }

        public float TotalPrice { get; set; }

        public DateTime date { get; set; }

        public Order()
        {
            Pizzas = new Dictionary<APizza, short>();
            TotalPrice = 0;
            date = DateTime.Now;
        }

        public bool AddPizza(APizza pizza)
        {
            if (!Pizzas.ContainsKey(pizza))
            {
                Pizzas.Add(pizza, 1);
            }
            else
            {
                Pizzas[pizza]++;
            }
            TotalPrice += pizza.CalculateTotalPrice();
            date = DateTime.Now;
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
            string s = "[$=====================$]\nOrder contains: ";
            // float total = 0;
            foreach (KeyValuePair<APizza, short> item in Pizzas)
            {
                s += item.Value + " of:\n" + item.Key;
                // total += item.Value + item.Key.CalculateTotalPrice();
            }
            s += "\nAnd its total is: $" + TotalPrice + "\nat time: " + date;
            return s;
        }
    }
}