using System.Collections.Generic;

namespace PizzaBox.Domain.Abstracts
{
    public abstract class ACustomer
    {
        public string Name { get; set; }

        public List<Order> FinishedOrders { get; set; }
    }
}