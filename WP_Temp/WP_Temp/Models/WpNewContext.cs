using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WP_Temp.Models;

public partial class WpNewContext : DbContext
{
    public WpNewContext()
    {
    }

    public WpNewContext(DbContextOptions<WpNewContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<OrderHistory> OrderHistories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5433;Database=WP_New;User Id=postgres;Password=PSQL?kut0v;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("images_pkey");

            entity.ToTable("images");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(500)
                .HasColumnName("image_url");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Product).WithMany(p => p.Images)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("fk_products");
        });

        modelBuilder.Entity<OrderHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("order_history_pkey");

            entity.ToTable("order_history");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.SiparisAdedi).HasColumnName("siparis_adedi");
            entity.Property(e => e.Tarih).HasColumnName("tarih");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderHistories)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("fk_product_id");

            entity.HasOne(d => d.User).WithMany(p => p.OrderHistories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_user_id");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("products_pkey");

            entity.ToTable("products");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Aciklama)
                .HasMaxLength(4096)
                .HasColumnName("aciklama");
            entity.Property(e => e.Baslik)
                .HasMaxLength(255)
                .HasColumnName("baslik");
            entity.Property(e => e.BegeniSayisi)
                .HasMaxLength(255)
                .HasColumnName("begeni_sayisi");
            entity.Property(e => e.Degerlendirme)
                .HasMaxLength(255)
                .HasColumnName("degerlendirme");
            entity.Property(e => e.Detaylar)
                .HasColumnType("json")
                .HasColumnName("detaylar");
            entity.Property(e => e.Fiyat)
                .HasMaxLength(255)
                .HasColumnName("fiyat");
            entity.Property(e => e.Kategori)
                .HasMaxLength(255)
                .HasColumnName("kategori");
            entity.Property(e => e.Marka)
                .HasMaxLength(255)
                .HasColumnName("marka");
            entity.Property(e => e.Satıcı)
                .HasMaxLength(255)
                .HasColumnName("satıcı");
        });

        modelBuilder.Entity<ShoppingCart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("shopping_cart_pkey");

            entity.ToTable("shopping_cart");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Product).WithMany(p => p.ShoppingCarts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("fk_product_id");

            entity.HasOne(d => d.User).WithMany(p => p.ShoppingCarts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_user_id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EMail)
                .HasMaxLength(50)
                .HasColumnName("e_mail");
            entity.Property(e => e.GoogleId)
                .HasMaxLength(50)
                .HasColumnName("google_id");
            entity.Property(e => e.NameSurname)
                .HasMaxLength(50)
                .HasColumnName("name_surname");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
