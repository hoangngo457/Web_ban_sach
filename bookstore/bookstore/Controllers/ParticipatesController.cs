using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using bookstore.Models;

namespace bookstore.Controllers
{
    public class ParticipatesController : Controller
    {
        private Book_StoreEntities db = new Book_StoreEntities();

        // GET: Participates
        public ActionResult Index()
        {
            var participates = db.Participates.Include(p => p.Author).Include(p => p.Book);
            return View(participates.ToList());
        }

        // GET: Participates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Participate participate = db.Participates.Find(id);
            if (participate == null)
            {
                return HttpNotFound();
            }
            return View(participate);
        }

        // GET: Participates/Create
        public ActionResult Create()
        {
            ViewBag.IdAu = new SelectList(db.Authors, "IdAu", "name");
            ViewBag.IdBook = new SelectList(db.Books, "IdBook", "nameBook");
            return View();
        }

        // POST: Participates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAu,IdBook,role,location")] Participate participate)
        {
            if (ModelState.IsValid)
            {
                db.Participates.Add(participate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAu = new SelectList(db.Authors, "IdAu", "name", participate.IdAu);
            ViewBag.IdBook = new SelectList(db.Books, "IdBook", "nameBook", participate.IdBook);
            return View(participate);
        }

        // GET: Participates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Participate participate = db.Participates.Find(id);
            if (participate == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAu = new SelectList(db.Authors, "IdAu", "name", participate.IdAu);
            ViewBag.IdBook = new SelectList(db.Books, "IdBook", "nameBook", participate.IdBook);
            return View(participate);
        }

        // POST: Participates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAu,IdBook,role,location")] Participate participate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(participate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAu = new SelectList(db.Authors, "IdAu", "name", participate.IdAu);
            ViewBag.IdBook = new SelectList(db.Books, "IdBook", "nameBook", participate.IdBook);
            return View(participate);
        }

        // GET: Participates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Participate participate = db.Participates.Find(id);
            if (participate == null)
            {
                return HttpNotFound();
            }
            return View(participate);
        }

        // POST: Participates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Participate participate = db.Participates.Find(id);
            db.Participates.Remove(participate);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
