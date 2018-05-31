using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API.Model;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();

            var context = new ExampleContext();

            // Adds User
            var user = new User
            {
                Name = "Olivier",
                Password = "Olivier",
                Admin = false
            };
            context.Users.Add(user);

            // Adds some orders to User
            var order1 = new Order
            {
                Date = DateTime.Now,
                User = user
            };
            context.Orders.Add(order1);
            //var order2 = new Order
            //{
            //    Date = DateTime.Now,
            //    Title = "Commande 2",
            //    User = user
            //};
            //context.Orders.Add(order2);

            //// Adds some product
            //var prod1 = new Product
            //{
            //    Name = "Topuillette",
            //    Price = 3.20f,
            //    UrlImg = "uu"
            //};
            //context.Products.Add(prod1);
            //var prod2 = new Product
            //{
            //    Name = "Calumet",
            //    Price = 8.20f,
            //    UrlImg = "u"
            //};
            //context.Products.Add(prod2);

            //// Adds some ref orderProduct between Order / Product
            //context.Order_Products.Add(new Order_Product
            //{
            //    Order = order1,
            //    Product = prod1
            //});
            //context.Order_Products.Add(new Order_Product
            //{
            //    Order = order1,
            //    Product = prod2
            //});
            //context.Order_Products.Add(new Order_Product
            //{
            //    Order = order2,
            //    Product = prod2
            //});


            //// Adds some categories
            //var cat1 = new Categorie
            //{
            //    Name = "Vegetal",
            //};
            //context.Categories.Add(cat1);
            //var cat2 = new Categorie
            //{
            //    Name = "Animal",
            //};
            //context.Categories.Add(cat2);

            //// Adds some ref orderProduct between Order / Product
            //context.Product_Categories.Add(new Product_Categorie
            //{
            //    Categorie = cat1,
            //    Product = prod1
            //});
            //context.Product_Categories.Add(new Product_Categorie
            //{
            //    Categorie = cat2,
            //    Product = prod1
            //});
            //context.Product_Categories.Add(new Product_Categorie
            //{
            //    Categorie = cat1,
            //    Product = prod2
            //});

            // Saves changes
            context.SaveChanges();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
