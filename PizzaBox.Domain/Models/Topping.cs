using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain.Models
{
    public enum ToppingType
    {
        Mozirilla, ShedarCheese, Pepperoni, Pineapple, Tomatoes, Mushroom, Onion, Meat, Chicken
    }

    public class Topping : AComponent<ToppingType>
    {
        public Topping(ToppingType type) : base(type)
        {
        }
    }
}