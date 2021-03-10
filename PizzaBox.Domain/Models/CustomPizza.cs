using System.Collections.Generic;
using PizzaBox.Domain.Models;
using PizzaBox.Domain.Singletons;

namespace PizzaBox.Domain.Abstracts
{
    // public enum Size { Small, Medium, Large }
    // public enum Crust { Thick, Thin, Pan }
    // public enum Topping { pepperoni, Onions, Tomatoes, Pineapples, Chicken, Meat, Mushrooms, CheddarCheese, MozirillaCheese }
    public class CustomPizza : APizza
    {
        private List<ToppingType> DefaultToppings = new List<ToppingType>();

        public CustomPizza(Size size, Crust crust) : base(size, crust)
        {
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