using API.Model;
using MySql.Data.EntityFrameworkCore.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace API.Model
{
    public class ExampleContext : DbContext
    {
        public ExampleContext()
        { }

        public ExampleContext(DbContextOptions<ExampleContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(@"server=localhost;userid=root;port=3306;database=commerce;sslmode=none;");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order_Product> Order_Products { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Product_Categorie> Product_Categories { get; set; }
        public DbSet<Categorie> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            User.OnModelCreating(modelBuilder);
            Order.OnModelCreating(modelBuilder);
            Order_Product.OnModelCreating(modelBuilder);
            Product.OnModelCreating(modelBuilder);
            Product_Categorie.OnModelCreating(modelBuilder);
            Categorie.OnModelCreating(modelBuilder);
        }
    }
}
