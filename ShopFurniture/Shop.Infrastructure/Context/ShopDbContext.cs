using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Context
{
    public class ShopDbContext:DbContext
    {
        public ShopDbContext()
        {

        }
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) 
        {

        }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>(e =>
            {
                e.ToTable("Category");
                e.HasKey(e => e.Id);
                e.Property(e => e.Id).IsRequired();
                e.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar");
            });
            modelBuilder.Entity<Product>(e =>
            {
                e.ToTable("Product");
                e.HasKey(e => e.Id);
                e.Property(e => e.Id).IsRequired();
                e.Property(e => e.CategoryId)
                    .IsRequired()
                    .HasColumnType("int");
                e.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar");
                e.Property(e => e.Price)
                    .IsRequired()
                    .HasColumnType("float");
                e.Property(e => e.Size)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar");
                e.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar");
                e.Property(e => e.Material)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar");
            });
            modelBuilder.Entity<ProductImage>(e =>
            {
                e.ToTable("ProductImage");
                e.HasKey(e => e.Id);
                e.Property(e => e.Id).IsRequired();
                e.Property(e => e.ProductId)
                    .IsRequired()
                    .HasColumnType("int");
                e.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnType("nvarchar");
            });
            modelBuilder.Entity<User>(e =>
            {
                e.ToTable("User");
                e.HasKey(e => e.Id);
                e.Property(e => e.Id).IsRequired();
                e.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar");
                e.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnType("nvarchar");
                e.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar");
                e.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar");
                e.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar");
                e.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar");
                e.Property(e => e.IsAdmin)
                    .IsRequired()
                    .HasColumnType("int");
                e.Property(e => e.IsCustomer)
                    .IsRequired()
                    .HasColumnType("int");
            });
            modelBuilder.Entity<Order>(e =>
            {
                e.ToTable("Order");
                e.HasKey(e => e.Id);
                e.Property(e => e.Id).IsRequired();
                e.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnType("int");
                e.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar");
                e.Property(e => e.DateOrder)
                    .IsRequired()
                    .HasColumnType("date");
                e.Property(e => e.CustomerPhone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar");
                e.Property(e => e.CustomerEmail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar");
                e.Property(e => e.CustomerAddress)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar");
                e.Property(e => e.TotalOrder)
                    .IsRequired()
                    .HasColumnType("float");
                e.Property(e => e.Status)
                   .IsRequired()
                   .HasColumnType("int");
            });
            modelBuilder.Entity<OrderDetail>(e =>
            {
                e.ToTable("OrderDetail");
                e.HasKey(e => e.Id);
                e.Property(e => e.Id).IsRequired();
                e.Property(e => e.ProductId)
                    .IsRequired()
                    .HasColumnType("int");
                e.Property(e => e.OrderId)
                    .IsRequired()
                    .HasColumnType("int");
                e.Property(e => e.Quantity)
                    .IsRequired()
                    .HasColumnType("int");
            });
        }
    }
}
