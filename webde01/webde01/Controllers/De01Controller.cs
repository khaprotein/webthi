using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webde01.Models;

namespace webde01.Controllers
{
    public class De01Controller : Controller
    {
        // GET: De01
        QLBanQuanAo_01Entities db = new QLBanQuanAo_01Entities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Product()
        {
            List<SanPham> sp = db.SanPhams.ToList();
            return PartialView("_Main", sp);
        }

        public ActionResult ProductID(string mapl)
        {
            List<SanPham> sp = db.SanPhams.Where(c=>c.MaPhanLoai == mapl).ToList();
            return PartialView("_Main", sp);
        }

        public ActionResult Them()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Them(SanPham sp, HttpPostedFileBase fileanh)
        {
            if (fileanh.ContentLength > 0)
            {
                string rf = Server.MapPath("~/Content/images/");
                string pathimage = rf + fileanh.FileName;
                fileanh.SaveAs(pathimage);
                sp.AnhDaiDien = fileanh.FileName;
            }

            try
            {
                sp.MaPhanLoai = Request.Form["Mapl"];
                sp.MaPhanLoaiPhu = Request.Form["Maplp"];

                db.SanPhams.Add(sp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(sp);
            }
        }
    }
}