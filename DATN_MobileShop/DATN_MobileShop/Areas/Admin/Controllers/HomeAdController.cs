﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DATN_MobileShop.Areas.Admin.Controllers
{
    public class HomeAdController : BaseController
    {
        // GET: Admin/HomeAd
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "LoginAd");
        }
    }
}