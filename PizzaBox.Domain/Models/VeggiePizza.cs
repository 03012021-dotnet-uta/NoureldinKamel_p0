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
        public VeggiePizza()
        {

        }
        // private List<ToppingType> 
        public VeggiePizza(Size size, Crust crust) : base(size, crust)
        {
        }

        protected override void InitializeToppings()
        {
            DefaultToppings = new List<ToppingType>() { ToppingType.Tomatoes, ToppingType.ShedarCheese, ToppingType.Mushroom, ToppingType.Onion };
        }
    }
}