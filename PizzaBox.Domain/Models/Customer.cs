using System;
using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Singletons;

namespace PizzaBox.Domain.Models
{
    public class Customer
    {
        public string Name { get; set; }

        public List<Order> FinishedOrders { get; set; }

        public Order CurrentOrder { get; set; }

        public Customer()
        {
            bool e = LoadPreviousOrders();
            if (!e)
            {
                throw new Exception("Couldn't load previous orders");
            }
        }

        public bool StartOrder()
        {
            if (OrderedInLastTwoHours())
            {
                return false;
            }
            if (CurrentOrder == null)
            {
                CurrentOrder = new Order();
                return true;
            }
            return false;
        }

        public bool AddPizza(APizza pizza)
        {
            if (!IsOrderOk())
            {
                return false;
            }
            CurrentOrder.AddPizza(pizza);
            return true;
        }

        public bool RemovePizza(APizza pizza)
        {
            return CurrentOrder.RemovePizza(pizza);
        }

        public bool HasOrderedStoreIn24Hrs(AStore store)
        {
            var now = DateTime.Now;
            var oneDaySpan = new TimeSpan(1, 0, 0, 0);
            foreach (var order in FinishedOrders)
            {
                if (order.Store.GetType() == store.GetType() && Math.Abs(now.Subtract(order.date).TotalHours) < oneDaySpan.TotalHours)
                {
                    return true;
                }
            }
            return false;
        }

        public bool OrderedInLastTwoHours()
        {
            var now = DateTime.Now;
            var twoHrSpan = new TimeSpan(2, 0, 0);
            foreach (var item in FinishedOrders)
            {
                if (Math.Abs(now.Subtract(item.date).TotalHours) < twoHrSpan.TotalHours)
                {
                    return true;
                }
            }
            return false;
        }

        //TODO: load from file
        private bool LoadPreviousOrders()
        {
            FinishedOrders = new List<Order>()
            {
                new Order(),
                new Order(),
                new Order(),
                new Order(),
                new Order()
            };
            return true;
        }

        public bool IsOrderOk()
        {
            if (CurrentOrder == null)
            {
                return false;
            }
            if (CurrentOrder.Store == null)
            {
                return false;
            }
            return true;
        }
    }
}