using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace API.Model
{
    public class Categorie
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "List Product_Categorie")]
        public virtual ICollection<Product_Categorie> Product_Categories { get; set; }

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            var e = modelBuilder.Entity<Categorie>();

            e.ToTable("categorie");

            e.HasKey(x => x.Id);

            e.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired(true);

            e.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(30);

        }
    }
}
