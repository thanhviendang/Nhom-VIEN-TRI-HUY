using KingFashion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KingFashion.Areas.Admin.Controllers
{
    public class ThongKeController : Controller
    {
        dbKingFashionDataContext data = new dbKingFashionDataContext();
        // GET: Admin/ThongKe
        public ActionResult ThongKe()
        {
            
            var tongdon = data.DONDATHANGs.Count();
            var summ = data.CHITIETDATHANGs.Count();
            var sumpr = data.CHITIETDATHANGs.Sum(a => a.ThanhTien);
            var tienvon = data.CHITIETDATHANGs.Where(a=> a.MaSp == a.SANPHAM.MaSP).Sum(a => a.SANPHAM.Giagoc * a.SoLuong);
            
            ViewBag.tongdon = tongdon;
            ViewBag.TongSpBanDc = summ;
            ViewBag.TongTien = sumpr;
            ViewBag.TienVon = tienvon;
            ViewBag.LoiNhuan = sumpr - tienvon;

            var tongdonDone = data.DONDATHANGs.Where(a => a.TinhTrangGiaoHang == "Hoàn thành").Count();
            var tongSpDone = data.CHITIETDATHANGs.Where(a => a.DONDATHANG.TinhTrangGiaoHang == "Hoàn thành").Count();
            var tongThuDone = data.CHITIETDATHANGs.Where(a => a.DONDATHANG.TinhTrangGiaoHang == "Hoàn thành").Sum(a => a.ThanhTien);
            var tongVonDone = data.CHITIETDATHANGs.Where(a => a.MaSp == a.SANPHAM.MaSP && a.DONDATHANG.TinhTrangGiaoHang == "Hoàn thành").Sum(a => a.SANPHAM.Giagoc * a.SoLuong);

            ViewBag.TongDonDone = tongdonDone;
            ViewBag.TongSpDone = tongSpDone;
            ViewBag.TongThuDone = tongThuDone;
            ViewBag.TongVonDone = tongVonDone;
            ViewBag.LoiNhuanDone = tongThuDone - tongVonDone;

            return View();
        }

        public ActionResult ThongKeTheoNgayPartial(string ngayDau, string ngayCuoi)
        {
            ViewBag.NgayDau = ngayDau;
            ViewBag.NgayCuoi = ngayCuoi;
            if (!string.IsNullOrEmpty(ngayDau) && !string.IsNullOrEmpty(ngayDau))
            {
                var donHangDtoD = data.DONDATHANGs.Where(a => a.NgayGiao > Convert.ToDateTime(ngayDau) && a.NgayGiao < Convert.ToDateTime(ngayCuoi)).Count();
                var spBanDuocDtoD = data.CHITIETDATHANGs.Where(a => a.DONDATHANG.NgayGiao > Convert.ToDateTime(ngayDau) && a.DONDATHANG.NgayGiao < Convert.ToDateTime(ngayCuoi)).Count();
                var tongThuDtoD = data.CHITIETDATHANGs.Where(a => a.DONDATHANG.NgayGiao > Convert.ToDateTime(ngayDau) && a.DONDATHANG.NgayGiao < Convert.ToDateTime(ngayCuoi)).Sum(a => a.ThanhTien);
                var tongVonDtoD = data.CHITIETDATHANGs.Where(a => a.DONDATHANG.NgayGiao > Convert.ToDateTime(ngayDau) && a.DONDATHANG.NgayGiao < Convert.ToDateTime(ngayCuoi) && a.MaSp == a.SANPHAM.MaSP).Sum(a => a.SANPHAM.Giagoc * a.SoLuong);
                ViewBag.DhDtoD = donHangDtoD;
                ViewBag.SpDtoD = spBanDuocDtoD;
                ViewBag.TongThuDtoD = tongThuDtoD;
                ViewBag.TongVonDtoD = tongVonDtoD;
                ViewBag.LoiNhuanDtoD = tongThuDtoD - tongVonDtoD;

            }
            return View();
        }
        public ActionResult ThongKeTheoNgay(string ngayDau, string ngayCuoi)
        {

            ViewBag.NgayDau = ngayDau;
            ViewBag.NgayCuoi = ngayCuoi;
            if (!string.IsNullOrEmpty(ngayDau) && !string.IsNullOrEmpty(ngayDau))
            {
                var donHangDtoD = data.DONDATHANGs.Where(a => a.NgayGiao > Convert.ToDateTime(ngayDau) && a.NgayGiao < Convert.ToDateTime(ngayCuoi)).Count();
                var spBanDuocDtoD = data.CHITIETDATHANGs.Where(a => a.DONDATHANG.NgayGiao > Convert.ToDateTime(ngayDau) && a.DONDATHANG.NgayGiao < Convert.ToDateTime(ngayCuoi)).Count();
                var tongThuDtoD = data.CHITIETDATHANGs.Where(a => a.DONDATHANG.NgayGiao > Convert.ToDateTime(ngayDau) && a.DONDATHANG.NgayGiao < Convert.ToDateTime(ngayCuoi)).Sum(a => a.ThanhTien);
                var tongVonDtoD = data.CHITIETDATHANGs.Where(a => a.DONDATHANG.NgayGiao > Convert.ToDateTime(ngayDau) && a.DONDATHANG.NgayGiao < Convert.ToDateTime(ngayCuoi) && a.MaSp == a.SANPHAM.MaSP).Sum(a => a.SANPHAM.Giagoc);
                ViewBag.DhDtoD = donHangDtoD;
                ViewBag.SpDtoD = spBanDuocDtoD;
                ViewBag.TongThuDtoD = tongThuDtoD;
                ViewBag.TongVonDtoD = tongVonDtoD;
                ViewBag.LoiNhuanDtoD = tongThuDtoD - tongVonDtoD;

            }
            return View();
        }
       
        public ActionResult SpdabanDtoD(string ngayDau, string ngayCuoi)
        {
             ViewBag.NgayDau = ngayDau;
             ViewBag.NgayCuoi = ngayCuoi;

            if (!string.IsNullOrEmpty(ngayDau) && !string.IsNullOrEmpty(ngayDau))
             {
                var spDtoD = data.CHITIETDATHANGs.Where(a => a.DONDATHANG.NgayGiao > Convert.ToDateTime(ngayDau) && a.DONDATHANG.NgayGiao < Convert.ToDateTime(ngayCuoi));
                return View(spDtoD.ToList());
            }
            return View();
        }
    }
}