using System.Collections.Generic;
using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Abstracts
{
    // public enum Size { Small, Medium, Large }
    // public enum Crust { Thick, Thin, Pan }
    // public enum Topping { pepperoni, Onions, Tomatoes, Pineapples, Chicken, Meat, Mushrooms, CheddarCheese, MozirillaCheese }
    public class VeggiePizza : APizza
    {
        public VeggiePizza(SizeType size) : base(size)
        {
        }
        protected override void AddDefaultCrust()
        {
            // PizzaCrust = new Crust();
        }

        protected override void AddSize(SizeType size)
        {
            // PizzaSize = new Size();
        }

        protected override void AddDefaultToppings()
        {
            ToppingList = new List<Topping>()
            {
                // new Topping(),
                // new Topping(),
            };
        }
    }
}