using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain.Models
{
    public enum ToppingType
    {
        Mozirilla, ShedarCheese, Pepperoni, Pineapple, Tomatoes, Mushroom, Onion
    }

    public class Topping : AComponent
    {
        public ToppingType Type { get; set; }
    }
}