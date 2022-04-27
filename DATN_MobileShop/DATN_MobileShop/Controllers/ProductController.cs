using PagedList;
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

namespace DATN_MobileShop.Controllers
{
    public class ProductController : Controller
    {
        private ShopMobileDB db = new ShopMobileDB();

        // GET: Product
        public ActionResult Index(string sortOrder, string searchstring, string currentFilter, int? page)
        {
            //các biến sắp xếp
            ViewBag.CurrentSort = sortOrder;//biến lấy yêu cầu sắp xếp hiện tại
            ViewBag.saptheogiatang = sortOrder == "gia" ? "" : "gia";
            ViewBag.saptheogiagiam = sortOrder == "gia_desc" ? "" : "gia_desc";
            ViewBag.saptheogia100_300 = sortOrder == "gia100_300" ? "" : "gia100_300";
            ViewBag.saptheogia300_500 = sortOrder == "gia300_500" ? "" : "gia300_500";
            ViewBag.saptheogia500_700 = sortOrder == "gia500_700" ? "" : "gia500_700";
            ViewBag.saptheogia700 = sortOrder == "gia700" ? "" : "gia700";

            ViewBag.iphone = sortOrder == "iphone" ? "" : "iphone";
            ViewBag.samsung = sortOrder == "samsung" ? "" : "samsung";
            ViewBag.oppo = sortOrder == "oppo" ? "" : "oppo";
            ViewBag.vivo = sortOrder == "vivo" ? "" : "vivo";


            ViewBag.sanphamnoibat = sortOrder == "sanphamnoibat" ? "" : "sanphamnoibat";
            ViewBag.sanphammoi = sortOrder == "sanphammoi" ? "" : "sanphammoi";
            ViewBag.thoiluongpin = sortOrder == "thoiluongpin" ? "" : "thoiluongpin";
            ViewBag.gaming = sortOrder == "gaming" ? "" : "gaming";
            ViewBag.tacvucoban = sortOrder == "tacvucoban" ? "" : "tacvucoban";
            ViewBag.camera = sortOrder == "camera" ? "" : "camera";
            ViewBag.manhinh = sortOrder == "manhinh" ? "" : "manhinh";

            //ViewBag.saptheogiagiam = sortOrder == "gia_desc";
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
            var product = db.Product.Where(n=>n.id_category!=3).Select(s => s);
            //lọc theo tên hàng
            if (!String.IsNullOrEmpty(searchstring))
            {
                product = product.Where(p => p.productName.Contains(searchstring));
            }
            //sắp xếp
            switch (sortOrder)
            {
                case "gia":
                    product = product.OrderBy(s => s.price);
                    break;
                case "gia_desc":
                    product = product.OrderByDescending(s => s.price);
                    break;
                case "gia100_300":
                    product = product.Where(s => s.price >= 1000000 && s.price <= 2000000).OrderBy(s => s.productName);
                    break;
                case "gia300_500":
                    product = product.Where(s => s.price >= 3000000 && s.price <= 5000000).OrderBy(s => s.productName);
                    break;
                case "gia500_700":
                    product = product.Where(s => s.price >= 5000000 && s.price <= 7000000).OrderBy(s => s.productName);
                    break;
                case "gia700":
                    product = product.Where(s => s.price >= 7000000).OrderBy(s => s.productName);
                    break;
                case "iphone":
                    product = product.Where(s => s.id_category == 1).OrderBy(s => s.id);
                    break;
                case "samsung":
                    product = product.Where(s => s.id_category == 2).OrderBy(s => s.id);
                    break;
                case "oppo":
                    product = product.Where(s => s.id_category == 3).OrderBy(s => s.id);
                    break;
                case "vivo":
                    product = product.Where(s => s.id_category == 4).OrderBy(s => s.id);
                    break;
                default:
                    product = product.OrderBy(s => s.id);
                    break;
            }
            
            int pageSize = 8;
            int PageNumber = (page ?? 1);//nếu page bằng Null thì trả về 1
            return View(product.ToPagedList(PageNumber, pageSize));
        }

        public ActionResult Product_search(string sortOrder, int? page)
        {
            List<Product> products = db.Product.ToList();
            List<Product_detail> product_Details = db.Product_detail.ToList();
            var product_detail = from p in products
                                 join pd in product_Details on p.id equals pd.id_product into table1
                                 from pd in table1.DefaultIfEmpty()
                                 where p.id_category != 3
                                 select new Products
                                 {
                                     product = p,
                                     product_Detail = pd,

                                 };
            ViewBag.sanphamnoibat = sortOrder == "sanphamnoibat" ? "" : "sanphamnoibat";
            ViewBag.gaming = sortOrder == "gaming" ? "" : "gaming";
            ViewBag.tacvucoban = sortOrder == "tacvucoban" ? "" : "tacvucoban";
            ViewBag.manhinh = sortOrder == "manhinh" ? "" : "manhinh";
            ViewBag.camera = sortOrder == "camera" ? "" : "camera";
            ViewBag.sanphammoi = sortOrder == "sanphammoi" ? "" : "sanphammoi";
            ViewBag.thoiluongpin = sortOrder == "thoiluongpin" ? "" : "thoiluongpin";
            ViewBag.Ram = sortOrder == "Ram" ? "" : "Ram";
            ViewBag.Rom = sortOrder == "Rom" ? "" : "Rom";
            //ViewBag.thoiluongpin = sortOrder == "thoiluongpin" ? "" : "thoiluongpin";
            //sắp xếp
            switch (sortOrder)
            {
                case "sanphamnoibat":
                    product_detail = product_detail.Where(n => n.product.id_category != 3).OrderByDescending(n => n.product_Detail.viewcount);
                    break;
                case "gaming":
                    product_detail = product_detail.Where(n => n.product.id_category != 3 && n.product_Detail.Ram > 6);
                    break;
                case "manhinh":
                    product_detail = product_detail.Where(n => n.product.id_category != 3 && n.product_Detail.Screen_size > 5);
                    break;
                case "tacvucoban":
                    product_detail = product_detail.Where(n => n.product.id_category != 3 && n.product_Detail.Ram <= 4 && n.product_Detail.Rom <= 32).OrderByDescending(n => n.product.id);
                    break;
                case "camera":
                    product_detail = product_detail.Where(n => n.product.id_category != 3 && n.product_Detail.Camera_rear > 15 && n.product_Detail.front_camera > 20).OrderByDescending(n => n.product.id);
                    break;
                case "sanphammoi":
                    product_detail = product_detail.Where(n => n.product.id_category != 3).OrderByDescending(n => n.product.id).Take(12);
                    break;
                case "thoiluongpin":
                    product_detail = product_detail.Where(n => n.product.id_category != 3 && n.product_Detail.battery_capacity > 4500).OrderByDescending(n => n.product.id);
                    break;
                case "Ram":
                    product_detail = product_detail.OrderByDescending(n => n.product_Detail.Ram);
                    break;

                default:
                    product_detail = product_detail.OrderByDescending(s => s.product.id);
                    break;
            }
            int pageSize = 10;
            int PageNumber = (page ?? 1);//nếu page bằng Null thì trả về 1
            return View(product_detail.ToPagedList(PageNumber, pageSize));
        }
        public ActionResult Details(int? id)
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
