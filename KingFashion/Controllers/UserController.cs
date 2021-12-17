using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KingFashion.Models;

namespace KingFashion.Controllers
{
    public class UserController : Controller
    {
        dbKingFashionDataContext data = new dbKingFashionDataContext();
        // GET: User
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KHACHHANG kh)
        {
            //Gan cac gia tri nguoi dung nhap du lieu cho cac bien
            var sHoTen = collection["HoTen"];
            var sTaiKhoan = collection["TaiKhoan"];
            var sMatKhau = collection["MatKhau"];
            var sMatKhauNhapLai = collection["MatKhauNL"];
            var sDiaChi = collection["DiaChi"];
            var sEmail = collection["Email"];
            var sDienThoai = collection["DienThoai"];
            var dNgaySinh = String.Format("{0:MM/dd/yyyy}", collection["NgaySinh"]);
            
            if (String.IsNullOrEmpty(sHoTen))
            {
                ViewData["err1"] = "Họ tên không được rỗng";
                ViewData["err8"] = "✖";
            }
            else if (String.IsNullOrEmpty(sTaiKhoan))
            {
                ViewData["err2"] = "Tên đăng nhập không được rỗng";
                ViewData["err9"] = "✖";
            }
            else if (String.IsNullOrEmpty(sMatKhau))
            {
                ViewData["err3"] = "Phải nhập mật khẩu";
                ViewData["err10"] = "✖";
            }
            else if (String.IsNullOrEmpty(sMatKhauNhapLai))
            {
                ViewData["err4"] = "Phải nhập lại mật khẩu";
                ViewData["err11"] = "✖";
            }
            else if (sMatKhau != sMatKhauNhapLai)
            {
                ViewData["err5"] = "Mật khẩu nhập lại không khớp";
                ViewData["err11"] = "✖";
            }
            else if (String.IsNullOrEmpty(sEmail))
            {
                ViewData["err6"] = "Email không được rỗng";
                ViewData["err12"] = "✖";
            }
            else if (String.IsNullOrEmpty(sDienThoai))
            {
                ViewData["err7"] = "Số điện thoại không được rỗng";
                ViewData["err13"] = "✖";
            }

            else if (data.KHACHHANGs.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan) != null)
            {
                ViewBag.ThongBao = "Tên đăng nhập đã tồn tại";
                ViewData["err9"] = "✖";
            }
            else if (data.KHACHHANGs.SingleOrDefault(n => n.Email == sEmail) != null)
            {
                ViewBag.ThongBao = "Email đã được sử dụng";
                ViewData["err12"] = "✖";
            }
            else
            {
                //Gán giá trị cho đối tượng được tạo mới (kh)
                kh.HoTen = sHoTen;
                kh.TaiKhoan = sTaiKhoan;
                kh.MatKhau = sMatKhau;
                kh.Email = sEmail;
                kh.DiaChi = sDiaChi;
                kh.DienThoai = sDienThoai;
                kh.NgaySinh = DateTime.Parse(dNgaySinh);
                data.KHACHHANGs.InsertOnSubmit(kh);
                data.SubmitChanges();
                return Redirect("~/User/DangNhap?id=1");
            }
            return this.DangKy();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {

            var sTaiKhoan = collection["TaiKhoan"];
            var sMatKhau = collection["MatKhau"];
            int state = int.Parse(Request.QueryString["id"]);
            if (String.IsNullOrEmpty(sTaiKhoan))
            {
                ViewData["Err1"] = "Bạn chưa nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(sMatKhau))
            {
                ViewData["Err2"] = "Phải nhập mật khẩu";
            }
            else
            {
                KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan && n.MatKhau == sMatKhau);
                if (kh != null)
                {
                    Session["TaiKhoan"] = kh;
                    if (state == 1)
                    {
                        return RedirectToAction("Index", "KingFashion");
                    }
                    else
                    {
                        return RedirectToAction("DatHang", "GioHang");
                    }
                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
                ADMIN ad = data.ADMINs.SingleOrDefault(n => n.TenDN == sTaiKhoan && n.MatKhau == sMatKhau);
                if (ad != null)
                {
                    
                    return RedirectToAction("Index", "Admin/Home");

                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult DangXuat()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult DangXuat(FormCollection collection)
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("Index", "KingFashion");
        }
        public ActionResult DetailsUser(int id)
        {
            var kh = data.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }
        [HttpGet]
        public ActionResult EditUser(int id)
        {
            var kh = data.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditUser(FormCollection f)
        {
            var kh = data.KHACHHANGs.SingleOrDefault(n => n.MaKH == int.Parse(f["iMaKH"]));
            
            if (ModelState.IsValid)
            {

                // lưu kh vào csdl
                kh.HoTen = f["sHoTen"];
                kh.TaiKhoan = f["sTaiKhoan"];
                kh.MatKhau = f["sMatKhau"];
                kh.Email = f["sEmail"];
                kh.DiaChi = f["sDiaChi"];
                kh.DienThoai = f["sDienThoai"];
                kh.NgaySinh = Convert.ToDateTime(f["dNgaySinh"]);
                data.SubmitChanges();
                return RedirectToAction("DetailsUser", new { id = kh.MaKH });
            }
            return View(kh);
        }
    }
}