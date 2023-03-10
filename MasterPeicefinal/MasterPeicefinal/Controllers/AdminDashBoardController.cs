using MasterPeicefinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MasterPeicefinal.Controllers
{
    public class AdminDashBoardController : Controller
    {
         MasterPeiceEntities db = new MasterPeiceEntities();

        // GET: AdminDashBoard
        public ActionResult Users()
        {
            ViewBag.userCount = db.Clients.Count();
            return View(db.Clients.ToList());
        }
        public ActionResult Userinfo(string id)
        {
            int clientid=db.Clients.FirstOrDefault(a=>a.AspUserID==id).ClientID;
            ViewBag.DoneOrder=db.MainOrders.Where(a=>a.UserID==clientid & a.OrderStatus==true).ToList();
            ViewBag.ProgressOrder = db.MainOrders.Where(a => a.UserID == clientid & a.OrderStatus == false).ToList();
            return View(db.Clients.FirstOrDefault(a=>a.AspUserID==id));
        }

        public ActionResult SwitchOrderStatus(int? id)
        {
            MainOrder mains = db.MainOrders.Find(id);
            mains.OrderStatus = true;
            db.SaveChanges();
            string asp = db.MainOrders.FirstOrDefault(a => a.OrderID == id).Client.AspUserID;
            return RedirectToAction("Userinfo","AdminDashBoard", new { id = asp });
        }

        public ActionResult AdminDoneOrder()
        {
            ViewBag.aDoneOrder = db.MainOrders.Where(a=>a.OrderStatus == true).Count();
            ViewBag.aProgressOrder = db.MainOrders.Where(a=>a.OrderStatus == false).Count();
            return View(db.MainOrders.Where(a=>a.OrderStatus == true).ToList());
        }

        public ActionResult AdminProgressOrder()
        {
            ViewBag.aDoneOrder = db.MainOrders.Where(a => a.OrderStatus == true).Count();
            ViewBag.aProgressOrder = db.MainOrders.Where(a => a.OrderStatus == false).Count();
            return View(db.MainOrders.Where(a => a.OrderStatus == false).ToList());
        }
        public ActionResult AdminRating()
        {
            ViewBag.RateAll = db.Ratings.Count();
            ViewBag.Rate5 = db.Ratings.Where(a => a.Rate == "5").Count();
            ViewBag.Rate4 = db.Ratings.Where(a => a.Rate == "4").Count();
            ViewBag.Rate3 = db.Ratings.Where(a => a.Rate == "3").Count();
            ViewBag.Rate2 = db.Ratings.Where(a => a.Rate == "2").Count();
            ViewBag.Rate1 = db.Ratings.Where(a => a.Rate == "1").Count();
            return View(db.Ratings.ToList());
        }

        public ActionResult Rating5()
        {
            ViewBag.RateAll = db.Ratings.Count();
            ViewBag.Rate5 = db.Ratings.Where(a => a.Rate == "5").Count();
            ViewBag.Rate4 = db.Ratings.Where(a => a.Rate == "4").Count();
            ViewBag.Rate3 = db.Ratings.Where(a => a.Rate == "3").Count();
            ViewBag.Rate2 = db.Ratings.Where(a => a.Rate == "2").Count();
            ViewBag.Rate1 = db.Ratings.Where(a => a.Rate == "1").Count();
            return View("AdminRating", db.Ratings.Where(a => a.Rate == "5").ToList());
        }

        public ActionResult Rating4()
        {
            ViewBag.RateAll = db.Ratings.Count();
            ViewBag.Rate5 = db.Ratings.Where(a => a.Rate == "5").Count();
            ViewBag.Rate4 = db.Ratings.Where(a => a.Rate == "4").Count();
            ViewBag.Rate3 = db.Ratings.Where(a => a.Rate == "3").Count();
            ViewBag.Rate2 = db.Ratings.Where(a => a.Rate == "2").Count();
            ViewBag.Rate1 = db.Ratings.Where(a => a.Rate == "1").Count();
            return View("AdminRating", db.Ratings.Where(a => a.Rate == "4").ToList());
        }

        public ActionResult Rating3()
        {
            ViewBag.RateAll = db.Ratings.Count();
            ViewBag.Rate5 = db.Ratings.Where(a => a.Rate == "5").Count();
            ViewBag.Rate4 = db.Ratings.Where(a => a.Rate == "4").Count();
            ViewBag.Rate3 = db.Ratings.Where(a => a.Rate == "3").Count();
            ViewBag.Rate2 = db.Ratings.Where(a => a.Rate == "2").Count();
            ViewBag.Rate1 = db.Ratings.Where(a => a.Rate == "1").Count();
            return View("AdminRating", db.Ratings.Where(a => a.Rate == "3").ToList());
        }

        public ActionResult Rating2()
        {
            ViewBag.RateAll = db.Ratings.Count();
            ViewBag.Rate5 = db.Ratings.Where(a => a.Rate == "5").Count();
            ViewBag.Rate4 = db.Ratings.Where(a => a.Rate == "4").Count();
            ViewBag.Rate3 = db.Ratings.Where(a => a.Rate == "3").Count();
            ViewBag.Rate2 = db.Ratings.Where(a => a.Rate == "2").Count();
            ViewBag.Rate1 = db.Ratings.Where(a => a.Rate == "1").Count();
            return View("AdminRating", db.Ratings.Where(a => a.Rate == "2").ToList());
        }

        public ActionResult Rating1()
        {
            ViewBag.RateAll = db.Ratings.Count();
            ViewBag.Rate5 = db.Ratings.Where(a => a.Rate == "5").Count();
            ViewBag.Rate4 = db.Ratings.Where(a => a.Rate == "4").Count();
            ViewBag.Rate3 = db.Ratings.Where(a => a.Rate == "3").Count();
            ViewBag.Rate2 = db.Ratings.Where(a => a.Rate == "2").Count();
            ViewBag.Rate1 = db.Ratings.Where(a => a.Rate == "1").Count();
            return View("AdminRating", db.Ratings.Where(a => a.Rate == "1").ToList());
        }

    }
}