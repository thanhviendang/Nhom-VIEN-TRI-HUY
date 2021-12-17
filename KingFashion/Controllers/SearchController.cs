using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using KingFashion.Models;

namespace KingFashion.Controllers
{
    public class SearchController : Controller
    {
        dbKingFashionDataContext data = new dbKingFashionDataContext();
        // GET: Search
        public ActionResult SearchPartial()
        {
            return PartialView();
        }
        public ActionResult Search(string strSearch)
        {

            ViewBag.Search = strSearch;
            if (!string.IsNullOrEmpty(strSearch))
            {
                //  var kq = from s in data.SACHes where s.TenSach.Contains(strSearch) orderby  (s.SoLuongBan)descending  select s;
                // var kq = data.SACHes.Where(s => s.MaCD == int.Parse(strSearch));
                // var kq = from s in data.SACHes where s == int.Parse(strSearch) select s;
                var kq = from s in data.SANPHAMs where s.TenSP.Contains(strSearch) || s.MoTa.Contains(strSearch) select s;
                //  var kq = from s in data.SACHes where s.SoLuongBan >= 5 && s.SoLuongBan <= 10 orderby (s.SoLuongBan) descending select s;
                //  var kq = data.SACHes.Where(s => s.MaCD == int.Parse(strSearch)).OrderByDescending(s=>s.SoLuongBan) ;
                //var kq = data.SANPHAMs.Where(s => s.TenSP == strSearch).OrderBy(s => s.SoLuongBan).ToList();
                return View(kq.ToList());
            }
            return View();
        }
        public ActionResult TimKiemPartial()
        {
            return PartialView();
        }
        public ActionResult TimKiem(string search)
        {

            ViewBag.Search = search;
            if (!string.IsNullOrEmpty(search))
            {
                //  var kq = from s in data.SACHes where s.TenSach.Contains(strSearch) orderby  (s.SoLuongBan)descending  select s;
                // var kq = data.SACHes.Where(s => s.MaCD == int.Parse(strSearch));
                // var kq = from s in data.SACHes where s == int.Parse(strSearch) select s;
                var kq = from s in data.SANPHAMs where s.TenSP.Contains(search) || s.MoTa.Contains(search) select s;
                var helps = data.DONDATHANGs.Count();
                //  var kq = from s in data.SACHes where s.SoLuongBan >= 5 && s.SoLuongBan <= 10 orderby (s.SoLuongBan) descending select s;
                //  var kq = data.SACHes.Where(s => s.MaCD == int.Parse(strSearch)).OrderByDescending(s=>s.SoLuongBan) ;
                //var kq = data.SANPHAMs.Where(s => s.TenSP == strSearch).OrderBy(s => s.SoLuongBan).ToList();
                return View(kq.ToList());
            }
            return View();
        }
        public ActionResult Group()
        {
            var kq = data.SANPHAMs.GroupBy(s => s.MaCD);
            return View(kq);
        }

        
    }
}