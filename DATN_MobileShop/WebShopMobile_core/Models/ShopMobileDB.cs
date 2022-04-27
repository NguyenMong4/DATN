using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebShopMobile_core.Models
{
    public partial class ShopMobileDB : DbContext
    {
        public ShopMobileDB()
            : base("name=ShopMobileDB")
        {
        }

        public virtual DbSet<Advantisment> Advantisment { get; set; }
        public virtual DbSet<Blog> Blog { get; set; }
        public virtual DbSet<Cart_Details> Cart_Details { get; set; }
        public virtual DbSet<Carts> Carts { get; set; }
        public virtual DbSet<color> color { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<GroupAdmin> GroupAdmins { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Product_category> Product_category { get; set; }
        public virtual DbSet<Product_detail> Product_detail { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Voucher> Voucher { get; set; }
        public virtual DbSet<Wishlist> Wishlist { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Advantisment>()
                .Property(e => e.photo)
                .IsUnicode(false);

            modelBuilder.Entity<Blog>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<Blog>()
                .Property(e => e.contents)
                .IsUnicode(false);

            modelBuilder.Entity<Cart_Details>()
                .Property(e => e.price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Carts>()
                .Property(e => e.total)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Carts>()
                .HasMany(e => e.Cart_Details)
                .WithOptional(e => e.Carts)
                .HasForeignKey(e => e.id_cart);

            modelBuilder.Entity<color>()
                .HasMany(e => e.Product_detail)
                .WithOptional(e => e.color)
                .HasForeignKey(e => e.id_color);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Carts)
                .WithOptional(e => e.Customer)
                .HasForeignKey(e => e.id_custommer);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Carts1)
                .WithOptional(e => e.Customer1)
                .HasForeignKey(e => e.id_custommer);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Wishlist)
                .WithOptional(e => e.Customer)
                .HasForeignKey(e => e.id_custommer);

            modelBuilder.Entity<Product>()
                .Property(e => e.price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Advantisment)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.id_product);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Cart_Details)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.id_product);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Product_detail)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.id_product);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Wishlist)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.id_product);

            modelBuilder.Entity<Product_category>()
                .HasMany(e => e.Product)
                .WithOptional(e => e.Product_category)
                .HasForeignKey(e => e.id_category);

            modelBuilder.Entity<Product_detail>()
                .Property(e => e.purchase_price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product_detail>()
                .Property(e => e.price_endow)
                .HasPrecision(19, 4);
            
            modelBuilder.Entity<Voucher>()
                .HasMany(e => e.Carts)
                .WithOptional(e => e.Voucher)
                .HasForeignKey(e => e.id_voucher);
        }      
    }
}
