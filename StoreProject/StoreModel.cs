using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace StoreProject
{
    public partial class StoreModel : DbContext
    {
        public StoreModel()
            : base("name=StoreModel")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemStore> ItemStores { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<StockIn> StockIns { get; set; }
        public virtual DbSet<StockOut> StockOuts { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(e => e.customer_name)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.customer_phone)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.customer_fax)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.customer_mobile)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.customer_email)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.customer_website)
                .IsUnicode(false);

            modelBuilder.Entity<Item>()
                .HasMany(e => e.ItemStores)
                .WithOptional(e => e.Item)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Store>()
                .Property(e => e.store_manager)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.supplier_name)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.supplier_phone)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.supplier_fax)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.supplier_mobile)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.supplier_email)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.supplier_website)
                .IsUnicode(false);
        }
    }
}
