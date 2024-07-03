using BanHaiSan.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using static System.Runtime.InteropServices.JavaScript.JSType;
using X.PagedList;
using Microsoft.EntityFrameworkCore;
using System.Data;
using BanHaiSan.ViewModel;

namespace BanHaiSan.Controllers
{
    public class Productc : Controller
    {

        BanhaisaiContext db = new BanhaisaiContext();


        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Product(int? intpage)
        {
            int pageSize = 3;
            int countPage = 0;
            var listpro = db.HaiSans.ToList();
            countPage = (listpro.Count() % pageSize == 0) ? (listpro.Count() / pageSize) :
                                        ((listpro.Count() / pageSize) + 1);

            if (intpage == null)
            {
                intpage = 1; // Trang mặc định là trang đầu tiên
            }

            int startIndex = ((int)intpage - 1) * pageSize;
            listpro = listpro.Skip(startIndex).Take(pageSize).ToList();

            ViewBag.CurrentPage = intpage; // Gửi số trang hiện tại đến view
            ViewBag.CountPages = countPage;
            return View(listpro);
        }

        public IActionResult ListProduct(int? intpage)
        {
            int pageSize = 3;
            int countPage = 0;
            var listpro = db.HaiSans.ToList();
            countPage = (listpro.Count() % pageSize == 0) ? (listpro.Count() / pageSize) :
                                        ((listpro.Count() / pageSize) + 1);

            if (intpage == null)
            {
                intpage = 1; // Trang mặc định là trang đầu tiên
            }

            int startIndex = ((int)intpage - 1) * pageSize;
            listpro = listpro.Skip(startIndex).Take(pageSize).ToList();

            ViewBag.CurrentPage = intpage; // Gửi số trang hiện tại đến view
            ViewBag.CountPages = countPage;
            return PartialView(listpro);
        }
        public IActionResult onload(int? intpage)
        {
            int pageSize = 4;
            int countPage = 0;
            var listpro = db.HaiSans.ToList();
            countPage = (listpro.Count() % pageSize == 0) ? (listpro.Count() / pageSize) :
                                        ((listpro.Count() / pageSize) + 1);

            if (intpage == null)
            {
                intpage = 1; // Trang mặc định là trang đầu tiên
            }

            int startIndex = ((int)intpage - 1) * pageSize;
            listpro = listpro.Skip(startIndex).Take(pageSize).ToList();
            return PartialView(listpro);
        }
        public IActionResult Listv(int? intpage)
        {
            int pageSize = 3;
            int countPage = 0;
            var listpro = db.HaiSans.ToList();
            countPage = (listpro.Count() % pageSize == 0) ? (listpro.Count() / pageSize) :
                                        ((listpro.Count() / pageSize) + 1);

            if (intpage == null)
            {
                intpage = 1; // Trang mặc định là trang đầu tiên
            }

            int startIndex = ((int)intpage - 1) * pageSize;
            listpro = listpro.Skip(startIndex).Take(pageSize).ToList();

            ViewBag.CurrentPage = intpage; // Gửi số trang hiện tại đến view
            ViewBag.CountPages = countPage;
            return PartialView(listpro);
        }
        public ActionResult ListProductByCategory(int idcategory)
        {

            if(idcategory == 0)
            {
                int intpage = 1;
                int pageSize = 3;
                int countPage = 0;
                var listpro = db.HaiSans.ToList();
                countPage = (listpro.Count() % pageSize == 0) ? (listpro.Count() / pageSize) :
                                            ((listpro.Count() / pageSize) + 1);

                if (intpage == null)
                {
                    intpage = 1; // Trang mặc định là trang đầu tiên
                }

                int startIndex = ((int)intpage - 1) * pageSize;
                listpro = listpro.Skip(startIndex).Take(pageSize).ToList();

                ViewBag.CurrentPage = intpage; // Gửi số trang hiện tại đến view
                ViewBag.CountPages = countPage;
                return PartialView(listpro);
            }    
            // Lấy danh sách sản phẩm theo loại hải sản từ cơ sở dữ liệu
            var products = db.HaiSans.Where(h => h.MaLoai == idcategory).ToList();

            // Trả về partial view chứa danh sách sản phẩm
            return PartialView(products);
        }


        public IActionResult SortProducts(int sortOrder)
        {
            // Xử lý sắp xếp sản phẩm tùy theo sortOrder
            var pro=db.HaiSans.ToList();
            switch (sortOrder)
            {
                case 1:
                    // Sắp xếp theo tên
                    pro = db.HaiSans.OrderBy(p => p.TenHaiSan).ToList();
                    break;
                case 2:
                    // Sắp xếp giá tăng dần
                    pro = db.HaiSans.OrderBy(p => p.Gia).ToList();
                    break;
                case 3:
                    // Sắp xếp giá giảm dần
                    pro = db.HaiSans.OrderByDescending(p => p.Gia).ToList();
                    break;
                default:
                    // Mặc định không sắp xếp
                    pro = db.HaiSans.ToList();
                    break;
            }

            // Trả về danh sách sản phẩm đã sắp xếp dưới dạng partial view
            return PartialView(pro);
        }



        public IActionResult shopDetail(int id)
        {
            //var product = db.HaiSans.FirstOrDefault(p => p.MaHaiSan == id);
            //if (product == null)
            //{
            //    return NotFound();
            //}

            //// Lấy tên loại hải sản
            //var loaiHaiSan = db.LoaiHaiSans.FirstOrDefault(l => l.MaLoai == product.MaLoai);
            //if (loaiHaiSan != null)
            //{
            //    product.TenHaiSan = loaiHaiSan.TenLoai;
            //}
            var product = db.HaiSans
                    .Include(p => p.MaLoaiNavigation) // Lấy thông tin về loại hải sản
                    .Include(p => p.MaNccNavigation)  // Lấy thông tin về nhà cung cấp
                    .FirstOrDefault(p => p.MaHaiSan == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        


    }
}
