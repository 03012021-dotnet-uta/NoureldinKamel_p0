using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Abstracts
{
    [XmlInclude(typeof(APizza))]
    [XmlInclude(typeof(Order))]
    [XmlInclude(typeof(ChicagoStore))]
    [XmlInclude(typeof(FreddyStore))]
    [XmlInclude(typeof(DetroitStore))]
    [XmlInclude(typeof(NewYorkStore))]
    // [assembly]
    public class AStore
    {
        [Key]
        public Guid StoreId { get; set; } = Guid.NewGuid();
        public string Name { get; set; } // property

        protected AStore()
        {
            ActiveOrders = new List<Order>();
        }

        // public virtual ICollection<Order> FullFilledOrders { get; set; }
        // public virtual ICollection<Order> CanceledOrders { get; set; }
        public virtual List<Order> ActiveOrders { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}