using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MoShop.Models
{
    public partial class MoShopContext : DbContext
    {
        public MoShopContext()
        {
        }

        public MoShopContext(DbContextOptions<MoShopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Chitiet> Chitiets { get; set; } = null!;
        public virtual DbSet<ThongKe> ThongKes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=MoShop;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chitiet>(entity =>
            {
                entity.HasKey(e => e.Ma)
                    .HasName("PK__Chitiet__3214CC9FAEFF5F46");

                entity.ToTable("Chitiet");

                entity.Property(e => e.GiaTriTien).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NgayGioNhap)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<ThongKe>(entity =>
            {
                entity.HasKey(e => e.MaTk)
                    .HasName("PK__ThongKe__27250070B3BA1BA2");

                entity.ToTable("ThongKe");

                entity.Property(e => e.MaTk).HasColumnName("MaTK");

                entity.Property(e => e.TongChi).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TongThu).HasColumnType("decimal(18, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
