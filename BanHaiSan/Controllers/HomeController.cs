using BanHaiSan.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using BanHaiSan.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using X.PagedList;

namespace BanHaiSan.Controllers
{
    public class HomeController : Controller
    {

        BanhaisaiContext db = new BanhaisaiContext();
        private readonly ILogger<HomeController> _logger;



        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int page = 1)
        {
            page = page < 1 ? 1 : page;
            int pageSize = 3;
            var pro = db.HaiSans.ToPagedList(page, pageSize);
            return View(pro);
            //var lstSanPham = db.HaiSans.ToList();
            //return View(lstSanPham);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult Caart()
        {
            var po = db.ChiTietDonHangs.ToList();
            return View(po);
        }


    }
}
