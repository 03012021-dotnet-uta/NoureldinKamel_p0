using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain.Models
{
    public enum SizeType
    {
        Small, Medium, Large, Xlarge
    }

    public class Size : AComponent<SizeType>
    {
        public Size(SizeType type) : base(type)
        {
        }
    }
}