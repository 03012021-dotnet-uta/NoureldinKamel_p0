using System.Collections.Generic;
using System.Linq;
using System;
using PizzaBox.Domain.Models;
using PizzaBox.Domain.Abstracts;

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
                    c = db.Customers.Where(c => c.Username == username).First();
                }
                catch (System.Exception)
                {
                    Console.WriteLine("customer not found");
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
    }
}