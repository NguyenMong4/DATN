using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopMobile_core.Common;
using WebShopMobile_core.Models;

namespace DATN_MobileShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private ShopMobileDB db = new ShopMobileDB();
        public ActionResult Index(string sortOrder)
        {
            ViewBag.sanphamnoibat= "sanphamnoibat";
            ViewBag.sanphammoi= "sanphammoi";
            ViewBag.gaming = "gaming";
            ViewBag.tacvucoban = "tacvucoban";
            ViewBag.camera = "camera";
            ViewBag.thoiluongpin = "thoiluongpin";
            ViewBag.manhinh = "manhinh";
            var listadvertisment = db.Advantisment.Where(p => p.posision == 1).Select(p => p).ToList();
            var adv2 = db.Advantisment.Where(p => p.posision == 2).Select(p => p).Take(1).FirstOrDefault();
            var adv3 = db.Advantisment.Where(p => p.posision == 3).Select(p => p).Take(1).FirstOrDefault();
            var upcoming_products = db.Product.Where(p => p.id_category == 3).Select(p => p).ToList();
            var top_new = db.Product.Where(s => s.id_category != 3).OrderByDescending(s => s.createdate).Take(5).ToList();
            var hs = new HomeShow();
            hs.list_advertisment = listadvertisment;
            hs.av2 = adv2;
            hs.av3 = adv3;
            hs.topnew = top_new;
            hs.upcoming_products = upcoming_products;
            return View(hs);

        }
    }
}