using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace API.Model
{
    public class Order_Product
    {
        public int Id { get; set; }
        
        // Order 1 : N Order_Product
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        // Order 1 : N Order_Product
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            var e = modelBuilder.Entity<Order_Product>();

            e.ToTable("orderProduct");

            e.HasKey(x => x.Id);

            e.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired(true);
            
            e.Property(x => x.OrderId)
                .HasColumnName("order_id")
                .IsRequired(true);

            // Foreign Key
            e.HasOne(x => x.Order)
                .WithMany(x => x.Order_Products)
                .HasForeignKey(x => x.OrderId);

            e.Property(x => x.ProductId)
            .HasColumnName("product_id")
            .IsRequired(true);

            // Foreign Key
            e.HasOne(x => x.Product)
                .WithMany(x => x.Order_Products)
                .HasForeignKey(x => x.ProductId);
        }
    }
}
