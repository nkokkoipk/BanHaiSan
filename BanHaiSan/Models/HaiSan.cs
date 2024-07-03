using System;
using System.Collections.Generic;

namespace BanHaiSan.Models;

public partial class HaiSan
{
    public int MaHaiSan { get; set; }

    public string? TenHaiSan { get; set; } = null!;

    public int? MaLoai { get; set; }

    public double? Gia { get; set; }

    public string? MoTa { get; set; }

    public int? MaNcc { get; set; }
    public string? Img { get; set; }
    public string? CanNangTB { get; set; }
    public string? CanNangMin { get; set; }
    public string? ChatLuong { get; set; }
    public string? Review { get; set; }
    public string? Nguon { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual LoaiHaiSan? MaLoaiNavigation { get; set; }

    public virtual NhaCungCap? MaNccNavigation { get; set; }

}
