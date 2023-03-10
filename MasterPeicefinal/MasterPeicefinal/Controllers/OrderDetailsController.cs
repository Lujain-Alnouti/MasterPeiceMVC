using MasterPeicefinal.Models;
using Microsoft.AspNet.Identity;
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

        public ActionResult Rating()
        {
            int id = int.Parse(Request.QueryString["id"]);
           ViewBag.orderID = int.Parse(Request.QueryString["orderID"]);

            var x = User.Identity.GetUserId();
            ViewBag.name = db.Clients.FirstOrDefault(a=>a.AspUserID==x).ClientName;
            return View(db.Products.FirstOrDefault(a => a.ProductID == id));
        }
        [HttpPost]
        public ActionResult SubmitRating(int id, int orderid, int star,string comment)
        {
            //int id = int.Parse(Request.QueryString["id"]);
            //int orderID = int.Parse(Request.QueryString["orderID"]);
            var x = User.Identity.GetUserId();
           int client = db.Clients.FirstOrDefault(a => a.AspUserID == x).ClientID;
            Rating rating=new Rating();
            rating.ProductID = (int)id;
            rating.UserID = client;
            rating.Comment = comment;
            rating.commentDate = DateTime.Now;
            rating.OrderID = orderid;

            if (star == 5)
            {
                rating.Rate ="5";
            }
            else if (star == 4)
            {
                rating.Rate = "4";
            }
            else if (star == 3)
            {
                rating.Rate = "3";
            }
            else if (star == 2)
            {
                rating.Rate = "2";
            }
            else if (star == 1)
            {
                rating.Rate = "1";
            }
             db.Ratings.Add(rating);
            db.SaveChanges();

            OrderDetail oo =db.OrderDetails.Find(orderid,id);
            if (oo != null)
            {
                oo.CommentStatus = true;
                db.SaveChanges();
                    }
            return RedirectToAction("UserProfile","Home");
        }




        public ActionResult ERating(int? id,int? orderID)
        {
            
            var x = User.Identity.GetUserId();
            ViewBag.name = db.Clients.FirstOrDefault(a => a.AspUserID == x).ClientName;
            int rate = db.Ratings.FirstOrDefault(a => a.ProductID == id & a.OrderID == orderID).RateID;
            var rating = db.Ratings.FirstOrDefault(a => a.RateID == rate);
            int proid =(int) rating.ProductID;
            var product = db.Products.Find(proid);
            return View(new Tuple<Rating, Product>(rating, product));
        }
        public ActionResult EditRating(int rateid, int star, string comment)
        {
            
            var x = User.Identity.GetUserId();
            int client = db.Clients.FirstOrDefault(a => a.AspUserID == x).ClientID;
            Rating rating = db.Ratings.Find(rateid);
            rating.Comment = comment;
            rating.commentDate = DateTime.Now;

            if (star == 5)
            {
                rating.Rate = "5";
            }
            else if (star == 4)
            {
                rating.Rate = "4";
            }
            else if (star == 3)
            {
                rating.Rate = "3";
            }
            else if (star == 2)
            {
                rating.Rate = "2";
            }
            else if (star == 1)
            {
                rating.Rate = "1";
            }
           
            db.SaveChanges();

         
            return RedirectToAction("UserProfile","Home");
        }
    }
}