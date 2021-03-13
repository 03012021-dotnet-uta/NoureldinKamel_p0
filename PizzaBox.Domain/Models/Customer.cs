using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Singletons;

namespace PizzaBox.Domain.Models
{
    public class Customer
    {
        private int MIN_PASS_LENGTH = 8;

        [Key]
        public Guid CustomerId { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        // public class PasswordMapper : EntityTypeConfiguration<Password>
        // {
        //     public PasswordMapper()
        //     {
        //         HasMany(s => s._subscribedList);
        //     }
        // }
        public bool SetPass(string password)
        {
            if (password.Length < MIN_PASS_LENGTH)
            {
                Console.WriteLine("Password length should at least be " + MIN_PASS_LENGTH);
                return false;
            }

            var pHasher = new PasswordHasher();
            Password = pHasher.Hash(password);

            // byte[] salt;
            // new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            // var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            // byte[] hash = pbkdf2.GetBytes(20);
            // //____
            // var data = Encoding.ASCII.GetBytes(password);
            // var sha1 = new SHA1CryptoServiceProvider();
            // var sha1Pass = sha1.ComputeHash(data);
            // Password = Convert.ToBase64String(sha1Pass);
            return true;
        }

        public static bool Compare(string rawPass, string entered)
        {
            return new PasswordHasher().ComparePass(rawPass, entered);
        }
        public virtual List<Order> FinishedOrders { get; set; }
        public Order CurrentOrder { get; set; }

        public Customer()
        {
            bool e = LoadPreviousOrders();
            if (!e)
            {
                throw new Exception("Couldn't load previous orders");
            }
        }

        public bool StartOrder()
        {
            if (OrderedInLastTwoHours())
            {
                return false;
            }
            if (CurrentOrder == null)
            {
                CurrentOrder = new Order();
                return true;
            }
            return false;
        }

        public bool AddPizza(APizza pizza)
        {
            if (!IsOrderOk())
            {
                return false;
            }
            return CurrentOrder.AddPizza(pizza);
        }

        public bool RemovePizza(APizza pizza)
        {
            return CurrentOrder.RemovePizza(pizza);
        }

        public bool HasOrderedStoreIn24Hrs(AStore store)
        {
            var now = DateTime.Now;
            var oneDaySpan = new TimeSpan(1, 0, 0, 0);
            foreach (var order in FinishedOrders)
            {
                if (order.Store.GetType() == store.GetType() && Math.Abs(now.Subtract(order.date).TotalHours) < oneDaySpan.TotalHours)
                {
                    return true;
                }
            }
            return false;
        }

        public bool OrderedInLastTwoHours()
        {
            var now = DateTime.Now;
            var twoHrSpan = new TimeSpan(2, 0, 0);
            foreach (var item in FinishedOrders)
            {
                if (Math.Abs(now.Subtract(item.date).TotalHours) < twoHrSpan.TotalHours)
                {
                    return true;
                }
            }
            return false;
        }

        //TODO: load from file
        private bool LoadPreviousOrders()
        {
            FinishedOrders = new List<Order>()
            {
                // new Order(),
                // new Order(),
                // new Order(),
                // new Order(),
                // new Order()
            };
            return true;
        }

        public bool IsOrderOk()
        {
            if (CurrentOrder == null)
            {
                Console.WriteLine("{{!!}} you have to start an order first {{!!}}");
                return false;
            }
            if (CurrentOrder.Store == null)
            {
                Console.WriteLine("{{!!}} you have to choose a store first {{!!}}");
                return false;
            }
            return true;
        }
        public bool CanOrderCheckout()
        {
            return CurrentOrder.CanCheckout();
        }

        internal void Checkout()
        {
            this.FinishedOrders.Add(CurrentOrder);
            CurrentOrder = null;
        }
    }
    class PasswordHasher
    {
        public string Hash(string pass)
        {
            // STEP 1 Create the salt value with a cryptographic PRNG:
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            // STEP 2 Create the Rfc2898DeriveBytes and get the hash value:
            var pbkdf2 = new Rfc2898DeriveBytes(pass, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            // STEP 3 Combine the salt and password bytes for later use:
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            // STEP 4 Turn the combined salt+hash into a string for storage
            return Convert.ToBase64String(hashBytes);
        }

        public bool ComparePass(string rawSaved, string entered)
        {
            /* Extract the bytes */
            byte[] hashBytes = Convert.FromBase64String(rawSaved);
            /* Get the salt */
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(entered, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            /* Compare the results */
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    return false;

            return true;
        }
    }
}