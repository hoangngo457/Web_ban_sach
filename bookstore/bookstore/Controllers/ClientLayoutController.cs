using bookstore.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;

namespace bookstore.Controllers
{
    public class ClientLayoutController : Controller
    {
        private Book_StoreEntities db = new Book_StoreEntities();

        public ActionResult Home()
        {
            return View(db.Books.ToList());
        }

        public PartialViewResult SachMoiPartial()
        {
            return PartialView(db.Books.OrderByDescending(n => n.IdBook).ToList());
        }

        public ActionResult Shop(int? page)
        {
            //số sản phẩm trên 1 trang
            int pageSize = 16;
            //Số trang
            int pageNumber = page ?? 1;

            return View(db.Books.ToList().OrderBy(n => n.IdBook).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        //GET: Register

        public ActionResult Register()
        {
            return View();
        }
        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = db.Users.FirstOrDefault(s => s.username == _user.username);
                if (check == null)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    if (_user.role == null)
                    {
                        _user.role = "Khách hàng";
                    }
                    db.Users.Add(_user);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.error = "Tên tài khoản này đã có người đăng kí.";
                    return View();
                }


            }
            return View();

        }

        //create a string MD5
        

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            var username = user.username;
            var password = user.password;
            if (ModelState.IsValid)
            {
                var userCheck = db.Users.SingleOrDefault(s => s.username.Equals(username) && s.password.Equals(password));
               
                if (userCheck != null)
                {
                    var role = userCheck.role;
                    if(role == "Chủ web") 
                    {
                        //add session
                        Session["User"] = userCheck;
                        return RedirectToAction("Index", "Books");
                        
                    }
                    else
                    {
                        //add session
                        Session["User"] = userCheck;
                        return RedirectToAction("Home", "ClientLayout");
                    }
                    
                }
                else
                {
                    ViewBag.LoginFailed = "Thông tin tài khoản hoặc mật khẩu không chính xác";
                    return View("Login");
                }
            }
            return View();
        }
        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Home", "ClientLayout");
        }

        public ActionResult DetailProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
    }
}