namespace PizzaBox.Domain.Abstracts
{
    public enum Size { Small, Medium, Large }
    public abstract class APizza
    {
        public Size PizzaSize { get; set; }
        public string Name { get; set; } // property
    }
}