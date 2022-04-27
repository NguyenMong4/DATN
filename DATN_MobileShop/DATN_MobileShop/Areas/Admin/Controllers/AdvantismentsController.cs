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
    public class AdvantismentsController : BaseController
    {
        private ShopMobileDB db = new ShopMobileDB();

        // GET: Admin/Advantisments
        public ActionResult Index()
        {
            var advantisment = db.Advantisment.Include(a => a.Product);
            return View(advantisment.ToList());
        }

        // GET: Admin/Advantisments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advantisment advantisment = db.Advantisment.Find(id);
            if (advantisment == null)
            {
                return HttpNotFound();
            }
            return View(advantisment);
        }

        // GET: Admin/Advantisments/Create
        public ActionResult Create()
        {
            ViewBag.id_product = new SelectList(db.Product, "id", "productName");
            return View();
        }

        // POST: Admin/Advantisments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_product,photo,statuss,posision,create_date")] Advantisment advantisment)
        {
            if (ModelState.IsValid)
            {
                advantisment.photo = " ";
                var f = Request.Files["ImageFile"];
                if (f != null && f.ContentLength > 0)
                {
                    string FileName = System.IO.Path.GetFileName(f.FileName);
                    string UploadPath = Server.MapPath("~/wwwroot/img/" + FileName);
                    f.SaveAs(UploadPath);
                    advantisment.photo = FileName;
                }
                advantisment.create_date = DateTime.Now;
                db.Advantisment.Add(advantisment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_product = new SelectList(db.Product, "id", "productName", advantisment.id_product);
            return View(advantisment);
        }

        // GET: Admin/Advantisments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advantisment advantisment = db.Advantisment.Find(id);
            if (advantisment == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_product = new SelectList(db.Product, "id", "productName", advantisment.id_product);
            return View(advantisment);
        }

        // POST: Admin/Advantisments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_product,photo,statuss,texts,posision,create_date,update_date")] Advantisment advantisment)
        {
            if (ModelState.IsValid)
            {
                advantisment.photo = " ";
                var f = Request.Files["ImageFile"];
                if (f != null && f.ContentLength > 0)
                {
                    string FileName = System.IO.Path.GetFileName(f.FileName);
                    string UploadPath = Server.MapPath("~/wwwroot/img/" + FileName);
                    f.SaveAs(UploadPath);
                    advantisment.photo = FileName;
                }
                advantisment.update_date = DateTime.Now;
                db.Entry(advantisment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_product = new SelectList(db.Product, "id", "productName", advantisment.id_product);
            return View(advantisment);
        }

        // GET: Admin/Advantisments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advantisment advantisment = db.Advantisment.Find(id);
            if (advantisment == null)
            {
                return HttpNotFound();
            }
            return View(advantisment);
        }

        // POST: Admin/Advantisments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Advantisment advantisment = db.Advantisment.Find(id);
            db.Advantisment.Remove(advantisment);
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
