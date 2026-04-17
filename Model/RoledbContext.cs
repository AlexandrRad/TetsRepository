using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace WebAPI_React.Model;

public partial class RoledbContext : DbContext
{
    public RoledbContext()
    {
    }

    public RoledbContext(DbContextOptions<RoledbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<RolesTable> RolesTables { get; set; }

    public virtual DbSet<Shop> Shops { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=124536;database=roledb", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.45-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrders).HasName("PRIMARY");

            entity.ToTable("orders");

            entity.HasIndex(e => new { e.ProductId, e.UserId }, "product_key_idx");

            entity.HasIndex(e => e.UserId, "user_key_idx");

            entity.Property(e => e.IdOrders).HasColumnName("idOrders");
            entity.Property(e => e.ProductId).HasColumnName("Product_id");
            entity.Property(e => e.UserId).HasColumnName("User_id");

            entity.HasOne(d => d.Product).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_key");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_key");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct).HasName("PRIMARY");

            entity.ToTable("product");

            entity.Property(e => e.IdProduct).HasColumnName("idProduct");
            entity.Property(e => e.Category).HasMaxLength(45);
            entity.Property(e => e.Description).HasMaxLength(45);
            entity.Property(e => e.Image).HasMaxLength(100);
            entity.Property(e => e.ProductName).HasMaxLength(45);
        });

        modelBuilder.Entity<RolesTable>(entity =>
        {
            entity.HasKey(e => e.IdRoles).HasName("PRIMARY");

            entity.ToTable("roles_table");

            entity.Property(e => e.IdRoles).HasColumnName("idRoles");
            entity.Property(e => e.RoleName).HasMaxLength(45);
        });

        modelBuilder.Entity<Shop>(entity =>
        {
            entity.HasKey(e => e.IdShop).HasName("PRIMARY");

            entity.ToTable("shop");

            entity.HasIndex(e => e.IdProduct, "category_key_idx");

            entity.HasIndex(e => e.IdUser, "key_user_idx");

            entity.Property(e => e.IdShop).HasColumnName("idShop");
            entity.Property(e => e.IdProduct).HasColumnName("idProduct");
            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.Price).HasPrecision(10, 2);

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.Shops)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("category_key");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Shops)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("key_user");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUsers).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.RoleId, "role_key_idx");

            entity.Property(e => e.IdUsers).HasColumnName("idUsers");
            entity.Property(e => e.Login).HasMaxLength(45);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.StatusConfirmed).HasColumnName("status_confirmed");
            entity.Property(e => e.UserName).HasMaxLength(45);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("role_key");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
