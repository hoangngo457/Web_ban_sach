using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bookstore.Models
{
    public class GioHang
    {
        Book_StoreEntities db = new Book_StoreEntities();
        public int iMaSach { get; set; }
        public string sTenSach { get; set; }
        public string sAnhBia { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double ThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }
       
        //Hàm tạo cho giỏ hàng
        public GioHang(int MaSach)
        {
            iMaSach = MaSach;
            Book sach = db.Books.Single(n => n.IdBook == iMaSach);
            sTenSach = sach.nameBook;
            sAnhBia = sach.image;
            dDonGia = double.Parse(sach.price.ToString());
            iSoLuong = 1;
        }
    }
}