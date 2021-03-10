using System.Collections.Generic;
using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Abstracts
{
    // public enum Size { Small, Medium, Large }
    // public enum Crust { Thick, Thin, Pan }
    // public enum Topping { pepperoni, Onions, Tomatoes, Pineapples, Chicken, Meat, Mushrooms, CheddarCheese, MozirillaCheese }
    public class VeganPizza : APizza
    {

        // protected override void AddDefaultToppings()
        // {
        //     ToppingList = new List<Topping>()
        //     {
        //         // new Topping(),
        //         // new Topping(),
        //     };
        // }
        public VeganPizza()
        {

        }
        // private List<ToppingType> 
        public VeganPizza(Size size, Crust crust) : base(size, crust)
        {
            DefaultToppings = new List<ToppingType>() { ToppingType.Tomatoes, ToppingType.Onion, ToppingType.Mushroom };
        }
    }
}