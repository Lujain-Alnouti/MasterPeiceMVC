using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MasterPeicefinal.Models;

namespace MasterPeicefinal.Controllers
{
    public class ProductsController : Controller
    {
        private MasterPeiceEntities db = new MasterPeiceEntities();

        // GET: Products
        public ActionResult Index()
        {
            ViewBag.categoryCount = db.Categories.Count();
            ViewBag.ProductCount = db.Products.Count();
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CateID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,CateID,ProductName,ProductImage,Desc1,Desc2,ProductPrice,TotalQuantity")] Product product, HttpPostedFileBase productIMG)
        {
            if (ModelState.IsValid)
            {
                if (productIMG != null)
                {
                    //string fileName = Path.GetFileName(image.FileName);
                    string path = Server.MapPath("~/website/") + productIMG.FileName;
                    productIMG.SaveAs(path);
                    product.ProductImage = productIMG.FileName;
                }
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CateID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CateID);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            Session["ProductImg"] = product.ProductImage;
            ViewBag.name1 = product.ProductName;
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CateID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CateID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,CateID,ProductName,ProductImage,Desc1,Desc2,ProductPrice,TotalQuantity")] Product product, HttpPostedFileBase productIMG)
        {
            if (ModelState.IsValid)
            {
                if (productIMG != null)
                {
                    //string fileName = Path.GetFileName(image.FileName);
                    string path = Server.MapPath("~/website/") + productIMG.FileName;
                    productIMG.SaveAs(path);
                    product.ProductImage = productIMG.FileName;
                }
                else
                {
                    product.ProductImage = Session["ProductImg"].ToString();
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CateID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CateID);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            ViewBag.name1 = product.ProductName;
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try { 
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            }
            catch(Exception ex) {
               Session["ErrorMessage"] = "Sorry ,You Can't Delete This Item 😢";
                return RedirectToAction("Delete",id);
            }
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
