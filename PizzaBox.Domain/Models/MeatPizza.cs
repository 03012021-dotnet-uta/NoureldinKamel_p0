using System.Collections.Generic;
using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Abstracts
{
    // public enum Size { Small, Medium, Large }
    // public enum Crust { Thick, Thin, Pan }
    // public enum Topping { pepperoni, Onions, Tomatoes, Pineapples, Chicken, Meat, Mushrooms, CheddarCheese, MozirillaCheese }
    public class MeatPizza : APizza
    {
        protected override void AddCrust()
        {
            PizzaCrust = new Crust();
        }

        protected override void AddSize()
        {
            PizzaSize = new Size();
        }

        protected override void AddToppings()
        {
            ToppingList = new List<Topping>()
            {
                new Topping()
                {
                    Name = "Meat",
                    Price = 1
                },
                new Topping()
                {
                    Name = ""
                }
            };
        }
        // public MeatPizza()
        // {
        //     PizzaCrust = new Crust();
        //     PizzaSize = new Size();
        //     ToppingList = new List<Topping>();
        // }

        // public MeatPizza(Crust crust, Size size)
        // {
        //     PizzaCrust = crust;
        //     PizzaSize = size;
        //     ToppingList = new List<Topping>();
        // }


    }
}