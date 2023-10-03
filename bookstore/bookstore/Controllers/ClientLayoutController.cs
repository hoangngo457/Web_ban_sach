using bookstore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace bookstore.Controllers
{
    public class ClientLayoutController : Controller
    {
        private Book_StoreEntities db = new Book_StoreEntities();

        public ActionResult Home()
        {
            return View(db.Books.ToList());
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
                        _user.role = "2";
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
                    if(user.role == "2") 
                    {
                        //add session
                        Session["User"] = userCheck;
                        return RedirectToAction("Home");
                    }
                    else
                    {
                        //add session
                        Session["User"] = userCheck;
                        return RedirectToAction("IndexBook", "Books");
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
    }
}