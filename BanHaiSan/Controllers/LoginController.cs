using Microsoft.AspNetCore.Mvc;
using BanHaiSan.Models;
using BanHaiSan.ViewModel;

namespace BanHaiSan.Controllers
{
    public class LoginController : Controller
    {
        BanhaisaiContext db = new BanhaisaiContext();
        public IActionResult Index()
        {
            return PartialView();
        }
        public IActionResult CreateUser()
        {
            return PartialView();
        }
        public IActionResult LoginFailed()
        {
            return PartialView();
        }
        public IActionResult Authenticate(UsersVM us)
        {
            var user = db.TaiKhoans.FirstOrDefault(u => u.tentaikhoan == us.tentaikhoan && u.matkhau == us.matkhau);
            if (user != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Đăng nhập thất bại, có thể hiển thị thông báo lỗi cho người dùng
                ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không đúng");
                return PartialView("LoginFailed", us); // Trả về lại view đăng nhập với model và hiển thị thông báo lỗi
            }
        }
    }
}
