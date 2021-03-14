using System.Collections.Generic;
using System.Linq;
using System;
using PizzaBox.Domain.Models;
using PizzaBox.Domain.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace PizzaBox.Domain.Models
{
    public class PizzaBoxRepositoryLayer
    {
        // List<Customer> Customers = new List<Customer>();
        // List<APizza> APizzas = new List<APizza>();
        // List<Order> Orders = new List<Order>();
        // List<AStore> AStores = new List<AStore>();
        // List<Size> Sizes = new List<Size>();
        // List<Crust> Crusts = new List<Crust>();
        // List<Topping> Toppings = new List<Topping>();

        // DbContextClass db = new DbContextClass();
        // int numberOfChoices = Enum.GetNames(typeof(Choice)).Length; // get a always-current number of options of Enum Choice
        // Random randomNumber = new Random((int)DateTime.Now.Millisecond); // create a random number object

        /// <summary>
        /// Creates a player after verifying that the player does not already exist. returns the player obj
        /// </summary>
        /// <returns></returns>
        // public Customer CreateCustomer(string fName = "null")
        // {
        //     Customer p1 = new Customer();
        //     p1 = Customers.Where(x => x.Fname == fName).FirstOrDefault();

        //     if (p1 == null)
        //     {
        //         p1 = new Customer()
        //         {
        //             Fname = fName,
        //             Lname = lName
        //         };
        //         Customers.Add(p1);
        //     }
        //     return p1;
        // }

        public bool CreateCustomer(Customer customer)
        {
            using (var db = new DbContextClass())
            {
                if (db.Customers.Contains(customer))
                    return false;
                db.Customers.Add(customer);
            }
            return true;
        }

        public bool Save()
        {
            int i = 0;
            using (var db = new DbContextClass())
            {
                i = db.SaveChanges();
            }
            return i > 0;
        }

        public bool Login(string username, string password, out Customer customer)
        {
            using (var db = new DbContextClass())
            {
                Customer c = null;
                try
                {
                    c = db.Customers.Include(c => c.CurrentOrder)
                    .Where(c => c.Username == username)
                    .Include(c => c.FinishedOrders)
                    .Include(c => c.CurrentOrder.Store)
                    .Include(c => c.CurrentOrder.Pizzas)
                    .First();

                    if (c.CurrentOrder != null)
                    {
                        c.CurrentOrder.Pizzas.ForEach(
                            pizza =>
                            {
                                db.Entry(pizza).Reference(p => p.PizzaCrust).Load();
                                db.Entry(pizza).Reference(p => p.PizzaSize).Load();
                                db.Entry(pizza).Collection(p => p.ToppingList).Load();
                            }
                        );
                    }
                    if (c.FinishedOrders.Count > 0)
                    {
                        foreach (var order in c.FinishedOrders)
                        {
                            db.Entry(order).Reference(o => o.Store).Load();
                            db.Entry(order).Collection(o => o.Pizzas).Load();
                            order.Pizzas.ForEach(
                                pizza =>
                                {
                                    db.Entry(pizza).Reference(p => p.PizzaCrust).Load();
                                    db.Entry(pizza).Reference(p => p.PizzaSize).Load();
                                    db.Entry(pizza).Collection(p => p.ToppingList).Load();
                                }
                            );
                        }
                    }
                    // .Include(c => c.CurrentOrder.Pizzas)
                    // .Include(c => c.CurrentOrder.Pizzas.FirstOrDefault().PizzaSize)
                    // .Include(c => c.CurrentOrder.Pizzas.FirstOrDefault().PizzaCrust)
                    // .Include(c => c.CurrentOrder.Pizzas.FirstOrDefault().ToppingList)
                }
                // catch (Microsoft.EntityFrameworkCore.DbUpdateException)
                // {
                //     Console.WriteLine("couldn't ");
                // }
                catch (System.Exception e)
                {
                    Console.WriteLine("customer not found" + e.Message + "\n" + e.StackTrace);
                    customer = null;
                    return false;
                }
                if (c == null)
                {
                    Console.WriteLine("customer not found");
                    customer = null;
                    return false;
                }
                else if (Customer.Compare(c.Password, password))
                {
                    Console.WriteLine("login success");
                    customer = c;
                    // if (c.CurrentOrder != null)
                    // {
                    //     c.CurrentOrder.Pizzas.ForEach(pizza => pizza.PizzaSize = db.Sizes.Where(s => s.ComponentId == pizza.Property()));
                    //     db.Sizes.Where(s => )
                    // }
                    return true;
                }
                else
                {
                    Console.WriteLine("passwords not matching");
                    customer = null;
                    return false;
                }
            }
        }

        public bool CheckUserExists(string username)
        {
            using (var db = new DbContextClass())
            {

                Customer c = null;
                try
                {
                    c = db.Customers.Where(c => c.Username == username).First();
                }
                catch (System.Exception)
                {
                    return false;
                }
                return true;
            }
        }

        internal bool CheckAndCreateNewOrder()
        {
            throw new NotImplementedException();
        }

        public bool Register(Customer customer)
        {
            bool saved = false;
            using (var db = new DbContextClass())
            {
                db.Customers.Add(customer);
                saved = db.SaveChanges() > 0;
            }
            return saved;
        }

        public bool SaveNewOrder(Order order)
        {
            using (var db = new DbContextClass())
            {
                db.Orders.Add(order);
                return db.SaveChanges() > 0;
            }
        }

        public bool SaveCustomerChanges(Customer customer)
        {
            using (var db = new DbContextClass())
            {
                try
                {
                    // db.DbContextOptionsBuilder.EnableSensitiveDataLogging = true;
                    db.Orders.Where(o => o.OrderId == customer.CurrentOrder.OrderId).First();
                }
                catch (System.Exception e)
                {
                    Console.WriteLine("couldn't find it " + e.Message + "\n" + e.StackTrace);
                    SaveNewOrder(customer.CurrentOrder);
                }
                // Customer savedCustomer = null;
                // try
                // {
                //     savedCustomer = db.Customers.Where(c => c.Username == customer.Username).First();
                // }
                // catch (System.Exception)
                // {
                //     Console.WriteLine("user not found");
                //     return false;
                // }
                try
                {
                    db.Update(customer);
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
                {
                    // db.Orders.Add(customer.CurrentOrder);
                    // db.Update(customer);
                    Console.WriteLine("an error occurred while saving your changes: " + e.Message + "\n" + e.StackTrace);
                }
                catch (System.InvalidOperationException e)
                {
                    Console.WriteLine("an error occurred while saving your changes: " + e.Message + "\n" + e.StackTrace);
                }
                return db.SaveChanges() > 0;

            }
        }

        public bool CheckoutCustomer(Customer customer, Order order)
        {
            using (var db = new DbContextClass())
            {
                customer.CurrentOrder = null;
                order.date = DateTime.Now;
                if (db.Orders.Contains(order))
                {
                    db.Update(customer);
                    db.Database.ExecuteSqlRaw("UPDATE dbo.Customers SET CurrentOrderOrderId = null WHERE CustomerId = '" + customer.CustomerId + "'");
                    Console.WriteLine("change? :" + db.SaveChanges());
                }
                customer.FinishedOrders.Add(order);
                db.Update(customer);
                return db.SaveChanges() > 0;
            }
        }
    }
}