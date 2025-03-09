using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using bookstore.Models;
using PagedList;

namespace bookstore.Controllers
{
    public class BooksController : Controller
    {
        private Book_StoreEntities db = new Book_StoreEntities();

        // GET: Books
        public ActionResult Index(int? page)
        {
            //số sản phẩm trên 1 trang
            int pageSize = 10;
            //Số trang
            int pageNumber = page ?? 1;
            
            var books = db.Books.Include(b => b.Category).Include(b => b.Publisher);
            return View(db.Books.ToList().OrderBy(n => n.IdBook).ToPagedList(pageNumber, pageSize));
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
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

        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.IdCate = new SelectList(db.Categories, "IdCate", "name");
            ViewBag.IdPub = new SelectList(db.Publishers, "IdPub", "name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdBook,nameBook,description,price,image,quality,date,IdCate,IdPub")] Book book)
        {
            var name = book.nameBook;
            if (ModelState.IsValid)
            {
                var check = db.Books.SingleOrDefault(s => book.nameBook.Equals(name));
                if(check == null)
                {
                    db.Books.Add(book);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return ViewBag.error = "Sách đã tồn tại";
                }

                
            }

            ViewBag.IdCate = new SelectList(db.Categories, "IdCate", "name", book.IdCate);
            ViewBag.IdPub = new SelectList(db.Publishers, "IdPub", "name", book.IdPub);
            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.IdCate = new SelectList(db.Categories, "IdCate", "name", book.IdCate);
            ViewBag.IdPub = new SelectList(db.Publishers, "IdPub", "name", book.IdPub);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdBook,nameBook,description,price,image,quality,date,IdCate,IdPub")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCate = new SelectList(db.Categories, "IdCate", "name", book.IdCate);
            ViewBag.IdPub = new SelectList(db.Publishers, "IdPub", "name", book.IdPub); 
            return View(book);
        }

        // GET: Books/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    Book book = db.Books.SingleOrDefault(m => m.IdBook == id);

        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
            
        //    if (book == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(book);
        //}


        public JsonResult DeleteBook(int bookId)
        {
            bool result = false;
            Book b = db.Books.Where(x => x.IdBook == bookId).SingleOrDefault();
           if(b != null)
            {
                db.Books.Remove(b);
                db.SaveChanges();
                result = true;
            }
            
            

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        // POST: Books/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Book book = db.Books.SingleOrDefault(m => m.IdBook == id);
        //    if(book == null)
        //    {
        //        return HttpNotFound();

        //    }
        //    db.Books.Remove(book);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
