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

namespace DATN_MobileShop.Areas.Admin.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Admin/Report
        private ShopMobileDB db = new ShopMobileDB();
        public ActionResult Index(string sortOrder, string searchstring, string currentFilter, int? page, string from_date, string to_date)
        {

            List<Product> products = db.Product.ToList();
            List<Product_detail> product_Details = db.Product_detail.ToList();
            List<Carts> carts = db.Carts.ToList();
            List<Cart_Details> cart_Details = db.Cart_Details.ToList();
            var thongke = from p in cart_Details
                          join pd in product_Details on p.id_product equals pd.id_product into table1
                          join cd in products on p.id_product equals cd.id into table2
                          join c in carts on p.id_cart equals c.id into table3
                          from pd in table1.DefaultIfEmpty()
                          from cd in table2.DefaultIfEmpty()
                          from c in table3
                          where cd.id_category != 3
                          select new Products
                          {
                              product = cd,
                              product_Detail = pd,
                              carts = c
                          };
            ViewBag.iphone = sortOrder == "iphone" ? "" : "iphone";
            ViewBag.samsung = sortOrder == "samsung" ? "" : "samsung";
            ViewBag.oppo = sortOrder == "oppo" ? "" : "oppo";
            ViewBag.vivo = sortOrder == "vivo" ? "" : "vivo";

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
            ViewBag.from_date = from_date;
            ViewBag.to_date = to_date;

            //lọc theo tên hàng
            if (!String.IsNullOrEmpty(searchstring))
            {
                thongke = thongke.Where(p => p.product.productName.Contains(searchstring));
            }

            if (!String.IsNullOrEmpty(from_date) && !String.IsNullOrEmpty(to_date))
            {
                DateTime from = DateTime.Parse(from_date);
                DateTime to = DateTime.Parse(to_date);
                thongke = thongke.Where(p => p.carts.create_date >= from && p.carts.create_date <=to);

            }
           

            //sắp xếp
            switch (sortOrder)
            {
                case "iphone":
                    thongke = thongke.Where(n => n.product.id_category == 1).OrderByDescending(n => n.carts.create_date);
                    break;
                case "samsung":
                    thongke = thongke.Where(n => n.product.id_category == 2).OrderByDescending(n => n.carts.create_date);
                    break;
                case "oppo":
                    thongke = thongke.Where(n => n.product.id_category == 4).OrderByDescending(n => n.carts.create_date);
                    break;
                case "vivo":
                    thongke = thongke.Where(n => n.product.id_category == 5).OrderByDescending(n => n.carts.create_date);
                    break;
                case "productNew":
                    thongke = thongke.Where(n => n.product.id_category == 3).OrderByDescending(n => n.carts.create_date);
                    break;
                default:
                    thongke = thongke.OrderByDescending(s => s.carts.create_date);
                    break;
            }
            int pageSize = 10;
            int PageNumber = (page ?? 1);//nếu page bằng Null thì trả về 1
            return View(thongke.ToPagedList(PageNumber, pageSize));
        }
    }
}