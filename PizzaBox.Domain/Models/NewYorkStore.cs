using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain.Models
{
    public class NewYorkStore : AStore
    {
        public NewYorkStore() : base()
        {
            Name = "NewYork's Pizza Store";
        }
    }
}