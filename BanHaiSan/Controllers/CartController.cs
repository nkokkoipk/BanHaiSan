using BanHaiSan.Models;
using BanHaiSan.ViewModel;
using Microsoft.AspNetCore.Mvc;
using BanHaiSan.Helpers;
using System.Xml;

namespace BanHaiSan.Controllers
{
    public class CartController : Controller
    {

        public readonly BanhaisaiContext db;
        public CartController(BanhaisaiContext context)
        {
            db = context;
        }
        public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();
        public IActionResult Index()
        {
            return View(Cart);
        }
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.MaHh == id);
            if (item == null)
            {
                var hangHoa = db.HaiSans.SingleOrDefault(p => p.MaHaiSan == id);
                if (hangHoa == null)
                {
                    TempData["Message"] = $"Không tìm thấy hàng hóa có mã {id}";
                    return Redirect("/404");
                }
                item = new CartItem
                {
                    MaHh = hangHoa.MaHaiSan,
                    TenHH = hangHoa.TenHaiSan,
                    DonGia = hangHoa.Gia ?? 0,
                    Hinh = hangHoa.Img ?? string.Empty,
                    SoLuong = quantity
                };
                gioHang.Add(item);
            }
            else
            {
                item.SoLuong += quantity;
            }

            HttpContext.Session.Set(MySetting.CART_KEY, gioHang);

            return RedirectToAction("Index");
        }
        public IActionResult RemoveCart(int id)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.MaHh == id);
            if(item != null)
            {
                gioHang.Remove(item);
                HttpContext.Session.Set(MySetting.CART_KEY, gioHang);
			}
			return RedirectToAction("Index");
		}
    }
}
