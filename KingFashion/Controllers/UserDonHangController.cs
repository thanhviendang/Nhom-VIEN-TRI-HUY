using KingFashion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KingFashion.Controllers
{
    public class UserDonHangController : Controller
    {
        dbKingFashionDataContext db = new dbKingFashionDataContext();
        // GET: UserDonHang
        
        public ActionResult Index(int id)
        {
            var sp = from s in db.DONDATHANGs
                     where s.MaKH == id
                     select s;
            return PartialView(sp.ToList());
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

    }
}