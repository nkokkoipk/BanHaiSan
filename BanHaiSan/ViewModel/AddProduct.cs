using System.ComponentModel.DataAnnotations;

namespace BanHaiSan.ViewModel
{
    public class AddProduct
    {
        public int? MaHaiSan { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập")]
        [MaxLength(30, ErrorMessage = "Vui lòng nhập dưới 30 ký tự")]
        public string TenHaiSan { get; set; } = null!;

        public int? MaLoai { get; set; }
        public string? tenLoai { get; set; }
        public double? Gia { get; set; }

        public string? MoTa { get; set; }
        public string? TenNCC { get; set; }

        public int? MaNcc { get; set; }
        public string? CanNangTB { get; set; }
        public string? CanNangMin { get; set; }
        public string? ChatLuong { get; set; }
        public string? Review { get; set; }
        public string? Nguon { get; set; }
    }
}
