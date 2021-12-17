using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KingFashion.Models;

namespace KingFashion.Areas.Admin.Controllers
{
    public class QuanlyTaiKhoanAdminController : Controller
    {
        // GET: Admin/QuanlyTaiKhoanAdmin
        dbKingFashionDataContext db = new dbKingFashionDataContext();
        // GET: Admin/KhachHang
        public ActionResult Index()
        {
            return View(db.ADMINs.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ADMIN cd, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                cd.HoTen = f["sHoTen"];
                cd.NgaySinh = Convert.ToDateTime(f["dNgaySinh"]);
                cd.DiaChi = f["sDiaChi"];
                cd.DienThoai = f["sDienThoai"];
                cd.Email = f["sEmail"];
                cd.TenDN = f["sTenDn"];
                cd.MatKhau = f["sMatkhau"];
                cd.Quyen = int.Parse(f["sQuyen"]);
                cd.ChucVu = f["sChucVu"];
                db.ADMINs.InsertOnSubmit(cd);
                db.SubmitChanges();
                //Về lại trang Quản lý cd
                return RedirectToAction("Index");
            }
            return View();
        }
        /*public ActionResult Details(int id)
        {
            var kh = db.ADMINs.SingleOrDefault(n => n.MaAd == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }*/
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var kh = db.ADMINs.SingleOrDefault(n => n.MaAd == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var kh = db.ADMINs.SingleOrDefault(n => n.MaAd == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            if (kh.Quyen==1)
            {
                // nội dung sẽ hiển thị khi .. cần xóa  đã có trong table

                ViewBag.ThongBao = "Không thể xóa tài khoản này. Chỉ có thể chỉnh sửa!";
                return View(kh);
            }
            // xóa tk
            db.ADMINs.DeleteOnSubmit(kh);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var ad = db.ADMINs.SingleOrDefault(n => n.MaAd == id);
            if (ad == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(ad);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {
            var cd = db.ADMINs.SingleOrDefault(n => n.MaAd == int.Parse(f["iMaAd"]));

            if (ModelState.IsValid)
            {
                cd.HoTen = f["sHoTen"];
                cd.NgaySinh = Convert.ToDateTime(f["dNgaySinh"]);
                cd.DiaChi = f["sDiaChi"];
                cd.DienThoai = f["sDienThoai"];
                cd.Email = f["sEmail"];
                cd.TenDN = f["sTenDn"];
                cd.MatKhau = f["sMatkhau"];
                cd.Quyen = int.Parse(f["sQuyen"]);
                cd.ChucVu = f["sChucVu"];
                
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(cd);
        }
    }
}