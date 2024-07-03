using System;
using System.Collections.Generic;

namespace BanHaiSan.Models;

public partial class DonHang
{
    public int MaDonHang { get; set; }

    public DateOnly NgayDat { get; set; }

    public double? TongTien { get; set; }

    public string? GhiChu { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();
}
