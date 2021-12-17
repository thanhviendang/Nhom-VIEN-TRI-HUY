using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KingFashion.Models;

namespace KingFashion.Areas.Admin.Controllers
{
    public class KhachHangController : Controller
    {
        dbKingFashionDataContext db = new dbKingFashionDataContext();
        // GET: Admin/KhachHang
        public ActionResult Index()
        {
            return View(db.KHACHHANGs.ToList());
        }
        public ActionResult Details(int id)
        {
            var kh = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var kh = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
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
            var kh = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var ddh = db.DONDATHANGs.Where(ct => ct.MaKH == id);
            if (ddh.Count() > 0)
            {
                // nội dung sẽ hiển thị khi sách cần xóa  đã có trong table
                // // chi tiết đơn hàng
                ViewBag.ThongBao = "Khách hàng này này đang có trong bảng Đơn đặt hàng \n" + 
                    "Nếu muốn xóa thì phải xóa hết mã Khách hàng này trong bảng Đơn đặt hàng";
                return View(kh);
            }

            // xóa sách
            db.KHACHHANGs.DeleteOnSubmit(kh);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}