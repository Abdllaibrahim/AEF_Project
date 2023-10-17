using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace AEF_Project
{
    public partial class EModel : DbContext
    {
        public EModel()
            : base("name=EModel")
        {
        }

        public virtual DbSet<customer> customers { get; set; }
        public virtual DbSet<Customer_item_store> Customer_item_store { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<supplier> suppliers { get; set; }
        public virtual DbSet<supplier_item_store> supplier_item_store { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<customer>()
                .Property(e => e.name_customer)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.e_mail)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .HasMany(e => e.Customer_item_store)
                .WithRequired(e => e.customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer_item_store>()
                .Property(e => e.store_name)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_item_store>()
                .Property(e => e.Item_unit)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_item_store>()
                .Property(e => e.item_name)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_item_store>()
                .Property(e => e.customer_name)
                .IsUnicode(false);

            modelBuilder.Entity<Item>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Item>()
                .Property(e => e.unit)
                .IsUnicode(false);

            modelBuilder.Entity<Item>()
                .Property(e => e.name_store)
                .IsUnicode(false);

            modelBuilder.Entity<Item>()
                .HasMany(e => e.Customer_item_store)
                .WithRequired(e => e.Item)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Item>()
                .HasMany(e => e.supplier_item_store)
                .WithRequired(e => e.Item)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Item>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.Item)
                .HasForeignKey(e => e.Item_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Store>()
                .Property(e => e.store_name)
                .IsUnicode(false);

            modelBuilder.Entity<Store>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<Store>()
                .Property(e => e.manger_name)
                .IsUnicode(false);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.Customer_item_store)
                .WithRequired(e => e.Store)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.Items)
                .WithOptional(e => e.Store)
                .HasForeignKey(e => e.name_store);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.supplier_item_store)
                .WithRequired(e => e.Store)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.Store)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<supplier>()
                .Property(e => e.name_sup)
                .IsUnicode(false);

            modelBuilder.Entity<supplier>()
                .Property(e => e.e_mail)
                .IsUnicode(false);

            modelBuilder.Entity<supplier>()
                .HasMany(e => e.supplier_item_store)
                .WithRequired(e => e.supplier)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<supplier>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.supplier)
                .HasForeignKey(e => e.Supplier_tel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<supplier_item_store>()
                .Property(e => e.store_name)
                .IsUnicode(false);

            modelBuilder.Entity<supplier_item_store>()
                .Property(e => e.Item_unit)
                .IsUnicode(false);

            modelBuilder.Entity<supplier_item_store>()
                .Property(e => e.item_name)
                .IsUnicode(false);

            modelBuilder.Entity<supplier_item_store>()
                .Property(e => e.supplier_name)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.Store_name)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.Store_Filled)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.Item_name)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.Supplier_name)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.Unit)
                .IsUnicode(false);
        }
    }
}
