using System;
using System.Collections.Generic;

namespace BanHaiSan.Models;

public partial class ChiTietDonHang
{
    public int MaChiTiet { get; set; }

    public int? MaDonHang { get; set; }

    public int? MaHaiSan { get; set; }

    public int? SoLuong { get; set; }

    public double? DonGia { get; set; }

    public double? ThanhTien { get; set; }

    public virtual DonHang? MaDonHangNavigation { get; set; }

    public virtual HaiSan? MaHaiSanNavigation { get; set; }
}
