using System;
using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain.Models
{
    public enum CrustType
    {
        Thin, Thick, Stuffed, Pan
    }
    public class Crust : AComponent<CrustType>
    {
        public Crust() : base()
        {

        }
        public Crust(CrustType type) : base(type)
        {
        }

        public Crust(Crust crust)
        {
            this.Type = crust.Type;
            this.Price = crust.Price;
        }
    }
}