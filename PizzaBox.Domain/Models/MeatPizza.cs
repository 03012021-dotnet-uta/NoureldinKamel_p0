using System.Collections.Generic;
using PizzaBox.Domain.Models;
using PizzaBox.Domain.Singletons;

namespace PizzaBox.Domain.Abstracts
{
    // public enum Size { Small, Medium, Large }
    // public enum Crust { Thick, Thin, Pan }
    // public enum Topping { pepperoni, Onions, Tomatoes, Pineapples, Chicken, Meat, Mushrooms, CheddarCheese, MozirillaCheese }
    public class MeatPizza : APizza
    {
        // private List<ToppingType>
        public MeatPizza() : base()
        {

        }
        public MeatPizza(Size size, Crust crust) : base(size, crust)
        {
            DefaultToppings = new List<ToppingType>() { ToppingType.Meat, ToppingType.Mozirilla, ToppingType.Mushroom };
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