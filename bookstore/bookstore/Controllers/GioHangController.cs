using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bookstore.Models;

namespace bookstore.Controllers
{
    public class GioHangController : Controller     
    {
        Book_StoreEntities db = new Book_StoreEntities();

        public List<GioHang> LayGioHang()
        {
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang == null)
            {
                //nếu giỏ hàng chưa tồn tại thì khởi tạo 
                listGioHang = new List<GioHang>();
                Session["GioHang"] = listGioHang;
            }
            return listGioHang;
        }
        //Thêm giỏ hàng
        public ActionResult ThemGioHang(int iMaSach, string strURL)
        {
            Book sach = db.Books.SingleOrDefault(n => n.IdBook == iMaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lấy session giỏ hàng
            List<GioHang> listGioHang = LayGioHang();
            GioHang sanpham = listGioHang.Find(n => n.iMaSach == iMaSach);
            if (sanpham == null)
            {
                sanpham = new GioHang(iMaSach);
                listGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoLuong++;
                return Redirect(strURL);
            }
        }
        //Cập nhật giỏ hàng 
        public ActionResult CapNhatGioHang(int iMaSP, FormCollection f)
        {
            //Kiểm tra masp
            Book sach = db.Books.SingleOrDefault(n => n.IdBook == iMaSP);
            //Nếu get saai masp thì sẽ trả về trang lỗi 404
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy giỏ hàng ra từ session
            List<GioHang> listGioHang = LayGioHang();
            //Kiểm tra sp có tồn tại trong session giỏ hàng
            GioHang sanpham = listGioHang.SingleOrDefault(n => n.iMaSach == iMaSP);
            //Nếu tồn tại thì cho sửa
            if (sanpham != null)
            {
                sanpham.iSoLuong = int.Parse(f["txtSoLuong"].ToString());

            }
            return RedirectToAction("GioHang");
        }
        //Xóa giỏ hàng
        public ActionResult XoaGioHang(int iMaSP)
        {

            Book sach = db.Books.SingleOrDefault(n => n.IdBook == iMaSP);

            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> listGioHang = LayGioHang();
            //Kiểm tra sp có tồn tại trong session giỏ hàng
            GioHang sanpham = listGioHang.SingleOrDefault(n => n.iMaSach == iMaSP);
            //Nếu tồn tại thì cho sửa
            if (sanpham != null)
            {
                listGioHang.RemoveAll(n => n.iMaSach == iMaSP);

            }
            if (listGioHang.Count == 0)
            {
                return RedirectToAction("Home", "ClientLayout");
            }
            return RedirectToAction("GioHang");
        }
        //Xây dựng trang giỏ hàng
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("GioHangRong", "GioHang");
            }
            List<GioHang> listGioHang = LayGioHang();
            return View(listGioHang);
        }
        public ActionResult GioHangRong()
        {
            return View();
        }
        public ActionResult DatHangThanhCong()
        {
            return View();
        }
        //Tính tổng số lượng và tổng tiền
        private int TongSoLuong()
        {
            int iTongSoLuong;
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang != null)
            {
                iTongSoLuong = listGioHang.Sum(n => n.iSoLuong);
            }
            else
            {
                iTongSoLuong = 0;
            }

            return iTongSoLuong;
        }
        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang != null)
            {
                dTongTien = listGioHang.Sum(n => n.ThanhTien);
            }
            return dTongTien;
        }
       
        public ActionResult GioHangPartial()
        {
            //if (TongSoLuong() == 0)
            //{
            //    return PartialView();
            //}
            ViewBag.TongSoLuong = TongSoLuong();
            //ViewBag.TongTien = TongTien();
            return PartialView();
        }
        public ActionResult GioHangPartial1()
        {
            //if (TongSoLuong() == 0)
            //{
            //    return PartialView();
            //}
          
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        public ActionResult SuaGioHang()
        {
            if(Session["GioHang"] == null)
            {
                return RedirectToAction("Home", "ClientLayout");
            }
            List<GioHang> listGioHang = LayGioHang();
            return View(listGioHang);
        }
        //đặt hàng
        #region Đặt hàng
        [HttpPost]
        public ActionResult DatHang()
        {
            //Kiểm tra đăng nhập
            if(Session["User"]==null)
            {
                return RedirectToAction("Login", "ClientLayout");
            }
            //kiểm tra giỏ hàng
            if (Session["GioHang"] == null)
            {
                RedirectToAction("Home", "ClientLayout");
            }
            //thêm đơn hàng
            Order ddh = new Order();
            User kh = (User)Session["User"];
            List<GioHang> gh = LayGioHang();
            ddh.MaKH = kh.IdUser;
            ddh.oderDate = DateTime.Now;
            db.Orders.Add(ddh);
            db.SaveChanges();
            //chi tiết đơn hàng
            foreach(var item in gh)
            {
                OrderDetail ctgh = new OrderDetail();
                ctgh.IdOrder = ddh.IdOrder;
                ctgh.IdBook = item.iMaSach;
                ctgh.quanlity = item.iSoLuong;
                ctgh.price = (int) item.dDonGia;
                db.OrderDetails.Add(ctgh);      
            }
            db.SaveChanges();
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            listGioHang.Clear();
            return RedirectToAction("DatHangThanhCong","GioHang");
        }
        #endregion
    }

}