using PagedList;
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
    public class ProductsController : BaseController
    {
        private ShopMobileDB db = new ShopMobileDB();

        // GET: Admin/Products
        public ActionResult Index(string sortOrder, string searchstring,string currentFilter, int? page)
        {
            //các biến sắp xếp
            ViewBag.CurrentSort = sortOrder;//biến lấy yêu cầu sắp xếp hiện tại
            ViewBag.iphone = sortOrder == "iphone" ? "" : "iphone";
            ViewBag.samsung = sortOrder == "samsung" ? "" : "samsung";
            ViewBag.oppo = sortOrder == "oppo" ? "" : "oppo";
            ViewBag.vivo = sortOrder == "vivo" ? "" : "vivo";
            ViewBag.productNew = sortOrder == "productNew" ? "" : "productNew";
            //Lấy giá trị bộ lọc dữ liệu hiện tại
            if (searchstring != null)
            {
                page = 1;//trang đầu tiên
            }
            else
            {
                searchstring = currentFilter;
            }
            ViewBag.CurrentFilter = searchstring;

            //lấy danh sách hàng
            var product = db.Product.Select(s => s);
            //lọc theo tên hàng
            if (!String.IsNullOrEmpty(searchstring))
            {
                product = product.Where(p => p.productName.Contains(searchstring));
            }
            //sắp xếp
            switch (sortOrder)
            {                        
                case "iphone":
                    product = product.Where(s => s.id_category == 1).OrderByDescending(s => s.id);
                    break;
                case "samsung":
                    product = product.Where(s => s.id_category == 2).OrderByDescending(s => s.id);
                    break;
                case "oppo":
                    product = product.Where(s => s.id_category == 4).OrderByDescending(s => s.id);
                    break;
                case "vivo":
                    product = product.Where(s => s.id_category == 5).OrderByDescending(s => s.id);
                    break; 
                case "productNew":
                    product = product.Where(s => s.id_category == 3).OrderByDescending(s => s.id);
                    break;
                default:
                    product = product.OrderByDescending(s => s.createdate);
                    break;
            }
            int pageSize = 10;
            int PageNumber = (page ?? 1);//nếu page bằng Null thì trả về 1
            return View(product.ToPagedList(PageNumber, pageSize));
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product_detail = db.Product_detail.Include(p => p.color).Include(p => p.Product).Where(a => a.id_product == id).FirstOrDefault();
            if (product_detail == null)
            {

            }
            return View(product_detail);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.id_category = new SelectList(db.Product_category, "id", "category_name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,productName,createdate,update_at,endow,stutus,photo,price,id_category")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.photo = " ";
                var f = Request.Files["ImageFile"];
                if (f != null && f.ContentLength > 0)
                {
                    string FileName = System.IO.Path.GetFileName(f.FileName);
                    string UploadPath = Server.MapPath("~/wwwroot/img/" + FileName);
                    f.SaveAs(UploadPath);
                    product.photo = FileName;
                }
                product.createdate = DateTime.Now;
                db.Product.Add(product);
                Product_detail product_Detail = new Product_detail();
                product_Detail.id_product = product.id;
                db.Product_detail.Add(product_Detail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_category = new SelectList(db.Product_category, "id", "category_name", product.id_category);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_category = new SelectList(db.Product_category, "id", "category_name", product.id_category);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,productName,endow,stutus,photo,price,id_category")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.photo = " ";
                var f = Request.Files["ImageFile"];
                if (f != null && f.ContentLength > 0)
                {
                    string FileName = System.IO.Path.GetFileName(f.FileName);
                    string UploadPath = Server.MapPath("~/wwwroot/img/" + FileName);
                    f.SaveAs(UploadPath);
                    product.photo = FileName;
                }
                product.update_at = DateTime.Now;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_category = new SelectList(db.Product_category, "id", "category_name", product.id_category);
            return View(product);
        }
        //Edit Product_detail
        public ActionResult Edit_pd(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_detail product_detail = db.Product_detail.Find(id);
            if (product_detail == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_color = new SelectList(db.color, "id", "color_name", product_detail.id_color);
            ViewBag.id_product = new SelectList(db.Product, "id", "productName", product_detail.id_product);
            return View(product_detail);
        }

        // POST: Admin/Product_detail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_pd([Bind(Include = "id,id_product,id_color,Screen_size,battery_capacity,Camera_rear,front_camera,operating_system,Ram,Rom,Sim,More_Image,Information,Size,material,release_time,Design,Chip,CPU_speed,purchase_price,price_endow,locations,Descriptions,viewcount,tophot,quantily,quantily_stock")] Product_detail product_detail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_detail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_color = new SelectList(db.color, "id", "color_name", product_detail.id_color);
            ViewBag.id_product = new SelectList(db.Product, "id", "productName", product_detail.id_product);
            return View(product_detail);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
  
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Advantisment advantisment = db.Advantisment.FirstOrDefault(x=>x.id_product==id);
            if (advantisment != null)
            {
                db.Advantisment.Remove(advantisment);
                db.SaveChanges();
            }
            Product_detail product_Detail = db.Product_detail.Where(n=>n.id_product==id).FirstOrDefault();
            if (product_Detail != null)
            {
                db.Product_detail.Remove(product_Detail);
                db.SaveChanges();
            }
            Cart_Details cart_Details = db.Cart_Details.Where(n=>n.id_product==id).FirstOrDefault();
            if (cart_Details != null)
            {
                db.Cart_Details.Remove(cart_Details);
                db.SaveChanges();
            }
            
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
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

        public ActionResult AddQuantity(int id, FormCollection f)
        {
        
            Product_detail pd = db.Product_detail.SingleOrDefault(n => n.id_product == id);
            pd.quantily += int.Parse(f["new_quantity"]);
            db.SaveChanges();
            ViewBag.thongbao = "Thêm thành công";
            return RedirectToAction("Index");
        }
    }
}
