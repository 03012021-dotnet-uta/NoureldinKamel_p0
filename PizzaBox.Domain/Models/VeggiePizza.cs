using System.Collections.Generic;
using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Abstracts
{
    // public enum Size { Small, Medium, Large }
    // public enum Crust { Thick, Thin, Pan }
    // public enum Topping { pepperoni, Onions, Tomatoes, Pineapples, Chicken, Meat, Mushrooms, CheddarCheese, MozirillaCheese }
    public class VeggiePizza : APizza
    {

        // protected override void AddDefaultToppings()
        // {
        //     ToppingList = new List<Topping>()
        //     {
        //         // new Topping(),
        //         // new Topping(),
        //     };
        // }
        private List<ToppingType> DefaultToppings = new List<ToppingType>() { ToppingType.Tomatoes, ToppingType.ShedarCheese, ToppingType.Mushroom, ToppingType.Onion };
        public VeggiePizza(Size size, Crust crust) : base(size, crust)
        {
        }
    }
}