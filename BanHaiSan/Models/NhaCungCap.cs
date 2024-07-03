using System;
using System.Collections.Generic;

namespace BanHaiSan.Models;

public partial class NhaCungCap
{
    public int MaNcc { get; set; }

    public string TenNcc { get; set; } = null!;

    public string? DiaChi { get; set; }

    public string? Sdt { get; set; }

    public virtual ICollection<HaiSan> HaiSans { get; set; } = new List<HaiSan>();
}
