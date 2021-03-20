using System;
using Microsoft.EntityFrameworkCore;
using PizzaBox.Domain.Abstracts;


namespace PizzaBox.Domain.Models
{
    //todo: change protection level
    internal class DbContextClass : DbContext
    {

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<APizza> Pizzas { get; set; }
        public DbSet<AStore> Stores { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Topping> Toppings { get; set; }
        public DbSet<Crust> Crusts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\PRODDB;Database=PizzaBoxP0NNK;Trusted_Connection=True;");
            // optionsBuilder.UseSqlServer(@"Server=127.0.0.1;Database=PizzaBoxP0NNK;User Id=SA;Password=1Secure*Password1;");
        }
    }
}