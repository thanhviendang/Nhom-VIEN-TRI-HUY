using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KingFashion.Models;

namespace KingFashion.Controllers
{
    public class UserChiTietDonHangController : Controller
    {
        dbKingFashionDataContext db = new dbKingFashionDataContext();
        // GET: UserChiTietDonHang
      
        public ActionResult Index(int id)
        {
            var sp = from s in db.CHITIETDATHANGs
                     where s.MaDonHang == id
                     select s;
            
            return PartialView(sp.ToList());
        }
    }
}