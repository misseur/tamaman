using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace API.Model
{
    public class Product
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public float Price { get; set; }

        public string UrlImg { get; set; }

        [Display(Name = "List Order_Porduct")]
        public virtual ICollection<Order_Product> Order_Products { get; set; }

        [Display(Name = "List Product_Categorie")]
        public virtual ICollection<Product_Categorie> Product_Categories { get; set; }

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            var e = modelBuilder.Entity<Product>();

            e.ToTable("product");

            e.HasKey(x => x.Id);

            e.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired(true);

            e.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(30)
                .IsRequired(true);

            e.Property(x => x.Price)
                .HasColumnName("price")
                .IsRequired(true);
            //                .HasMaxLength(30);

            e.Property(x => x.UrlImg)
                .HasColumnName("urlImg")
                .HasMaxLength(200);

         
        }
    }
}
