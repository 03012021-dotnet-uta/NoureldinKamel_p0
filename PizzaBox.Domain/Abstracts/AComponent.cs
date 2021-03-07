using System;
using PizzaBox.Domain.Singletons;

namespace PizzaBox.Domain.Abstracts
{
    public abstract class AComponent<T> where T : Enum
    {
        public AComponent(T type)
        {
            Type = type;
        }
        public T Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                Price = PriceManager.Instance.getPrice(_type);
            }
        }
        private T _type;
        public float Price { get; set; }

        public override string ToString()
        {
            return _type + " with price: " + Price;
        }
    }
}