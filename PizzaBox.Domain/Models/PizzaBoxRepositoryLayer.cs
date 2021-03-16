using System.Collections.Generic;
using System.Linq;
using System;
using PizzaBox.Domain.Models;
using PizzaBox.Domain.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace PizzaBox.Domain.Models
{
    /* #region Customer Methods */
    public class PizzaBoxRepositoryLayer
    {

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

        /// <summary>
        /// check if customer has an order saved in the db
        /// if yes, delete that order
        /// if no, just do nothing
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        internal bool DeleteOrderIfExists(Customer customer)
        {
            using (var db = new DbContextClass())
            {
                Order o = null;
                try
                {
                    o = db.Orders.Where(o => o.OrderId == db.Customers.Where(c => c.CustomerId == customer.CustomerId).First().CurrentOrder.OrderId).First();
                }
                catch (System.Exception)
                {
                    customer.CurrentOrder = null;
                    return true;
                }
                customer.CurrentOrder = null;
                // db.Database.ExecuteSqlRaw("UPDATE dbo.Customers SET CurrentOrderOrderId = null WHERE CustomerId = '" + customer.CustomerId + "'");
                db.Update(customer);
                // db.SaveChanges();
                db.Orders.Remove(o);
                return db.SaveChanges() > 0;
            }
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
                if (!db.Orders.Contains(customer.CurrentOrder))
                {
                    SaveNewOrder(customer.CurrentOrder);
                }
                else
                {
                    customer.CurrentOrder.Pizzas.ForEach(pizza =>
                    {
                        if (!db.Pizzas.Contains(pizza))
                        {
                            db.Pizzas.Add(pizza);
                        }
                        else
                        {
                            pizza.ToppingList.ForEach(topping =>
                            {
                                if (!db.Toppings.Contains(topping))
                                {
                                    db.Toppings.Add(topping);
                                }
                            });
                            if (!db.Crusts.Contains(pizza.PizzaCrust))
                            {
                                db.Crusts.Add(pizza.PizzaCrust);
                            }
                            if (!db.Sizes.Contains(pizza.PizzaSize))
                            {
                                db.Sizes.Add(pizza.PizzaSize);
                            }
                        }
                    });
                    db.SaveChanges();
                }
                // try
                // {
                //     // db.DbContextOptionsBuilder.EnableSensitiveDataLogging = true;
                //     db.Orders.Where(o => o.OrderId == customer.CurrentOrder.OrderId).First();
                // }
                // catch (System.Exception e)
                // {
                //     Console.WriteLine("couldn't find it " + e.Message + "\n" + e.StackTrace);
                //     SaveNewOrder(customer.CurrentOrder);
                // }
                try
                {
                    db.Update(customer.CurrentOrder);
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

        // public bool AddPizza(APizza pizza)
        // {
        //     using (var db = new DbContextClass())
        //     {
        //         try
        //         {

        //         }
        //         catch (System.Exception)
        //         {

        //             throw;
        //         }
        //     }
        // }

        public bool RemovePizzaFromDBOrder(Order order, APizza pizza)
        {
            using (var db = new DbContextClass())
            {
                if (!db.Orders.Contains(order))
                {
                    return true;
                }
                if (!db.Pizzas.Contains(pizza))
                {
                    return true;
                }
                db.Pizzas.Remove(pizza);
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

        public bool DeleteCustomer(Customer customer)
        {
            using (var db = new DbContextClass())
            {
                db.Customers.Remove(customer);
                return db.SaveChanges() > 0;
            }
        }

        /* #endregion */
        public Dictionary<Customer, List<Order>> GetAllFinishedOrders()
        {
            // customerOrders = new Dictionary<Customer, List<Order>>();
            Dictionary<Customer, List<Order>> os = new Dictionary<Customer, List<Order>>();
            using (var db = new DbContextClass())
            {
                // db.Orders.EntityType.GetProperty("CustomerId").;
                db.Customers.Include(c => c.FinishedOrders).ToList().ForEach(c =>
                  {
                      //   Console.WriteLine("customer? " + c.Username);
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
                              //       Console.WriteLine("order? " + order);
                          }
                      }
                      //   Console.WriteLine(c.FinishedOrders);
                      os.Add(c, c.FinishedOrders);
                  });
                return os;

                // orders = db.Database.ExecuteSqlRaw("SELECT * FROM dbo.Orders WHERE CustomerId NOT null");
                // db.Orders.Where(o=>o.Property());
            }
        }

        // public Dictionary<Customer, List<Order>> GetPizzaRevenue(Dictionary<Customer, List<Order>> customerOrders)
        // {
        //     if (customerOrders.Count <= 0)
        //     {
        //         customerOrders = GetAllFinishedOrders();
        //     }
        //     List<APizza> pizzas = new List<APizza>();
        //     foreach (var pair in customerOrders)
        //     {
        //         pair.Value.ForEach(o =>
        //         {
        //             pizzas.AddRange(o.Pizzas);
        //         });
        //     }
        //     using (var db = new DbContextClass())
        //     {
        //         foreach (var pair in customerOrders)
        //         {

        //         }
        //     }
        // }
    }
}