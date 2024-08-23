using FastFood.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FastFood.Repositories
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }  // Changed to plural
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }  // Corrected to singular
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }  // Changed to plural

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cart relationship
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.ApplicationUser)
                .WithMany(u => u.Carts)
                .HasForeignKey(c => c.ApplicationUserId);

            // OrderHeader relationship
            modelBuilder.Entity<OrderHeader>()
                .HasOne(o => o.ApplicationUser)
                .WithMany(u => u.OrderHeaders)
                .HasForeignKey(o => o.ApplicationUserId);

            // Item relationship with Category
            modelBuilder.Entity<Item>()
                .HasOne(i => i.Category)
                .WithMany(c => c.Items)
                .HasForeignKey(i => i.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);  // Cascade deletes on Category

            // Item relationship with SubCategory
            modelBuilder.Entity<Item>()
                .HasOne(i => i.SubCategory)
                .WithMany(sc => sc.Items)
                .HasForeignKey(i => i.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);  // Restrict deletes on SubCategory
        }
    }
}

