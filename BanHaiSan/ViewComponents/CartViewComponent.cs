using Microsoft.AspNetCore.Mvc;
using BanHaiSan.Models;
using BanHaiSan.ViewModel;
using BanHaiSan.Helpers;
using System.Xml;
using MongoDB.Driver.Core.Operations;
namespace BanHaiSan.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get < List<CartItem>>(MySetting.CART_KEY)?? new List<CartItem>();
            return View("CartPanel",new CartModel
            {
                Quantity = cart.Sum(p => p.SoLuong),
                Total = cart.Sum(p=>p.ThanhTien)
            });
                
        }
    }
}
