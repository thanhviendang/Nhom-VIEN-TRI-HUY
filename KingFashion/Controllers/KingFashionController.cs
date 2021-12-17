using KingFashion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace KingFashion.Controllers
{
    public class KingFashionController : Controller
    {
        dbKingFashionDataContext data = new dbKingFashionDataContext();
        
        /// <summary>
        /// LaySachMoi
        /// </summary>
        /// <param name="count">int</param>
        /// <returns>List</returns>
        private List<SANPHAM> LaySanPhamMoi(int count)
        {
            return data.SANPHAMs.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }
        private List<SANPHAM> LaySanPhamHot(int count)
        {
            /*var tienvon = data.CHITIETDATHANGs.Where(a => a.MaSp == a.SANPHAM.MaSP).Sum(a=> a.SoLuong);
*/
            return data.SANPHAMs.OrderByDescending(a => a.CHITIETDATHANGs.Sum(c=>c.SoLuong) ).Take(count).ToList();
        }
        // GET: TravelShop
        public ActionResult Index(int? page)
        {
            //lay 6 sach moi
            var listSPMoi = LaySanPhamMoi(100);
            // return View(listSachMoi);

            // tạo biến quy định số sản phẩm trên mỗi trang
            int iSize = 9;
            //Tạo biến số trang
            int iPageNum = (page ?? 1);

            return View(listSPMoi.ToPagedList(iPageNum, iSize));

        }
       
        public ActionResult NavPartial()
        {
            return PartialView();
        }
        public ActionResult SliderPartial()
        {
            var listSph = LaySanPhamHot(5);
            return PartialView(listSph);
        }
        public ActionResult slidePartial()
        {
            return PartialView();
        }
        public ActionResult SanPhamTheoChuDe(int id, int? page, string n)
        {
            ViewBag.MaCD = id;
            var tencd = from s in data.CHUDEs
                     where s.TenChuDe == n
                     select s;
            ViewBag.TenChuDe = tencd;
            // tạo biến quy định số sản phẩm trên mỗi trang
            int iSize = 9;
            //Tạo biến số trang
            int iPageNum = (page ?? 1);
            var sp = from s in data.SANPHAMs
                       where s.MaCD == id
                       select s;
            
            return View(sp.ToPagedList(iPageNum, iSize));
        }
        public ActionResult SanPhamTheoNSX(int id, int? page)
        {
            ViewBag.MaNSX = id;
            // tạo biến quy định số sản phẩm trên mỗi trang
            int iSize = 9;
            //Tạo biến số trang
            int iPageNum = (page ?? 1);
            var sp = from s in data.SANPHAMs
                       where s.MaNSX == id
                       select s;
            return View(sp.ToPagedList(iPageNum, iSize));
        }

        public ActionResult ChiTietSanPham(int id)
        {
            var sp = from s in data.SANPHAMs where s.MaSP == id select s;
            return PartialView(sp.Single());
        }

        public ActionResult ChuDePartial()
        {
            var listChuDe = from cd in data.CHUDEs select cd;
            return PartialView(listChuDe);
        }
        public ActionResult NhaSanXuatPartial()
        {
            var listNSX = from nxb in data.NHASANXUATs select nxb;
            return PartialView(listNSX);
        }


        public ActionResult LoginLogout()
        {
            return PartialView("LoginLogout");
        }

    }
}
