using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace API.Model
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        // Foreign Key
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Display(Name = "List Order_Porduct")]
        public virtual ICollection<Order_Product> Order_Products { get; set; }

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            var e = modelBuilder.Entity<Order>();

            e.ToTable("t_order");

            e.HasKey(x => x.Id);

            e.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired(true);

            e.Property(x => x.Date)
                .HasColumnName("dateTime");

            e.Property(x => x.UserId)
                .HasColumnName("user_id")
                .IsRequired(true);

            // Foreign Key
            e.HasOne(x => x.User)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.UserId);
        }
    }
}