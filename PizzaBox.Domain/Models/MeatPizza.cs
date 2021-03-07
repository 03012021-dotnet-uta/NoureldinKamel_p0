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
        public MeatPizza(SizeType size) : base(size)
        {
        }

        protected override void AddDefaultCrust()
        {
            PizzaCrust = new Crust(CrustType.Thick);
        }

        protected override void AddSize(SizeType size)
        {
            PizzaSize = new Size(size);
        }

        protected override void AddDefaultToppings()
        {
            ToppingList = new List<Topping>()
            {
                new Topping(ToppingType.Meat),
                new Topping(ToppingType.Onion),
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