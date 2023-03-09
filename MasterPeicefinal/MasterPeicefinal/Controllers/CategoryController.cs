using MasterPeicefinal.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MasterPeicefinal.Controllers
{
    
    public class CategoryController : Controller
    {
        // GET: Category
        MasterPeiceEntities db = new MasterPeiceEntities();
        public ActionResult Categories()
        {
            return View(db.Categories.ToList());
        }

        public ActionResult Products(int? id)
        {
            ViewBag.categ = db.Categories.FirstOrDefault(a => a.CategoryID == id).CategoryName;
            
            return View(db.Products.Where(a=>a.CateID==id).ToList());
        }

        public ActionResult ProductDetails(int? id)
        {
            return View(db.Products.Where(a => a.ProductID == id).ToList());
        }



        public ActionResult AddProduct(int? productid, int Quantity)
        { var useruu = User.Identity.GetUserId();
            foreach (var item in (db.Carts.Where(a => a.ProductID == productid && a.UserID == useruu).ToList())) {
                //item.UserID == User.Identity.GetUserId()
                //if (item.ProductID == productid && item.UserID== useruu)
                //{
                int CartID1 =item.CartID;


                int qq = (int)item.Quantity;
                   
                    int newQ = qq + Quantity;
                    Cart cart1 = db.Carts.Find(CartID1);
                    float x1 = (float)db.Products.FirstOrDefault(a => a.ProductID == productid).ProductPrice;
                    float total1 = newQ * x1;
                    cart1.Price = (float)total1;
                    cart1.Quantity = newQ;
                    db.SaveChanges();
                    return RedirectToAction("ProductDetails/"+productid);
                //}

            }

            Cart cart=new Cart();
            float x = (float)db.Products.FirstOrDefault(a => a.ProductID == productid).ProductPrice;
            float total = Quantity*x;
            cart.Price =(float) total;
            cart.ProductID = productid;
            cart.Quantity = Quantity;
            //cart.UserID = User.Identity.GetUserId();
            cart.UserID = User.Identity.GetUserId();

            db.Carts.Add(cart);
            db.SaveChanges();
            return RedirectToAction("ProductDetails/"+productid);
        }


        public ActionResult _Comments(int? id)
        {
            string count = db.Ratings.Where(a => a.ProductID== id).Count().ToString();   
            ViewBag.Num = count;
            return PartialView(db.Ratings.Where(a => a.ProductID == id).ToList());
        }

        public ActionResult Cart()
        {
            //var user = User.Identity.GetUserId();   
            var user = User.Identity.GetUserId();
            int count = db.Carts.Where(a => a.UserID == user).Count();
            ViewBag.Num = count;
            float priceafter = 0;
            if (count > 0) { 
            float totalPrice = (float)db.Carts.Where(a => a.UserID == user).Select(a=>a.Price).Sum();
            ViewBag.TotalPrice = totalPrice;
            ViewBag.Discount = "0";

            


            if (totalPrice>=455 && totalPrice <= 689)
            {
                ViewBag.Discount = "5";
                //priceafter = totalPrice - (totalPrice * (10 / 100));
                float sub = totalPrice*((float)5.0/ (float)100.0);
                priceafter = totalPrice-sub;


            }
            else if (totalPrice >= 690 && totalPrice <= 849)
            {
                ViewBag.Discount = "10";
                //priceafter = totalPrice - (totalPrice * (15 / 100));
                float sub = totalPrice * ((float)10.0 / (float)100.0); 
                priceafter = totalPrice - sub;

            }
            else if (totalPrice >= 850 && totalPrice <= 949)
            {
                ViewBag.Discount = "15";
                float sub = totalPrice * ((float)15.0 / (float)100.0);
                priceafter = totalPrice-sub;

            }
            else if (totalPrice >= 950 )
            {
                ViewBag.Discount = "20";
                //priceafter = totalPrice - (totalPrice * (25 / 100));
                float sub = totalPrice * ((float)20.0 / (float)100.0); 
                priceafter = totalPrice - sub;

            }
          
            if (totalPrice > 1500)
            {
               
                ViewBag.shipping = 0;
            }
            else
            {
               priceafter += 10;
                ViewBag.shipping = 10;
            }
            ViewBag.AfterPrice = priceafter;
            Session["FinalTotal"] = priceafter;
            }
            return View(db.Carts.Where(a=>a.UserID==user).ToList());
        }




        public ActionResult CheckOut()
        {
            ViewBag.Cities = db.Cities.ToList();
           
            //var user = User.Identity.GetUserId();   
            var user = User.Identity.GetUserId();
            ViewBag.cart = db.Carts.Where(a => a.UserID == user).ToList();
            int count = db.Carts.Where(a => a.UserID == user).Count();
            ViewBag.Usercity = db.Clients.FirstOrDefault(a => a.AspUserID == user).City;
            ViewBag.Num = count;
            float priceafter = 0;
            if (count > 0)
            {
                float totalPrice = (float)db.Carts.Where(a => a.UserID == user).Select(a => a.Price).Sum();
                ViewBag.TotalPrice = totalPrice;
                ViewBag.Discount = "0";
                int Dis = 0;




                if (totalPrice >= 455 && totalPrice <= 689)
                {
                    Dis = 5;
                    ViewBag.Discount = "5";
                    //priceafter = totalPrice - (totalPrice * (10 / 100));
                    float sub = totalPrice * ((float)5.0 / (float)100.0);
                    priceafter = totalPrice - sub;


                }
                else if (totalPrice >= 690 && totalPrice <= 849)
                {
                    Dis = 10;
                    ViewBag.Discount = "10";
                    //priceafter = totalPrice - (totalPrice * (15 / 100));
                    float sub = totalPrice * ((float)10.0 / (float)100.0);
                    priceafter = totalPrice - sub;

                }
                else if (totalPrice >= 850 && totalPrice <= 949)
                {
                    Dis = 15;
                    ViewBag.Discount = "15";
                    float sub = totalPrice * ((float)15.0 / (float)100.0);
                    priceafter = totalPrice - sub;

                }
                else if (totalPrice >= 950)
                {
                    ViewBag.Discount = "20";
                    Dis = 20;
                    //priceafter = totalPrice - (totalPrice * (25 / 100));
                    float sub = totalPrice * ((float)20.0 / (float)100.0);
                    priceafter = totalPrice - sub;

                }

                if (totalPrice > 1500)
                {

                    ViewBag.shipping = 0;
                }
                else
                {
                    priceafter += 10;
                    ViewBag.shipping = 10;
                }
                ViewBag.AfterPrice = priceafter;
                Session["FinalTotal"] = priceafter;
                Session["Discounts"] = Dis;
                Session["Shipping"] = ViewBag.shipping;
            }

            return View(db.Clients.Where(a => a.AspUserID == user).ToList());
        }

        public ActionResult CheckOrder(string Email,string Address1,string Address2,int city,string Phone)
        {
            var uuser = User.Identity.GetUserId();
            int ClientIID = db.Clients.FirstOrDefault(a => a.AspUserID ==uuser).ClientID;
            Client client = db.Clients.Find(ClientIID);
            client.Address1 = Address1;
            client.Address2 = Address2;
            client.City = city;
            client.Email = Email;
            client.PhoneNumber = Phone;
            db.SaveChanges();
            DateTime orderdate = DateTime.Now.Date;
            string ordertime = DateTime.Now.ToString("hh:mm:ss");

            MainOrder mainorder = new MainOrder();
            mainorder.OrderStatus = false;
            mainorder.OrderDate = orderdate;
            mainorder.OrderTime = ordertime;
            mainorder.TotalPrice = (float)Session["FinalTotal"];
            mainorder.UserID =ClientIID;
            mainorder.shipping =(int)Session["Shipping"];
            mainorder.Discount = Convert.ToInt32(Session["Discounts"]);
            db.MainOrders.Add(mainorder);
            db.SaveChanges();
            var uu= db.Clients.FirstOrDefault(a => a.AspUserID == uuser).ClientID;
            var cart = db.Carts.Where(a => a.UserID == uuser).ToList();
            var ooor = db.MainOrders.Where( a => a.OrderDate == orderdate &&  a.OrderTime == ordertime ).ToList();
            int orderID = 0;
            foreach (var ii in ooor)
            {
                orderID = ii.OrderID;
              
            }
            foreach (var item in cart)
            {
                OrderDetail details = new OrderDetail();
                //int orderID = db.MainOrders.FirstOrDefault(a => a.OrderDate == orderdate).OrderID;
                DateTime dd = DateTime.Now;
                if (item.UserID == User.Identity.GetUserId())
                {
                    
                        details.OrderID = orderID;
                    
                    
                 
                    details.ProductID =(int)item.ProductID;
                    details.Quantity = (int)item.Quantity;
                    details.Price = (float)item.Price;
                    details.CommentStatus = false;
                    db.OrderDetails.Add(details);
                    db.SaveChanges();

                }
                

            }
            foreach (var itemm in db.Carts.Where(a => a.UserID == uuser).ToList())
            {
                Cart cart2 = db.Carts.FirstOrDefault(a => a.UserID == uuser);
                db.Carts.Remove(cart2);
                db.SaveChanges();
            }

            return RedirectToAction("Categories");
        }


        public ActionResult DeleteFromCart(int? id)
        {
            var user = User.Identity.GetUserId();
            var users = db.Carts.Where(a => a.ProductID == id && a.UserID == user).ToList();
            int cartiid = 0;
            foreach(var yy in users)
            {
                cartiid = (int)yy.CartID;

            }
            if (id != null)
            {
                Cart cart = db.Carts.Find(cartiid);
                    db.Carts.Remove(cart);
                db.SaveChanges();
                return RedirectToAction("Cart");
            }
            return RedirectToAction("Cart");
        }
    }
    
}
