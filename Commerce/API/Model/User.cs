using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace API.Model
{
    public class User
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Admin")]
        public bool Admin { get; set; }

        // User 1 : N Order
        [Display(Name = "List Order")]
        public virtual ICollection<Order> Orders { get; set; }

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            var e = modelBuilder.Entity<User>();

            e.ToTable("user");

            e.HasKey(x => x.Id);

            // Primary key
            e.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired(true);

            e.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(30)
                .IsRequired(true);

            e.Property(x => x.Admin)
             .HasColumnName("admin")
             .IsRequired(true);

            e.Property(x => x.Password)
             .HasColumnName("password")
             .HasMaxLength(30)
             .IsRequired(true);
        }
    }
}
