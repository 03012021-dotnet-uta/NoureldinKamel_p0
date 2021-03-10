using System.Collections.Generic;
using System.Xml.Serialization;
using PizzaBox.Domain.Models;
using PizzaBox.Domain.Singletons;

namespace PizzaBox.Domain.Abstracts
{
    // public enum Size { Small, Medium, Large }
    // public enum Crust { Thick, Thin, Pan }
    // public enum Topping { pepperoni, Onions, Tomatoes, Pineapples, Chicken, Meat, Mushrooms, CheddarCheese, MozirillaCheese }    [XmlInclude(typeof(NewYorkStore))]
    [XmlInclude(typeof(MeatPizza))]
    [XmlInclude(typeof(VeggiePizza))]
    [XmlInclude(typeof(VeganPizza))]
    [XmlInclude(typeof(CustomPizza))]
    public abstract class APizza
    {
        protected List<ToppingType> DefaultToppings = new List<ToppingType>();
        protected Size PizzaSize { get; set; }
        protected Crust PizzaCrust { get; set; }
        protected List<Topping> ToppingList { get; set; }
        public string Name { get; set; } // property

        private int MAX_TOPPING = 5;

        private int MIN_TOPPING = 2;

        public APizza()
        {

        }

        public APizza(Size size, Crust crust)
        {
            FactoryMethod(size, crust);
        }

        private void FactoryMethod(Size size, Crust crust)
        {
            SetCrust(crust);
            SetSize(size);
            AddDefaultToppings();
        }

        // protected abstract void AddCrust(Crust crust);
        // protected abstract void AddSize(Size size);
        public void AddDefaultToppings()
        {
            foreach (var topType in DefaultToppings)
            {
                float price = PriceManager.Instance.getPrice(topType);
                ToppingList.Add(new Topping(topType) { Price = price });

                // foreach (var toppingObject in toppingPriceList)
                // {
                //     if (toppingObject.Type == topType)
                //     {
                //         ToppingList.Add(toppingObject);
                //         break;
                //     }
                // }

            }
        }

        public void AddDefaultCrust()
        {
            float price = PriceManager.Instance.getPrice(CrustType.Thick);
            SetCrust(new Crust(CrustType.Thick) { Price = price });
        }

        public void AddDefaultSize()
        {
            float price = PriceManager.Instance.getPrice(SizeType.Medium);
            SetSize(new Size(SizeType.Medium) { Price = price });
        }

        public bool SetCrust(Crust crust)
        {
            PizzaCrust = crust;
            return true;
        }

        public bool SetSize(Size size)
        {
            PizzaSize = size;
            return true;
        }

        public bool CanAddMoreTopping()
        {
            bool b = ToppingList.Count < 5;
            if (!b)
            {
                System.Console.WriteLine("Reached maximum toppings of " + MAX_TOPPING);
            }
            return ToppingList.Count < 5;
        }

        public bool AddTopping(Topping topping)
        {
            if (CanAddMoreTopping() && !DoesToppingExist(topping, true))
            {
                ToppingList.Add(topping);
                return true;
            }
            return false;
        }

        public bool RemoveTopping(Topping topping)
        {
            if (DoesToppingExist(topping, false))
            {
                return ToppingList.Remove(topping);
            }
            System.Console.WriteLine("topping " + topping + "was not added yet");
            return false;
        }

        public List<Topping> GetAddedToppings()
        {
            return new List<Topping>(ToppingList);
        }

        public bool DoesToppingExist(Topping topping, bool print)
        {
            foreach (var t in ToppingList)
            {
                if (t.Type == topping.Type)
                {
                    if (print)
                    {
                        System.Console.WriteLine("topping: " + t + " already added");
                    }
                    return true;
                }
            }
            return false;
        }

        public bool IsPizzaToppingsOk()
        {
            if (ToppingList.Count > MAX_TOPPING || ToppingList.Count < MIN_TOPPING)
            {
                System.Console.WriteLine("A pizza must contain between " + MAX_TOPPING + " and " + MIN_TOPPING + " toppings");
                return false;
            }
            return true;
        }

        public float CalculateTotalPrice()
        {
            float total = PizzaCrust.Price;
            total += PizzaSize.Price;
            foreach (var item in ToppingList)
            {
                total += item.Price;
            }
            return total;
        }


        public override string ToString()
        {
            string s = "A pizza with toppings: ";
            foreach (var item in ToppingList)
            {
                s += "\n\t" + item;
            }
            s += "\nwith crust: " + PizzaCrust;
            s += "\nof size: " + PizzaSize;
            return s;
        }
    }
}