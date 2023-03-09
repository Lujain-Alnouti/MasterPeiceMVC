using MasterPeicefinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MasterPeicefinal.Controllers
{
    public class OrderDetailsController : Controller
    {
        MasterPeiceEntities db = new MasterPeiceEntities();

        // GET: OrderDetails
        public ActionResult ProgressOrderDetails(int? id)
        {
            ViewBag.orderid = id;
            
            return View(db.OrderDetails.Where(a=>a.OrderID==id).ToList());
        }
        public ActionResult DoneOrderDetails(int? id)
        {
            ViewBag.orderid = id;

            return View(db.OrderDetails.Where(a => a.OrderID == id).ToList());
        }

        public ActionResult Rating(int? id)
        {
            
            return View(db.Products.Where(a => a.ProductID == id).ToList());
        }
    }
}