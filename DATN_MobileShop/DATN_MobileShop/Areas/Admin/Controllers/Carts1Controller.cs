using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebShopMobile_core.Common;
using WebShopMobile_core.Models;

namespace DATN_MobileShop.Areas.Admin.Controllers
{
    public class Carts1Controller : BaseController
    {
        private ShopMobileDB db = new ShopMobileDB();
        // GET: Admin/Carts1
        public ActionResult Index()
        {
            var carts = db.Carts.Include(c => c.Customer).Include(c => c.Customer1).Include(c => c.Voucher);               
            return View(carts.ToList());
        }  
     
        // GET: Admin/Carts1/Details/5
        public ActionResult Details(int? id)
        {        
            var cart_Details = db.Cart_Details.Include(c => c.Carts).Include(c => c.Product).Where(a => a.id_cart == id);
            double iTongtien = 0;
            iTongtien = (double)cart_Details.Sum(n => n.price);
            ViewBag.Tongtien = iTongtien;
            return View(cart_Details.ToList());
        }

        // GET: Admin/Carts1/Create
        public ActionResult Create()
        {
            ViewBag.id_custommer = new SelectList(db.Customer, "id", "fullname");
            ViewBag.id_voucher = new SelectList(db.Voucher, "id", "code");
            return View();
        }

        // POST: Admin/Carts1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_voucher,id_custommer,create_date,update_at,statuss,total,payment,address")] Carts carts)
        {
            if (ModelState.IsValid)
            {
                db.Carts.Add(carts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_custommer = new SelectList(db.Customer, "id", "fullname", carts.id_custommer);
            //ViewBag.id_custommer = new SelectList(db.Customer, "id", "fullname", carts.id_custommer);
            ViewBag.id_voucher = new SelectList(db.Voucher, "id", "code", carts.id_voucher);
            return View(carts);
        }

        // GET: Admin/Carts1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carts carts = db.Carts.Find(id);
            if (carts == null)
            {
                return HttpNotFound();
            }
            /*ViewBag.id_custommer = new SelectList(db.Customer, "id", "fullname", carts.id_custommer);
            ViewBag.id_custommer = new SelectList(db.Customer, "id", "fullname", carts.id_custommer);
            ViewBag.id_voucher = new SelectList(db.Voucher, "id", "code", carts.id_voucher);*/           
            return View(carts);
        }

        // POST: Admin/Carts1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_voucher,id_custommer,create_date,update_at,statuss,total,payment,address")] Carts carts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carts).State = EntityState.Modified;               
                if(carts.payment == true)
                {
                    var cart_Details = db.Cart_Details.Where(p => p.id_cart == carts.id);
                    foreach (var item in cart_Details)
                    {
                        Product_detail product_Detail = db.Product_detail.Where(p => p.id_product == item.id_product).FirstOrDefault();
                        product_Detail.quantily -= item.quanity;
                        product_Detail.viewcount += item.quanity;
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_custommer = new SelectList(db.Customer, "id", "fullname", carts.id_custommer);
            ViewBag.id_voucher = new SelectList(db.Voucher, "id", "code", carts.id_voucher);
            return View(carts);
        }

        // GET: Admin/Carts1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carts carts = db.Carts.Find(id);
            if (carts == null)
            {
                return HttpNotFound();
            }
            return View(carts);
        }

        // POST: Admin/Carts1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var cart_detail = db.Cart_Details.Where(n => n.id_cart == id).FirstOrDefault();
            if (cart_detail != null)
            {
                db.Cart_Details.Remove(cart_detail);
                db.SaveChanges();
            }
            Carts carts = db.Carts.Find(id);
            db.Carts.Remove(carts);
            db.SaveChanges();
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
