namespace PizzaBox.Domain.Abstracts
{
    public abstract class AStore
    {
        public string Name { get; protected set; } // property

        public override string ToString()
        {
            return base.ToString();
        }
    }
}