using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Abstracts
{
    [XmlInclude(typeof(ChicagoStore))]
    [XmlInclude(typeof(FreddyStore))]
    [XmlInclude(typeof(DetroitStore))]
    [XmlInclude(typeof(NewYorkStore))]
    // [assembly]
    public class AStore
    {
        public string Name { get; set; } // property

        protected AStore()
        {

        }

        public List<Order> OrderList { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}