﻿using MasterPeicefinal.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
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
            return View(db.Clients.Where(a=>a.AspUserID==user).ToList());
        }
    }
}