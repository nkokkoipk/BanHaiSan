using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BanHaiSan.ViewModel
{
    public class EditCate
    {

        public int? MaLoai { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập Tên Loại")]
        public string TenLoai { get; set; }
    }
}
