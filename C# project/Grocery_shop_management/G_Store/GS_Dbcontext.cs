using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_Store
{
    public class GS_Dbcontext: DbContext
    {
        private readonly string _connectionstring;
        public GS_Dbcontext()
        {
            _connectionstring = "Server=DESKTOP-V5UJJLP\\SQLEXPRESS;Database=Grocery_S;User Id=riyad_C#;Password=riyad102;TrustServerCertificate=True";
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionstring);
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Inventory>()
                .Property(i => i.selling_price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Supplie>()
            .HasKey(s => new { s.pro_id, s.sup_id });

            // base.OnModelCreating(modelBuilder);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Inventory> Inventorys { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Supplie> Supplies { get; set; }
        
    }
}
