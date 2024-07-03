using System.ComponentModel.DataAnnotations;

namespace BanHaiSan.ViewModel
{
    public class UsersVM
    {
        [Required(ErrorMessage = "Vui lòng nhập Tên Đăng Nhập")]
        [Display(Name = "Tên Đăng Nhập")]
        public string? tentaikhoan { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Mật Khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật Khẩu")]
        public string? matkhau { get; set; }
    }
}
