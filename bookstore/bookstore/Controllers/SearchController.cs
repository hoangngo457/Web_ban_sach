using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bookstore.Models;
using PagedList.Mvc;
using PagedList;

namespace bookstore.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        Book_StoreEntities db = new Book_StoreEntities();

        [HttpPost]
        public ActionResult Result(FormCollection f, int? page)
        {
            string text = f["tuKhoa"].ToString();
            ViewBag.TuKhoa = text;
            List<Book> kq = db.Books.Where(n => n.nameBook.Contains(text)).ToList();

            //Phân trang
            int pageNumber = page ?? 1;
            int pageSize = 10;

            if (kq.Count == 0 ) {
                ViewBag.Thongbao = "Không thể tìm thấy sản phẩm nào phù hợp.";
                return View(db.Books.OrderBy(n => n.IdBook).ToPagedList(pageNumber, pageSize));
            }
            else { 
                ViewBag.Thongbao = "Đã tìm thấy " + kq.Count + " kết quả phù hợp.";
                return View(kq.OrderBy(n => n.IdBook).ToPagedList(pageNumber, pageSize));
            }
        }


        [HttpGet]
        public ActionResult Result(int? page, String tuKhoa )
        {
            ViewBag.TuKhoa = tuKhoa;
            List<Book> kq = db.Books.Where(n => n.nameBook.Contains(tuKhoa)).ToList();

            //Phân trang
            int pageNumber = page ?? 1;
            int pageSize = 10;

            if (kq.Count == 0)
            {
                ViewBag.Thongbao = "Không thể tìm thấy sản phẩm nào phù hợp.";
                return View();
            }

            ViewBag.Thongbao = "Đã tìm thấy " + kq.Count + " kết quả phù hợp.";
            return View(kq.OrderBy(n => n.nameBook).ToPagedList(pageNumber, pageSize));
        }


        [HttpPost]
        public ActionResult SearchResult(FormCollection f, int? page)
        {
            string text = f["tuKhoa"].ToString();
            ViewBag.TuKhoa = text;
            List<Book> kq = db.Books.Where(n => n.nameBook.Contains(text)).ToList();

            //Phân trang
            int pageNumber = page ?? 1;
            int pageSize = 16;

            if (kq.Count == 0)
            {
                ViewBag.Thongbao = "Không thể tìm thấy sản phẩm nào phù hợp.";
                return ViewBag.Thongbao;
            }
            else
            {
                ViewBag.Thongbao = "Đã tìm thấy " + kq.Count + " kết quả phù hợp.";
                return View(kq.OrderBy(n => n.nameBook).ToPagedList(pageNumber, pageSize));
            }
        }


        [HttpGet]
        public ActionResult SearchResult(int? page, String tuKhoa)
        {
            ViewBag.TuKhoa = tuKhoa;
            List<Book> kq = db.Books.Where(n => n.nameBook.Contains(tuKhoa)).ToList();

            //Phân trang
            int pageNumber = page ?? 1;
            int pageSize = 16;

            if (kq.Count == 0)
            {
                ViewBag.Thongbao = "Không thể tìm thấy sản phẩm nào phù hợp.";
                return ViewBag.Thongbao;
            }

            ViewBag.Thongbao = "Đã tìm thấy " + kq.Count + " kết quả phù hợp.";
            return View(kq.OrderBy(n => n.nameBook).ToPagedList(pageNumber, pageSize));
        }
    }
}