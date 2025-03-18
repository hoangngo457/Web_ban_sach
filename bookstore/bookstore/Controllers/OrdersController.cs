using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using bookstore.Models;
using PagedList;

namespace bookstore.Controllers
{
    public class OrdersController : Controller
    {
        private Book_StoreEntities db = new Book_StoreEntities();

        // GET: Orders
       

        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            
            int pageNumber = page ?? 1;

            var orders = db.Orders.Include(o => o.User);
            return View(db.Orders.ToList().OrderBy(n => n.IdOrder).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult OrderDetail(int? id) {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var detail = db.OrderDetails.Where(x => x.IdOrder == id).ToList();
            if (detail == null)
            {
                return HttpNotFound();
            }
            return View(detail);
        }

        [HttpPost]
        public ActionResult ConfirmOrder(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                order.status = "Đã xác nhận";
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }
    }
}
