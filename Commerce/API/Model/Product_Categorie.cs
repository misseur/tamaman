using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace API.Model
{
    public class Product_Categorie
    {
        public int Id { get; set; }

        public int CategorieId { get; set; }
        public virtual Categorie Categorie { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            var e = modelBuilder.Entity<Product_Categorie>();

            e.ToTable("productCategorie");

            e.HasKey(x => x.Id);

            e.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired(true);

            e.Property(x => x.CategorieId)
                .HasColumnName("categorie_id")
                .IsRequired(true);

            // Foreign Key
            e.HasOne(x => x.Categorie)
                .WithMany(x => x.Product_Categories)
                .HasForeignKey(x => x.CategorieId);

            e.Property(x => x.ProductId)
            .HasColumnName("product_id")
            .IsRequired(true);

            // Foreign Key
            e.HasOne(x => x.Product)
                .WithMany(x => x.Product_Categories)
                .HasForeignKey(x => x.ProductId);
        }
    }
}
