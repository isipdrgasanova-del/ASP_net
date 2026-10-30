using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace WebApplication13.Models;

public partial class AspNetPracticeContext : DbContext
{
    public AspNetPracticeContext() { }

    public AspNetPracticeContext(DbContextOptions<AspNetPracticeContext> options) : base(options) { }

    public virtual DbSet<Cart> Carts { get; set; }
    public virtual DbSet<CartItem> CartItems { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderItem> OrderItems { get; set; }
    public virtual DbSet<Position> Positions { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.IdCart).HasName("PK__carts__70179490FD44559B");
            entity.ToTable("carts");
            entity.Property(e => e.IdCart).HasColumnName("ID_cart");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())").HasColumnName("created_at");
            entity.Property(e => e.IdUser).HasColumnName("ID_user");
            entity.HasOne<User>().WithMany(p => p.Carts).HasForeignKey(d => d.IdUser).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__carts__ID_user");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.IdCartItem).HasName("PK__cart_ite__2D213196EB3C5401");
            entity.ToTable("cart_items");
            entity.HasIndex(e => new { e.IdCart, e.IdProduct }, "UQ__cart_ite__5FC06A537666F25A").IsUnique();
            entity.Property(e => e.IdCartItem).HasColumnName("ID_cart_item");
            entity.Property(e => e.IdCart).HasColumnName("ID_cart");
            entity.Property(e => e.IdProduct).HasColumnName("ID_product");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.CartItems).HasForeignKey(d => d.IdProduct).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__cart_item__ID_pr__4BAC3F29");
            entity.HasOne(d => d.IdCartNavigation).WithMany(p => p.CartItems).HasForeignKey(d => d.IdCart).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__cart_item__ID_ca");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("PK__categori__AC5CA40EEAB98C88");
            entity.ToTable("categories");
            entity.HasIndex(e => e.CategoryName, "UQ__categori__5189E255E9BB3E09").IsUnique();
            entity.Property(e => e.IdCategory).HasColumnName("ID_category");
            entity.Property(e => e.CategoryName).HasMaxLength(100).IsUnicode(false).HasColumnName("category_name");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("PK__orders__89BFCD59AD2C9B64");
            entity.ToTable("orders");
            entity.Property(e => e.IdOrder).HasColumnName("ID_order");
            entity.Property(e => e.IdUser).HasColumnName("ID_user");
            entity.Property(e => e.OrderDate).HasDefaultValueSql("(getdate())").HasColumnName("order_date");
            entity.Property(e => e.Status).HasMaxLength(50).IsUnicode(false).HasDefaultValue("Новый").HasColumnName("status");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)").HasColumnName("total_amount");
            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Orders).HasForeignKey(d => d.IdUser).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__orders__ID_user__4F7CD00D");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.IdOrderItem).HasName("PK__order_it__5B1127FF7DB4849A");
            entity.ToTable("order_items");
            entity.Property(e => e.IdOrderItem).HasColumnName("ID_order_item");
            entity.Property(e => e.IdOrder).HasColumnName("ID_order");
            entity.Property(e => e.IdProduct).HasColumnName("ID_product");
            entity.Property(e => e.PriceAtOrder).HasColumnType("decimal(18, 2)").HasColumnName("price_at_order");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.OrderItems).HasForeignKey(d => d.IdOrder).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__order_ite__ID_or__5535A963");
            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.OrderItems).HasForeignKey(d => d.IdProduct).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__order_ite__ID_pr__5629CD9C");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.IdPosition).HasName("PK__position__91FB1AE043189543");
            entity.ToTable("positions");
            entity.Property(e => e.IdPosition).HasColumnName("ID_position");
            entity.Property(e => e.PositionName).HasMaxLength(50).IsUnicode(false).HasColumnName("position_name");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct).HasName("PK__products__FD7FEC23AB0E4558");
            entity.ToTable("products");
            entity.Property(e => e.IdProduct).HasColumnName("ID_product");
            entity.Property(e => e.IdCategory).HasColumnName("ID_category");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)").HasColumnName("price");
            entity.Property(e => e.ProductName).HasMaxLength(200).IsUnicode(false).HasColumnName("product_name");

            // ВОТ ЗДЕСЬ МЫ ЖЕСТКО ПРИВЯЗЫВАЕМ IMAGEURL К IMAGE_URL
            entity.Property(e => e.ImageUrl).HasMaxLength(500).IsUnicode(false).HasColumnName("image_url");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Products).HasForeignKey(d => d.IdCategory).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__products__ID_cat__4222D4EF");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__users__D7B4671EA38A7900");
            entity.ToTable("users");
            entity.HasIndex(e => e.Login, "UQ__users__7838F2721F13E9D8").IsUnique();
            entity.Property(e => e.IdUser).HasColumnName("ID_user");
            entity.Property(e => e.IdPosition).HasColumnName("ID_position");
            entity.Property(e => e.Login).HasMaxLength(50).IsUnicode(false).HasColumnName("login");
            entity.Property(e => e.MiddleName).HasMaxLength(25).IsUnicode(false).HasColumnName("middle_name");
            entity.Property(e => e.Name).HasMaxLength(25).IsUnicode(false).HasColumnName("name");
            entity.Property(e => e.Password).HasMaxLength(256).IsUnicode(false).HasColumnName("password");
            entity.Property(e => e.RegistrationDate).HasDefaultValueSql("(getdate())").HasColumnName("registration_date");
            entity.Property(e => e.Surname).HasMaxLength(25).IsUnicode(false).HasColumnName("surname");
            entity.HasOne(d => d.IdPositionNavigation).WithMany(p => p.Users).HasForeignKey(d => d.IdPosition).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__users__ID_positi__3A81B327");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}