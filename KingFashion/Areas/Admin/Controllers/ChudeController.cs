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
    public class ChudeController : Controller
    {
        dbKingFashionDataContext db = new dbKingFashionDataContext();
        // GET: Admin/SanPham

      
        public ActionResult Index()
        {
            
           
            return View(db.CHUDEs.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(CHUDE cd, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                cd.TenChuDe = f["sTenCD"];

                db.CHUDEs.InsertOnSubmit(cd);
                db.SubmitChanges();
                //Về lại trang Quản lý cd
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var cd = db.CHUDEs.SingleOrDefault(n => n.MaCd == id);
            if (cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
           
            return View(cd);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {
            var cd = db.CHUDEs.SingleOrDefault(n => n.MaCd == int.Parse(f["iMaCD"]));
           
            if (ModelState.IsValid)
            {
                cd.TenChuDe = f["sTenCD"];
               
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(cd);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var sp = db.CHUDEs.SingleOrDefault(n => n.MaCd == id);
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
            var sp = db.CHUDEs.SingleOrDefault(n => n.MaCd == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var cdsp = db.SANPHAMs.Where(c => c.MaCD == id);
            if (cdsp.Count() > 0)
            {
                // nội dung sẽ hiển thị khi .. cần xóa  đã có trong table

                ViewBag.ThongBao = "Đang có sản phẩm trong chủ đề này<br>" +
                    "Nếu muốn xóa thì phải xóa hết sản phẩm có trong chủ đề";
                return View(sp);
            }


            db.CHUDEs.DeleteOnSubmit(sp);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}