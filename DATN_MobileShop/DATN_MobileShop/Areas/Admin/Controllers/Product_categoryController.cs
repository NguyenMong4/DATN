using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebShopMobile_core.Models;

namespace DATN_MobileShop.Areas.Admin.Controllers
{
    public class Product_categoryController : BaseController
    {
        private ShopMobileDB db = new ShopMobileDB();

        // GET: Admin/Product_category
        public ActionResult Index()
        {
            return View(db.Product_category.ToList());
        }

        // GET: Admin/Product_category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_category product_category = db.Product_category.Find(id);
            if (product_category == null)
            {
                return HttpNotFound();
            }
            return View(product_category);
        }

        // GET: Admin/Product_category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Product_category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,category_name,createdate,createby,stutus,showonhome")] Product_category product_category)
        {
            if (ModelState.IsValid)
            {
                product_category.createdate = DateTime.Now;
                product_category.createby = (string)Session["FullName"];
                db.Product_category.Add(product_category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product_category);
        }

        // GET: Admin/Product_category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_category product_category = db.Product_category.Find(id);
            if (product_category == null)
            {
                return HttpNotFound();
            }
            return View(product_category);
        }

        // POST: Admin/Product_category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,category_name,createby,stutus,showonhome")] Product_category product_category)
        {
            if (ModelState.IsValid)
            {
                product_category.update_at = DateTime.Now;             
                db.Entry(product_category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product_category);
        }

        // GET: Admin/Product_category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_category product_category = db.Product_category.Find(id);
            if (product_category == null)
            {
                return HttpNotFound();
            }
            return View(product_category);
        }

        // POST: Admin/Product_category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product_category product_category = db.Product_category.Find(id);
            db.Product_category.Remove(product_category);
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
