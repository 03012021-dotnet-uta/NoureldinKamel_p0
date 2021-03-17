using System;
using System.Collections.Generic;
using PizzaBox.Domain.Models;
using PizzaBox.Storing;

namespace PizzaBox.Domain.Singletons
{
    public class PriceManager
    {
        private Dictionary<ToppingType, float> _toppingPriceDictionary;
        private Dictionary<SizeType, float> _sizePriceDictionary;
        private Dictionary<CrustType, float> _crustPriceDictionary;

        public static PriceManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PriceManager();
                }
                return _instance;
            }
        }

        public Dictionary<ToppingType, float> GetToppings()
        {
            return new Dictionary<ToppingType, float>(_toppingPriceDictionary);
        }

        public Dictionary<SizeType, float> GetSizes()
        {
            return new Dictionary<SizeType, float>(_sizePriceDictionary);
        }

        public Dictionary<CrustType, float> GetCrusts()
        {
            return new Dictionary<CrustType, float>(_crustPriceDictionary);
        }

        private static PriceManager _instance;

        private PriceManager()
        {
            // _crustPriceDictionary = new Dictionary<CrustType, float>()
            // {
            //     {CrustType.Pan, 1.7F},
            //     {CrustType.Thick, 1.5F},
            //     {CrustType.Thin, 1.0F},
            //     {CrustType.Stuffed, 2.1F},
            // };
            // _toppingPriceDictionary = new Dictionary<ToppingType, float>()
            // {
            //     {ToppingType.Mozirilla, 0.3F},
            //     {ToppingType.Mushroom, 0.3F},
            //     {ToppingType.Onion, 0.3F},
            //     {ToppingType.Pepperoni, 0.3F},
            //     {ToppingType.Pineapple, 0.3F},
            //     {ToppingType.ShedarCheese, 0.3F},
            //     {ToppingType.Tomatoes, 0.3F},
            //     {ToppingType.Meat, 0.6F},
            //     {ToppingType.Chicken, 0.6F},
            // };

            // _sizePriceDictionary = new Dictionary<SizeType, float>()
            // {
            //     {SizeType.Small, 2.5F},
            //     {SizeType.Medium, 5.5F},
            //     {SizeType.Large, 6.5F},
            //     {SizeType.Xlarge, 7.0F},
            // };
            var storage = new FileStorage();
            _toppingPriceDictionary = storage.ReadFromXml<ToppingType>(FileType.Toppings);
            _crustPriceDictionary = storage.ReadFromXml<CrustType>(FileType.Crusts);
            _sizePriceDictionary = storage.ReadFromXml<SizeType>(FileType.Sizes);

            // storage.WriteToXml<ToppingType>(FileType.Toppings, _toppingPriceDictionary);
            // storage.WriteToXml<CrustType>(FileType.Crusts, _crustPriceDictionary);
            // storage.WriteToXml<SizeType>(FileType.Sizes, _sizePriceDictionary);
        }

        public float getPrice(Enum enumType)
        {
            if (enumType.GetType() == typeof(CrustType))
            {
                return _crustPriceDictionary[(CrustType)enumType];
            }
            else if (enumType.GetType() == typeof(ToppingType))
            {

                return _toppingPriceDictionary[(ToppingType)enumType];
            }
            else if (enumType.GetType() == typeof(SizeType))
            {
                return _sizePriceDictionary[(SizeType)enumType];
            }
            else
            {
                return 0;
            }
        }
    }
}