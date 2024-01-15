using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webde08.Models;
namespace webde08.Controllers
{
    public class De08Controller : Controller
    {
        // GET: De08
        QLBanChauCanhEntities db = new QLBanChauCanhEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Menu()
        {
            List<PhanLoaiPhu> plp = db.PhanLoaiPhus.ToList();
            return PartialView("_Header",plp);
        }
        public ActionResult Product()
        {
            List<SanPham> sp = db.SanPhams.ToList();
            return PartialView("_Main", sp);
        }
        public ActionResult ProductID(string maplp)
        {
            List<SanPham> sp = db.SanPhams.Where(c=>c.MaPhanLoaiPhu == maplp ).ToList();
            return PartialView("_Main", sp);
        }

        public ActionResult Update(string id)
        {
            var sp = db.SanPhams.Find(id);
            return View(sp);
        }
        [HttpPost]
        public ActionResult Update(SanPham sp , HttpPostedFileBase fileanh)
        {
            if (fileanh.ContentLength > 0)
            {
                string rf = Server.MapPath("/Content/img/bg-img/");
                string pathimage = rf + fileanh.FileName;
                fileanh.SaveAs(pathimage);
                sp.AnhDaiDien = fileanh.FileName;
            }
            var s = db.SanPhams.Find(sp.MaSanPham);
            try
            {
                s.TenSanPham = sp.TenSanPham;
                s.DonGiaBanLonNhat = sp.DonGiaBanLonNhat;
                s.DonGiaBanNhoNhat = sp.DonGiaBanNhoNhat;
                s.AnhDaiDien = sp.AnhDaiDien;
                s.GiaNhap = sp.GiaNhap;
                s.MaPhanLoai = sp.MaPhanLoai;
                s.MaPhanLoaiPhu = sp.SelectedMaPLP;
                s.MoTaNgan = sp.MoTaNgan;
                s.TrangThai = sp.TrangThai;
                s.NoiBat = sp.NoiBat;

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(sp);
            }   
        }


        public ActionResult Xoa(string id)
        {
            var sp = db.SanPhams.Find(id);
            db.SanPhams.Remove(sp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}