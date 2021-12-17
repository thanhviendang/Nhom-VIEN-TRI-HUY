using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KingFashion.Models;

namespace KingFashion.Areas.Admin.Controllers
{
    public class ChiTietDonHangController : Controller
    {
        dbKingFashionDataContext db = new dbKingFashionDataContext();
        // GET: Admin/ChiTietDonHang
        public ActionResult Index(int id)
        {
            var sp = from s in db.CHITIETDATHANGs
                     where s.MaDonHang == id
                     select s;
            return PartialView(sp.ToList());
        }
        public ActionResult Spdaban()
        {
            var sl = db.CHITIETDATHANGs.Where(a => a.MaSp == a.SANPHAM.MaSP && a.DONDATHANG.TinhTrangGiaoHang == "Hoàn thành").Sum(a => a.SoLuong);
            var sum = db.CHITIETDATHANGs.Where(a => a.MaSp == a.SANPHAM.MaSP && a.DONDATHANG.TinhTrangGiaoHang == "Hoàn thành").Sum(a => a.ThanhTien);
            var sumgoc = db.CHITIETDATHANGs.Where(a => a.MaSp == a.SANPHAM.MaSP && a.DONDATHANG.TinhTrangGiaoHang == "Hoàn thành").Sum(a => a.SANPHAM.Giagoc * a.SoLuong);
            ViewBag.TongSl = sl;
            ViewBag.SumVon = sumgoc;
            ViewBag.LoiNhuan = sum - sumgoc;
            return View(db.CHITIETDATHANGs.Where(a => a.MaSp == a.SANPHAM.MaSP && a.DONDATHANG.TinhTrangGiaoHang == "Hoàn thành").ToList());
        }

        
    }
}