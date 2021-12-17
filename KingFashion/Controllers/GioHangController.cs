using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KingFashion.Models;

namespace TravelShop.Controllers
{
    public class GioHangController : Controller
    {
        
        // GET: GioHang
        dbKingFashionDataContext db = new dbKingFashionDataContext();
        public ActionResult Index()
        {
            return View();
        }
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                //khởi tạo giỏ hàng ( giỏ hàng chưa tồn tại)
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        //them san pham vao giỏ hàng
        public ActionResult ThemGioHang(int ms, string url)
        {
            // lấy giỏ hàng hiện tại
            List<GioHang> lstGioHang = LayGioHang();
            // Kiểm tra nếu sản phẩm chưa có trong giỏ thì thêm vào, nếu đã có thì tăng số lượng
            GioHang sp = lstGioHang.Find(n => n.iMaSP == ms);
            if (sp == null)
            {
                sp = new GioHang(ms);
                lstGioHang.Add(sp);
            }
            else
            {   
                sp.iSoLuong++;
            }
            return Redirect(url);
        }
        // tổng số lượng
        private double TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        // tính tổng tiền
        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.dThanhTien);
            }
            return dTongTien;
        }
        // action trả về view GioHang
        public ActionResult GioHang()
        {
            List<GioHang> lstGioHang = LayGioHang();
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "KingFashion");
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        // xóa sản phẩm khỏi giỏ hàng
        public ActionResult XoaSPKhoiGioHang(int iMaSP)
        {
            List<GioHang> lstGioHang = LayGioHang();
            //kiem tra sach da co trong session["GioHang"]
            GioHang sp = lstGioHang.SingleOrDefault(n => n.iMaSP == iMaSP);
            // xóa sp khỏi giỏ hàng
            if (sp != null)
            {
                lstGioHang.RemoveAll(n => n.iMaSP == iMaSP);
                if (lstGioHang.Count == 0)
                {
                    return RedirectToAction("Index", "KingFashion");
                }
            }
            return RedirectToAction("GioHang");
        }
        // cập nhật giỏ hàng
        public ActionResult CapNhatGioHang(int iMaSP, FormCollection f)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.SingleOrDefault(n => n.iMaSP == iMaSP);
            // nếu tồn tại thì cho sửa số lượng
            if (sp != null)
            {
                sp.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        // xóa all giỏ hàng
        public ActionResult XoaGioHang()
        {
            List<GioHang> lstGioHang = LayGioHang();
            lstGioHang.Clear();
            return RedirectToAction("Index", "KingFashion");
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            //Kiểm tra đăng nhập chưa
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return Redirect("~/User/DangNhap?id=2");
            }
            // kiểm tra có hàng trong giỏ chưa
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "KingFashion");
            }
            // lấy hàng từ session
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection f)
        {
            //thêm đơn hàng
            DONDATHANG ddh = new DONDATHANG();
            KHACHHANG kh = (KHACHHANG)Session["TaiKhoan"];
            List<GioHang> lstGioHang = LayGioHang();
            var NgayGiao = String.Format("{0:MM/dd/yyyy}", f["NgayGiao"]);
            ddh.MaKH = kh.MaKH;
            ddh.NgayDat = DateTime.Now;

            ddh.NgayGiao = DateTime.Parse(NgayGiao);
            ddh.TinhTrangGiaoHang = "Xác nhận";
            ddh.HoTenNguoiNhan = f["sTenNn"];
            ddh.NoiNhan = f["sNoiNhan"];
            ddh.Sodienthoai = f["sSdtNguoiNhan"];
            db.DONDATHANGs.InsertOnSubmit(ddh);
            db.SubmitChanges();
            //thêm chi tiết đơn hàng
            foreach (var item in lstGioHang)
            {
                CHITIETDATHANG ctdh = new CHITIETDATHANG();
                ctdh.MaDonHang = ddh.MaDonHang;
                ctdh.MaSp = item.iMaSP;
                ctdh.SoLuong = item.iSoLuong;
                ctdh.ThanhTien = (decimal)item.dThanhTien;
                db.CHITIETDATHANGs.InsertOnSubmit(ctdh);

            }
            db.SubmitChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XacNhanDonHang", "GioHang");
        }
        public ActionResult XacNhanDonHang()
        {
            return View();
        }
    }
}