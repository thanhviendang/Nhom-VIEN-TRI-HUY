using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KingFashion.Models;

namespace KingFashion.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        dbKingFashionDataContext db = new dbKingFashionDataContext();
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NavadPartial()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            // gán các giá trị người dùng nhập liệu cho các biến
            var sTenDN = f["UserName"];
            var sMatKhau = f["Password"];
            // gán giá trị cho đối tượng được tạo mới(ad)
            ADMIN boss = db.ADMINs.SingleOrDefault(n => n.TenDN == sTenDN && n.MatKhau == sMatKhau);
            if (boss != null)
            {
                Session["BOSS"] = boss;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
    }
}