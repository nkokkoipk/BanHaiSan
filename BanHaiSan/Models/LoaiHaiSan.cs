using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BanHaiSan.Models;

public partial class LoaiHaiSan
{
    public int MaLoai { get; set; }


    [Required(ErrorMessage = "Vui lòng nhập")]
    public string TenLoai { get; set; } = null!;

    public virtual ICollection<HaiSan> HaiSans { get; set; } = new List<HaiSan>();
}
