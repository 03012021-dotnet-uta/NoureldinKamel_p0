using System.Collections.Generic;
using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Abstracts
{
    // public enum Size { Small, Medium, Large }
    // public enum Crust { Thick, Thin, Pan }
    // public enum Topping { pepperoni, Onions, Tomatoes, Pineapples, Chicken, Meat, Mushrooms, CheddarCheese, MozirillaCheese }
    public abstract class APizza
    {
        public Size PizzaSize { get; set; }
        public Crust PizzaCrust { get; set; }
        public List<Topping> ToppingList { get; set; }
        public string Name { get; set; } // property

        public APizza(SizeType size)
        {
            FactoryMethod(size);
        }

        private void FactoryMethod(SizeType size)
        {
            AddDefaultCrust();
            AddSize(size);
            AddDefaultToppings();
        }

        protected abstract void AddDefaultCrust();
        protected abstract void AddSize(SizeType size);
        protected abstract void AddDefaultToppings();

        public float CalculateTotalPrice()
        {
            float total = PizzaCrust.Price;
            total += PizzaSize.Price;
            foreach (var item in ToppingList)
            {
                total += item.Price;
            }
            return total;
        }

        public override string ToString()
        {
            string s = "A pizza with toppings: ";
            foreach (var item in ToppingList)
            {
                s += "\n\t" + item;
            }
            s += "\nwith crust: " + PizzaCrust;
            s += "\nof size: " + PizzaSize;
            return s;
        }
    }
}