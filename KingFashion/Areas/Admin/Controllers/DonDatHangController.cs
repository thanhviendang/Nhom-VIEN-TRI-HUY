using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KingFashion.Models;

namespace KingFashion.Areas.Admin.Controllers
{
    public class DonDatHangController : Controller
    {
        dbKingFashionDataContext db = new dbKingFashionDataContext();
        // GET: Admin/DonDatHang
        
        public ActionResult Index()
        {
            return View(db.DONDATHANGs.ToList());
        }

        public ActionResult DonHangDone()
        {
            return View(db.DONDATHANGs.Where(a => a.TinhTrangGiaoHang == "Hoàn thành").ToList());
        }
        public ActionResult DonHangDtoD(string ngayDau, string ngayCuoi)
        {
            ViewBag.NgayDau = ngayDau;
            ViewBag.NgayCuoi = ngayCuoi;

            if (!string.IsNullOrEmpty(ngayDau) && !string.IsNullOrEmpty(ngayDau))
            {
                var spDtoD = db.DONDATHANGs.Where(a => a.NgayGiao > Convert.ToDateTime(ngayDau) && a.NgayGiao < Convert.ToDateTime(ngayCuoi));
                return View(spDtoD.ToList());
            }
            return View();
        }
        public ActionResult Details(int id)
        {
            var kh = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            
            return View(kh);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var ddh = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            if (ddh == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(ddh);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {
            var ddh = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == int.Parse(f["iMaDonHang"]));
            if (ModelState.IsValid)
            {
                ddh.TinhTrangGiaoHang = f["sTinhTrang"];
                ddh.NgayDat =  Convert.ToDateTime(f["dNgayDat"]);
                ddh.NgayGiao = Convert.ToDateTime(f["dNgayGiao"]);
                ddh.MaKH = int.Parse(f["MaKH"]);
                ddh.NoiNhan = f["sNoiNhan"];
                ddh.HoTenNguoiNhan = f["sNguoiNhan"];
                ddh.Sodienthoai = f["sSoDienThoai"];
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(ddh);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var sp = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var sp = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var cdsp = db.KHACHHANGs.Where(c => c.MaKH == id);
            if (cdsp.Count() > 0)
            {
                // nội dung sẽ hiển thị khi .. cần xóa  đã có trong table

                ViewBag.ThongBao = "Đang có sản phẩm trong chủ đề này" +"/n"+
                    "Nếu muốn xóa thì phải xóa hết sản phẩm có trong chủ đề";
                return View(sp);
            }


            db.DONDATHANGs.DeleteOnSubmit(sp);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

    }
}