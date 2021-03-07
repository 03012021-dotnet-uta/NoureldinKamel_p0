using System.Collections.Generic;

namespace PizzaBox.Domain.Abstracts
{
    public abstract class AStore
    {
        public string Name { get; set; } // property

        public List<Order> OrderList { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}