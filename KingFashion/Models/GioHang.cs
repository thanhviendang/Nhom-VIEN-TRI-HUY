using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KingFashion.Models;
namespace KingFashion.Models
{
    public class GioHang
    {
        dbKingFashionDataContext db = new dbKingFashionDataContext();
        public int iMaSP { get; set; }
        public string sTenSP { get; set; }
        public string sAnhBia { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }
        ///Khởi tạo giỏ hàng theo Masp được truyền vào với SoLuong mặc định là 1
        public GioHang(int ms)
        {
            iMaSP = ms;
            SANPHAM s = db.SANPHAMs.Single(n => n.MaSP == iMaSP);
            sTenSP = s.TenSP;
            sAnhBia = s.AnhBia;
            dDonGia = double.Parse(s.GiaBan.ToString());
            iSoLuong = 1;
        }
    }
}