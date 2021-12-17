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
    public class SanPhamController : Controller
    {
        dbKingFashionDataContext db = new dbKingFashionDataContext();
        // GET: Admin/SanPham
        
        public ActionResult Index(int? page)
        {
            
            int iPageNum = (page ?? 1);
            int ipageSize = 15;
            return View(db.SANPHAMs.ToList().ToPagedList(iPageNum, ipageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            // lấy danh sách từ các table Chude, nhaxuatban, hien thi ten,khi chon sẽ lấy mã
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe");
            ViewBag.MaNSX = new SelectList(db.NHASANXUATs.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            return View();
        }
        
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(SANPHAM sp, FormCollection f, HttpPostedFileBase fFileUpload)
        {
            //Đưa dữ liệu vào DropDown
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe");
            ViewBag.MaNSX = new SelectList(db.NHASANXUATs.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            if (fFileUpload == null)
            {
                //Nội dung thông báo yêu cầu chọn ảnh bìa
                ViewBag.ThongBao = "Hãy chọn ảnh bìa.";
                //Lưu thông tin để khi load lại trang do yêu cầu chọn ảnh bìa sẽ hiển thị các thông tin này lên trang
                ViewBag.TenSP = f["sTenSP"];
                ViewBag.MoTa = f["sMoTa"];
                ViewBag.SoLuong = int.Parse(f["iSoLuong"]);
                ViewBag.GiaBan = decimal.Parse(f["mGiaBan"]);
                ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe", int.Parse(f["MaCD"]));
                ViewBag.MaNXB = new SelectList(db.NHASANXUATs.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX", int.Parse(f["MaNSX"]));
                ViewBag.GiaGoc = decimal.Parse(f["mGiaGoc"]);
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                //Lấy tên file (Khai báo thư viện: System.IO)
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                    //Lấy đường dẫn lưu file
                    var path = Path.Combine(Server.MapPath("~/Images"), sFileName);
                    //Kiểm tra ảnh bìa đã tồn tại chưa để lưu lên thư mục
                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }
                    //Lưu Sach vào CSDL
                    sp.TenSP = f["sTenSP"];
                    sp.MoTa = f["sMoTa"].Replace("<p>", "").Replace("</p>", "\n");
                    sp.AnhBia = sFileName;
                    sp.NgayCapNhat = Convert.ToDateTime(f["dNgayCapNhat"]);
                    sp.SoLuongBan = int.Parse(f["iSoLuong"]);
                    sp.GiaBan = decimal.Parse(f["mGiaBan"]);
                    sp.MaCD = int.Parse(f["MaCD"]);
                    sp.MaNSX = int.Parse(f["MaNSX"]);
                    sp.Giagoc = decimal.Parse(f["mGiaGoc"]);
                    db.SANPHAMs.InsertOnSubmit(sp);
                    db.SubmitChanges();
                    //Về lại trang Quản lý sp
                    return RedirectToAction("Index");
                }
                return View();
            }
        }
        public ActionResult Details(int id)
        {
            var sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }

        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // hiển thị danh sách chủ đề và nhà xuất bản đồng thời chọn chủ đề và 
            // nhà xuất bản của cuốn hiện tại
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe", sp.MaCD);
            ViewBag.MaNSX = new SelectList(db.NHASANXUATs.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX", sp.MaNSX);
            return View(sp);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f, HttpPostedFileBase fFileUpload)
        {
            var sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == int.Parse(f["iMaSP"]));
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe),
                "MaCD", "TenChuDe", sp.MaCD);
            ViewBag.MaNSX = new SelectList(db.NHASANXUATs.ToList().OrderBy(n => n.TenNSX), "" +
                "MaNSX", "TenNSX", sp.MaNSX);
            if (ModelState.IsValid)
            {
                if (fFileUpload != null)
                // kiểm tra để xác nhận cho thay đổi ảnh bìa
                {
                    // lấy tên file(khai báo thư viện: system.io)
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                    // lấy đường dẫn lưu file
                    var path = Path.Combine(Server.MapPath("~/Images"), sFileName);
                    // kiểm tra file đã tồn tại chưa
                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }
                    sp.AnhBia = sFileName;
                }
                // lưu sách vào csdl
                sp.TenSP = f["sTenSP"];
                sp.GiaBan = decimal.Parse(f["mGiaBan"]);
                sp.MoTa = f["sMoTa"].Replace("<p>", "").Replace("<p>", "\n");
                sp.NgayCapNhat = Convert.ToDateTime(f["dNgayCapNhat"]);
                sp.SoLuongBan = int.Parse(f["iSoLuong"]);
                
                sp.MaCD = int.Parse(f["MaCD"]);
                sp.MaNSX = int.Parse(f["MaNSX"]);
                sp.Giagoc = decimal.Parse(f["mGiaGoc"]);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(sp);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var cthd = db.CHITIETDATHANGs.Where(ct => ct.MaSp == id);
            if (cthd.Count() > 0)
            {
                // nội dung sẽ hiển thị khi sách cần xóa  đã có trong table

                ViewBag.ThongBao = "Sản phẩm này đang có trong bảng Chi tiết đặt hàng<br>" +
                    "Nếu muốn xóa thì phải xóa hết mã sách này trong bảng CHi tiết đặt hàng";
                return View(sp);
            }


            // xóa sp
            db.SANPHAMs.DeleteOnSubmit(sp);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
       
    }
}