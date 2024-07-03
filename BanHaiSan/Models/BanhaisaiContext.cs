using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BanHaiSan.Models;

public partial class BanhaisaiContext : DbContext
{
    public BanhaisaiContext()
    {
    }

    public BanhaisaiContext(DbContextOptions<BanhaisaiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }

    public virtual DbSet<DonHang> DonHangs { get; set; }

    public virtual DbSet<HaiSan> HaiSans { get; set; }

    public virtual DbSet<LoaiHaiSan> LoaiHaiSans { get; set; }

    public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=HOANGTUAN;Initial Catalog=BANHAISAI;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False;Trusted_Connection=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietDonHang>(entity =>
        {
            entity.HasKey(e => e.MaChiTiet).HasName("PK__Chi_Tiet__7FDE2B4772488D25");

            entity.ToTable("Chi_Tiet_Don_Hang");

            entity.Property(e => e.MaChiTiet).HasColumnName("Ma_Chi_Tiet");
            entity.Property(e => e.DonGia).HasColumnName("Don_Gia");
            entity.Property(e => e.MaDonHang).HasColumnName("Ma_Don_Hang");
            entity.Property(e => e.MaHaiSan).HasColumnName("Ma_Hai_San");
            entity.Property(e => e.SoLuong).HasColumnName("So_Luong");
            entity.Property(e => e.ThanhTien).HasColumnName("Thanh_Tien");

            entity.HasOne(d => d.MaDonHangNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaDonHang)
                .HasConstraintName("fk_ma_don_hang");

            entity.HasOne(d => d.MaHaiSanNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaHaiSan)
                .HasConstraintName("fk_ma_hai_san");
        });

        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.HasKey(e => e.MaDonHang).HasName("PK__Don_Hang__FD0F413D1098F2C3");

            entity.ToTable("Don_Hang");

            entity.Property(e => e.MaDonHang).HasColumnName("Ma_Don_Hang");
            entity.Property(e => e.GhiChu)
                .HasColumnType("text")
                .HasColumnName("Ghi_Chu");
            entity.Property(e => e.NgayDat).HasColumnName("Ngay_Dat");
            entity.Property(e => e.TongTien).HasColumnName("Tong_Tien");
        });

        modelBuilder.Entity<HaiSan>(entity =>
        {
            entity.HasKey(e => e.MaHaiSan).HasName("PK__Hai_San__7600AA0A747199F6");

            entity.ToTable("Hai_San");

            entity.Property(e => e.MaHaiSan).HasColumnName("Ma_Hai_San");
            entity.Property(e => e.Img).HasColumnType("text");
            entity.Property(e => e.MaLoai).HasColumnName("Ma_Loai");
            entity.Property(e => e.MaNcc).HasColumnName("Ma_NCC");
            entity.Property(e => e.MoTa)
                .HasMaxLength(50)
                .HasColumnName("Mo_Ta");
            entity.Property(e => e.TenHaiSan)
                .HasMaxLength(100)
                //.IsUnicode(false) // chỗ này nè - xóa đi => để false thì nó k nhận dấu tiếng việt
                .HasColumnName("Ten_Hai_San");

            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.HaiSans)
                .HasForeignKey(d => d.MaLoai)
                .HasConstraintName("fk_ma_loai");

            entity.HasOne(d => d.MaNccNavigation).WithMany(p => p.HaiSans)
                .HasForeignKey(d => d.MaNcc)
                .HasConstraintName("fk_ma_ncc");
        });

        modelBuilder.Entity<LoaiHaiSan>(entity =>
        {
            entity.HasKey(e => e.MaLoai).HasName("PK__Loai_Hai__586312F9D930A7E9");

            entity.ToTable("Loai_Hai_San");

            entity.Property(e => e.MaLoai).HasColumnName("Ma_Loai");
            entity.Property(e => e.TenLoai)
                .HasMaxLength(50)
                //.IsUnicode(false)
                .HasColumnName("Ten_Loai");
        });

        modelBuilder.Entity<NhaCungCap>(entity =>
        {
            entity.HasKey(e => e.MaNcc).HasName("PK__Nha_Cung__C23A87C7D2DE5EA0");

            entity.ToTable("Nha_Cung_Cap");

            entity.Property(e => e.MaNcc).HasColumnName("Ma_NCC");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(200)
                //.IsUnicode(false)
                .HasColumnName("Dia_Chi");
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                //.IsUnicode(false)
                .HasColumnName("SDT");
            entity.Property(e => e.TenNcc)
                .HasMaxLength(100)
                //.IsUnicode(false)
                .HasColumnName("Ten_NCC");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.tentaikhoan).HasName("PK__TaiKhoan__A38B885716C37A89");

            entity.ToTable("TaiKhoan");

            entity.Property(e => e.email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.matkhau)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("matkhau");
            entity.Property(e => e.sdt).HasColumnName("sdt");
            entity.Property(e => e.tentaikhoan)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tentaikhoan");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
