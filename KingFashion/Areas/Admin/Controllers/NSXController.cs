using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KingFashion.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace KingFashion.Areas.Admin.Controllers
{
    public class NSXController : Controller
    {
        dbKingFashionDataContext db = new dbKingFashionDataContext();
        // GET: Admin/nsx

        public ActionResult Index()
        {           
            return View(db.NHASANXUATs.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(NHASANXUAT nsx, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                nsx.TenNSX = f["sTenNSX"];
                nsx.DiaChi = f["sDiaChi"];
                nsx.DienThoai = f["sDienThoai"];

                db.NHASANXUATs.InsertOnSubmit(nsx);
                db.SubmitChanges();
                //Về lại trang Quản lý cd
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var nsx = db.NHASANXUATs.SingleOrDefault(n => n.MaNsx == id);
            if (nsx == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(nsx);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {
            var nsx = db.NHASANXUATs.SingleOrDefault(n => n.MaNsx == int.Parse(f["iMaNsx"]));

            if (ModelState.IsValid)
            {
                
                nsx.TenNSX = f["sTenNSX"];
                nsx.DiaChi = f["sDiaChi"];
                nsx.DienThoai = f["sDienThoai"];
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(nsx);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var sp = db.NHASANXUATs.SingleOrDefault(n => n.MaNsx == id);
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
            var sp = db.NHASANXUATs.SingleOrDefault(n => n.MaNsx == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var cdsp = db.SANPHAMs.Where(c => c.MaNSX == id);
            if (cdsp.Count() > 0)
            {
                // nội dung sẽ hiển thị khi .. cần xóa  đã có trong table

                ViewBag.ThongBao = "Đang có sản phẩm thuộc trường này<br>" +
                    "Nếu muốn xóa thì phải xóa hết sản phẩm thuộc nhà sản xuất này";
                return View(sp);
            }


            db.NHASANXUATs.DeleteOnSubmit(sp);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}