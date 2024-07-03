using BanHaiSan.Models;
using BanHaiSan.ViewModel;
using DurableTask.Core.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BanHaiSan.Controllers
{
    public class AddMinController : Controller
    {

        BanhaisaiContext db = new BanhaisaiContext();
        public IActionResult Index()
        {
            return PartialView();
        }
        public ActionResult ShowPro(int? intpage)
        {
            List<EditProVM> listPro1 = new List<EditProVM>();
            using(var db = new BanhaisaiContext())
            {
                listPro1 = (
                    from p in db.HaiSans
                    join c in db.LoaiHaiSans on p.MaLoai equals c.MaLoai into Loai
                    from c in Loai.DefaultIfEmpty()
                    join s in db.NhaCungCaps on p.MaNcc equals s.MaNcc into supplierJoin
                    from s in supplierJoin.DefaultIfEmpty()
                    select new EditProVM
                    {
                        MaLoai = c.MaLoai,
                        TenHaiSan = p.TenHaiSan,
                        MaHaiSan = p.MaHaiSan,
                        tenLoai = c.TenLoai,
                        Gia = p.Gia,
                        MoTa = p.MoTa,
                        TenNCC = s.TenNcc,
                        Img = p.Img,
                       
                    }).ToList();
            }

            int pageSize = 3;
            int countPage = 0;

            if (intpage == null || intpage < 1)
            {
                intpage = 1; // Trang mặc định là trang đầu tiên
            }
            countPage = (int)Math.Ceiling((double)listPro1.Count() / pageSize);
            ViewBag.CurrentPage = intpage; // Gửi số trang hiện tại đến view
            ViewBag.CountPages = countPage;
            int startIndex = (intpage.Value - 1) * pageSize;
            listPro1 = listPro1.Skip(startIndex).Take(pageSize).ToList();
            return PartialView(listPro1);
        }
        public ActionResult ShowProV(int? intpage)
        {
            List<EditProVM> listPro1 = new List<EditProVM>();
            using (var db = new BanhaisaiContext())
            {
                listPro1 = (
                    from p in db.HaiSans
                    join c in db.LoaiHaiSans on p.MaLoai equals c.MaLoai into Loai
                    from c in Loai.DefaultIfEmpty()
                    join s in db.NhaCungCaps on p.MaNcc equals s.MaNcc into supplierJoin
                    from s in supplierJoin.DefaultIfEmpty()
                    select new EditProVM
                    {
                        MaLoai = c.MaLoai,
                        TenHaiSan = p.TenHaiSan,
                        MaHaiSan = p.MaHaiSan,
                        tenLoai = c.TenLoai,
                        Gia = p.Gia,
                        MoTa = p.MoTa,
                        TenNCC = s.TenNcc,
                        Img = p.Img,
                    }).ToList();
            }

            int pageSize = 3;
            int countPage = 0;

            if (intpage == null || intpage < 1)
            {
                intpage = 1; // Trang mặc định là trang đầu tiên
            }
            countPage = (int)Math.Ceiling((double)listPro1.Count() / pageSize);
            ViewBag.CurrentPage = intpage; // Gửi số trang hiện tại đến view
            ViewBag.CountPages = countPage;
            int startIndex = (intpage.Value - 1) * pageSize;
            listPro1 = listPro1.Skip(startIndex).Take(pageSize).ToList();
            return PartialView(listPro1);
        }

        public ActionResult categorize(int? intpage)
        {

            var cate = db.LoaiHaiSans.ToList();
            int pageSize = 3;
            int countPage = 0;

            if (intpage == null || intpage < 1)
            {
                intpage = 1; // Trang mặc định là trang đầu tiên
            }
            countPage = (int)Math.Ceiling((double)cate.Count() / pageSize);
            ViewBag.CurrentPage = intpage; // Gửi số trang hiện tại đến view
            ViewBag.CountPages = countPage;
            int startIndex = (intpage.Value - 1) * pageSize;
            cate = cate.Skip(startIndex).Take(pageSize).ToList();
            return PartialView(cate);
        }
        public ActionResult categorizeV(int? intpage)
        {

            var cate = db.LoaiHaiSans.ToList();
            int pageSize = 3;
            int countPage = 0;

            if (intpage == null || intpage < 1)
            {
                intpage = 1; // Trang mặc định là trang đầu tiên
            }
            countPage = (int)Math.Ceiling((double)cate.Count() / pageSize);
            ViewBag.CurrentPage = intpage; // Gửi số trang hiện tại đến view
            ViewBag.CountPages = countPage;
            int startIndex = (intpage.Value - 1) * pageSize;
            cate = cate.Skip(startIndex).Take(pageSize).ToList();
            return PartialView(cate);
        }
        public ActionResult XoaCate(int idpro)
        {
            LoaiHaiSan model;
            int idcheck = idpro;
            using (BanhaisaiContext db = new BanhaisaiContext())
            {
                model = db.LoaiHaiSans.FirstOrDefault(x => x.MaLoai == idcheck);
                db.LoaiHaiSans.Remove(model);
                db.SaveChanges();
                return RedirectToAction("categorizeV", "AddMin");
            }
        }
        public async Task<IActionResult> SaveCate(EditCate proVM)
        {
            using (var db = new BanhaisaiContext())
            {
                if (ModelState.IsValid)
                {
                    if (proVM.MaLoai == null) // Kiểm tra xem MaLoai có giá trị mặc định (0) không
                    {
                        LoaiHaiSan pro = new LoaiHaiSan();
                        pro.TenLoai = proVM.TenLoai;
                        db.LoaiHaiSans.Add(pro);
                        db.SaveChanges();
                    }
                    else
                    {
                        LoaiHaiSan pro = db.LoaiHaiSans.FirstOrDefault(x => x.MaLoai == proVM.MaLoai);
                        if (pro != null)
                        {
                            pro.TenLoai = proVM.TenLoai;
                            db.SaveChanges();
                        }
                    }
                    return RedirectToAction("Index");
                }
            }
            return Content("");
        }
        public ActionResult AddCate(int? idpro)
        {
            EditCate product = null;
            using (var db = new BanhaisaiContext())
            {
                if (idpro != null)
                {
                    product = db.LoaiHaiSans.Where(x => x.MaLoai == idpro)
                        .Select(x => new EditCate
                        {
                            MaLoai = x.MaLoai,
                            TenLoai = x.TenLoai
                        }).FirstOrDefault();
                }
            }
            if (product != null)
            {
                return PartialView(product);
            }
            return PartialView();
        }
        public ActionResult Add(int? idpro)
        {
            EditProVM product = null;
            using (var db = new BanhaisaiContext())
            {
                if (idpro != null)
                {
                    product = db.HaiSans.Where(x => x.MaHaiSan == idpro)
                        .Select(x => new EditProVM
                        {
                            MaLoai = x.MaLoai,
                            TenHaiSan = x.TenHaiSan,
                            MaHaiSan = x.MaHaiSan,
                            Gia = x.Gia,
                            MoTa = x.MoTa,
                            MaNcc = x.MaNcc,
                            CanNangTB = x.CanNangTB,
                            CanNangMin = x.CanNangMin,
                            ChatLuong = x.ChatLuong,
                            Nguon = x.Nguon
                        }).FirstOrDefault();
                }

                ViewBag.ListCate = db.LoaiHaiSans.ToList();
                ViewBag.listCC = db.NhaCungCaps.ToList();
            }
            if (product != null)
            {
                return PartialView(product);
            }
            return PartialView();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(AddProduct proVM, IFormFile Img)
        {
            using (var db = new BanhaisaiContext())
            {
                if (proVM.MaHaiSan == null)
                {
                    if (ModelState.IsValid)
                    {

                        HaiSan pro = new HaiSan();
                        //lấy tên file có đuôi
                        if (Img != null && Img.Length > 0)
                        {
                            var fileName = Path.GetFileName(Img.FileName);
                            // Lấy tên file gốc không có phần mở rộng
                            var originalFileName = Path.GetFileNameWithoutExtension(Img.FileName);
                            // Lấy phần mở rộng của file
                            var fileExtension = Path.GetExtension(Img.FileName);

                            // Tạo đường dẫn lưu trữ tệp hình ảnh
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Product", fileName);

                            // Lưu tệp hình ảnh vào đường dẫn đã chọn
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await Img.CopyToAsync(fileStream);
                            }
                            pro.Img = fileName;
                        }
                        else
                        {
                            pro.Img = "NoImage.jpg";
                        }    
                        pro.TenHaiSan =proVM.TenHaiSan;
                        pro.MaLoai = proVM.MaLoai;
                        pro.Gia = proVM.Gia;
                        pro.MoTa = proVM.MoTa;
                        pro.MaNcc = proVM.MaNcc;
                        pro.CanNangMin = proVM.CanNangMin;
                        pro.CanNangTB = proVM.CanNangTB;
                        pro.ChatLuong = proVM.ChatLuong;
                        pro.Nguon = proVM.Nguon;
                        db.HaiSans.Add(pro);
                        db.SaveChanges();
                        return RedirectToAction("");
                    }
                }
                else
                {
                    HaiSan pro = db.HaiSans.FirstOrDefault(x => x.MaHaiSan == proVM.MaHaiSan);

                    if (Img != null && Img.Length > 0)
                    {
                        var fileName = Path.GetFileName(Img.FileName);
                        // Lấy tên file gốc không có phần mở rộng
                        var originalFileName = Path.GetFileNameWithoutExtension(Img.FileName);
                        // Lấy phần mở rộng của file
                        var fileExtension = Path.GetExtension(Img.FileName);

                        // Tạo đường dẫn lưu trữ tệp hình ảnh
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Product", fileName);

                        // Lưu tệp hình ảnh vào đường dẫn đã chọn
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await Img.CopyToAsync(fileStream);
                        }
                        pro.Img = fileName;
                    }
                    else
                    {
                        
                    }
                    pro.TenHaiSan = proVM.TenHaiSan;
                    pro.MaLoai = proVM.MaLoai;
                    pro.Gia = proVM.Gia;
                    pro.MoTa = proVM.MoTa;
                    pro.MaNcc = proVM.MaNcc;
                    pro.CanNangMin = proVM.CanNangMin;
                    pro.CanNangTB = proVM.CanNangTB;
                    pro.ChatLuong = proVM.ChatLuong;
                    pro.Nguon = proVM.Nguon;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return Content("");
        }
        public ActionResult Xoa(int idpro)
        {
            HaiSan model;
            int idcheck = idpro;
            using (BanhaisaiContext db = new BanhaisaiContext())
            {
                model = db.HaiSans.FirstOrDefault(x => x.MaHaiSan == idcheck);
                db.HaiSans.Remove(model);
                db.SaveChanges();
                return RedirectToAction("");
            }
        }

        public ActionResult NhaCungCap(int? intpage)
        {

            var cate = db.NhaCungCaps.ToList();
            int pageSize = 3;
            int countPage = 0;

            if (intpage == null || intpage < 1)
            {
                intpage = 1; // Trang mặc định là trang đầu tiên
            }
            countPage = (int)Math.Ceiling((double)cate.Count() / pageSize);
            ViewBag.CurrentPage = intpage; // Gửi số trang hiện tại đến view
            ViewBag.CountPages = countPage;
            int startIndex = (intpage.Value - 1) * pageSize;
            cate = cate.Skip(startIndex).Take(pageSize).ToList();
            return PartialView(cate);
        }
        public ActionResult NhaCungCapV(int? intpage)
        {

            var cate = db.NhaCungCaps.ToList();
            int pageSize = 3;
            int countPage = 0;

            if (intpage == null || intpage < 1)
            {
                intpage = 1; // Trang mặc định là trang đầu tiên
            }
            countPage = (int)Math.Ceiling((double)cate.Count() / pageSize);
            ViewBag.CurrentPage = intpage; // Gửi số trang hiện tại đến view
            ViewBag.CountPages = countPage;
            int startIndex = (intpage.Value - 1) * pageSize;
            cate = cate.Skip(startIndex).Take(pageSize).ToList();
            return PartialView(cate);
        }
        public ActionResult XoaNCC(int idpro)
        {
            NhaCungCap model;
            int idcheck = idpro;
            using (BanhaisaiContext db = new BanhaisaiContext())
            {
                model = db.NhaCungCaps.FirstOrDefault(x => x.MaNcc == idcheck);
                db.NhaCungCaps.Remove(model);
                db.SaveChanges();
                return RedirectToAction("NhaCungCapV", "AddMin");
            }
        }
        public ActionResult SaveNCC(NCCVM proVM)
        {
            using (var db = new BanhaisaiContext())
            {
                if (ModelState.IsValid)
                {
                    if (proVM.MaNcc == null) // Kiểm tra xem MaLoai có giá trị mặc định (0) không
                    {
                        NhaCungCap pro = new Models.NhaCungCap();
                        pro.TenNcc = proVM.TenNcc;
                        pro.DiaChi = proVM.DiaChi;
                        pro.Sdt = proVM.Sdt;
                        db.NhaCungCaps.Add(pro);
                        db.SaveChanges();
                    }
                    else
                    {
                        NhaCungCap pro = db.NhaCungCaps.FirstOrDefault(x => x.MaNcc == proVM.MaNcc);
                        if (pro != null)
                        {
                            pro.TenNcc = proVM.TenNcc;
                            pro.DiaChi = proVM.DiaChi;
                            pro.Sdt = proVM.Sdt;
                            db.SaveChanges();
                        }
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return Content("");
        }
        public ActionResult AddNCC(int? idpro)
        {
            NCCVM product = null;
            using (var db = new BanhaisaiContext())
            {
                if (idpro != null)
                {
                    product = db.NhaCungCaps.Where(x => x.MaNcc == idpro)
                        .Select(x => new NCCVM
                        {
                            MaNcc = x.MaNcc,
                            TenNcc = x.TenNcc,
                            DiaChi = x.DiaChi,
                            Sdt = x.Sdt
                        }).FirstOrDefault();
                }
            }
            if (product != null)
            {
                return PartialView(product);
            }
            return PartialView();
        }
        public ActionResult TaiKhoans(int? intpage)
        {

            var us = db.TaiKhoans.ToList();
            int pageSize = 3;
            int countPage = 0;

            if (intpage == null || intpage < 1)
            {
                intpage = 1; // Trang mặc định là trang đầu tiên
            }
            countPage = (int)Math.Ceiling((double)us.Count() / pageSize);
            ViewBag.CurrentPage = intpage; // Gửi số trang hiện tại đến view
            ViewBag.CountPages = countPage;
            int startIndex = (intpage.Value - 1) * pageSize;
            us = us.Skip(startIndex).Take(pageSize).ToList();
            return PartialView(us);
        }
    }
}


