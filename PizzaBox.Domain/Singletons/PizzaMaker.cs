// using System.Collections.Generic;
// using PizzaBox.Domain.Abstracts;
// using PizzaBox.Domain.Models;

// namespace PizzaBox.Domain.Singletons
// {
//     class PizzaMaker
//     {

//         private APizza CurrentPizza { get; set; }

//         public static PizzaMaker Instance
//         {
//             get
//             {
//                 if (_instance == null)
//                 {
//                     _instance = new PizzaMaker();
//                 }
//                 return _instance;
//             }
//         }

//         private static PizzaMaker _instance;

//         private PizzaMaker()
//         {

//         }

//         public void SetPizza(APizza pizza)
//         {
//             CurrentPizza = pizza;
//         }

//         public bool ChangeSize(Size size)
//         {
//             if (IsPizzaNull())
//             {
//                 return false;
//             }
//             // CurrentPizza.PizzaSize = new Size(size);
//             CurrentPizza.SetSize(size);
//             return true;
//         }

//         public bool ChangeCrust(Crust crust)
//         {
//             if (IsPizzaNull())
//             {
//                 return false;
//             }
//             // CurrentPizza.PizzaSize = new Size(size);
//             CurrentPizza.SetCrust(crust);
//             return true;
//         }

//         public bool RemoveTopping(Topping topping)
//         {
//             if (IsPizzaNull())
//             {
//                 return false;
//             }
//             return CurrentPizza.RemoveTopping(topping);
//         }

//         public List<Topping> GetAddedToppings()
//         {
//             if (IsPizzaNull())
//             {
//                 return null;
//             }
//             return CurrentPizza.GetAddedToppings();
//         }

//         public bool AddTopping(Topping topping)
//         {
//             if (IsPizzaNull())
//             {
//                 return false;
//             }
//             // if (CurrentPizza.DoesToppingExist(topping))
//             // {
//             //     return false;
//             // }
//             // CurrentPizza.ToppingList.Add(topping);
//             // bool b = CurrentPizza.IsPizzaToppingsOk();
//             // if (!b)
//             // {
//             //     System.Console.WriteLine("the topping " + topping + " was not added");
//             //     CurrentPizza.ToppingList.Remove(topping);
//             // }
//             // return b;
//             return CurrentPizza.AddTopping(topping);
//         }

//         public bool IsPizzaNull()
//         {
//             if (CurrentPizza == null)
//             {
//                 System.Console.WriteLine("you must choose a pizza first");
//                 return true;
//             }
//             return false;
//         }

//         public List<Topping> GetAllToppings()
//         {
//             System.Console.WriteLine("You can choose at least 2 toppings and up to 5");
//             var dict = PriceManager.Instance.GetToppings();
//             List<ToppingType> toppTypes = new List<ToppingType>(dict.Keys);
//             List<Topping> topps = new List<Topping>();
//             foreach (var pair in dict)
//             {
//                 topps.Add(new Topping(pair.Key)
//                 {
//                     Price = pair.Value
//                 });
//             }
//             return topps;
//         }

//         public List<Crust> GetAllCrusts()
//         {
//             var dict = PriceManager.Instance.GetCrusts();
//             List<CrustType> crustTypes = new List<CrustType>(dict.Keys);
//             List<Crust> crusts = new List<Crust>();
//             foreach (var pair in dict)
//             {
//                 crusts.Add(new Crust(pair.Key)
//                 {
//                     Price = pair.Value
//                 });
//             }
//             return crusts;
//         }

//         public List<Size> GetAllSizes()
//         {
//             var dict = PriceManager.Instance.GetSizes();
//             List<SizeType> sizeTypes = new List<SizeType>(dict.Keys);
//             List<Size> sizes = new List<Size>();
//             foreach (var pair in dict)
//             {
//                 sizes.Add(new Size(pair.Key)
//                 {
//                     Price = pair.Value
//                 });
//             }
//             return sizes;
//         }
//     }
// }