using bookstore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace bookstore.Controllers
{
    public class RevenueStatisticsController : Controller
    {
        private Book_StoreEntities db = new Book_StoreEntities();
        // GET: RevenueStatistics
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetStatistical(string fromDate, string endDate)
        {
            var query = from o in db.Orders
                        join od in db.OrderDetails
                        on o.IdOrder equals od.IdOrder
                        join p in db.Books
                        on od.IdBook equals p.IdBook
                        select new
                        {
                            OrderDate = o.oderDate,
                            quanlity = od.quanlity,
                            price = od.price
                        };
            if (!string.IsNullOrEmpty(fromDate))
            {
                DateTime startDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                query = query.Where(x => x.OrderDate == startDate);
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                DateTime toDate = DateTime.ParseExact(endDate, "dd/MM/yyyy", null);
                query = query.Where(x => x.OrderDate == toDate);
            }
           
            var result = query.GroupBy(x => DbFunctions.TruncateTime(x.OrderDate)).Select(x => new
            {
                Date = x.Key.Value,
                totalPrice = x.Sum(y => y.quanlity * y.price)
            }).Select(x => new
            {
                Date = x.Date,
                DoanhThu = x.totalPrice
            });

            return Json(new { Data = result }, JsonRequestBehavior.AllowGet);
        }
    }
}