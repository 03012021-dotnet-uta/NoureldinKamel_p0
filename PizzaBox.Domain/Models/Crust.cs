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
        public Crust(CrustType type) : base(type)
        {
        }
    }
}