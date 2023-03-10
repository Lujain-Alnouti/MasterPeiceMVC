using MasterPeicefinal.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MasterPeicefinal.Controllers
{
    public class HomeController : Controller
    {
        MasterPeiceEntities db = new MasterPeiceEntities();

        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult UserProfile()
        {
            var user = User.Identity.GetUserId();
            var clientID = db.Clients.FirstOrDefault(c => c.AspUserID == user).ClientID;
            ViewBag.ProgressOrder = db.MainOrders.Where(a => a.OrderStatus == false && a.UserID==clientID).ToList();
            ViewBag.DoneOrder = db.MainOrders.Where(a => a.OrderStatus == true && a.UserID == clientID).ToList();
            


            return View(db.Clients.Where(a=>a.AspUserID==user).ToList());
        }


        public ActionResult EditProfile()
        {
            var user = User.Identity.GetUserId();
            var clientID = db.Clients.FirstOrDefault(c => c.AspUserID == user).ClientID;
            return View(db.Clients.Where(a => a.AspUserID == user).ToList());
        }



    }
}