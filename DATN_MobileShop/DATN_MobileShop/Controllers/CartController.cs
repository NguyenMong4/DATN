using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopMobile_core.Common;
using WebShopMobile_core.Models;

namespace DATN_MobileShop.Controllers
{
    public class CartController : Controller
    {
        ShopMobileDB db = new ShopMobileDB();
        // GET: Cart
        public List<Cart> Laygiohang()
        {
            List<Cart> lstGiohang = Session["Giohang"] as List<Cart>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<Cart>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }

        public ActionResult ThemGiohang(int IDsanpham, string strURL)
        {
            List<Cart> lstGiohang = Laygiohang();
            Cart sanpham = lstGiohang.Find(n => n.iIDsanpham == IDsanpham);
            if (sanpham == null)
            {
                sanpham = new Cart(IDsanpham);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoluong++;
                return Redirect(strURL);
            }
        }

        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<Cart> lstGiohang = Session["Giohang"] as List<Cart>;
            if (lstGiohang != null)
            {
                iTongSoLuong = lstGiohang.Sum(n => n.iSoluong);

            }
            return iTongSoLuong;
        }

        private double Tongtien()
        {
            double iTongtien = 0;
            List<Cart> lstGiohang = Session["Giohang"] as List<Cart>;
            if (lstGiohang != null)
            {
                iTongtien = lstGiohang.Sum(n => n.dThanhtien);
            }
            return iTongtien;
        }

        public ActionResult Giohang()
        {
            List<Cart> lstGiohang = Laygiohang();
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("CartNull");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            Session["tongsoluong"] = TongSoLuong();
            ViewBag.Tongtien = Tongtien();
            return View(lstGiohang);
        }

        public ActionResult XoaGiohang(int iMaSp)
        {
            List<Cart> lstGiohang = Laygiohang();
            Cart sanpham = lstGiohang.SingleOrDefault(n => n.iIDsanpham == iMaSp);
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.iIDsanpham == iMaSp);
                return RedirectToAction("Giohang");
            }
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("CartNull");
            }
            return RedirectToAction("Giohang");
        }
        public ActionResult CapnhatGiohang(int iMaSp, FormCollection f)
        {
            List<Cart> lstGiohang = Laygiohang();
            Cart sanpham = lstGiohang.SingleOrDefault(n => n.iIDsanpham == iMaSp);
            if (sanpham != null)
            {
                sanpham.iSoluong = int.Parse(f["txtSoluong"].ToString());
            }

            return RedirectToAction("Giohang");
        }
        // GET: Giohang
        public ActionResult XoaTatcaGiohang()
        {
            List<Cart> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("Login", "Customers");
            }
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Cart> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = Tongtien();
            return View(lstGiohang);
        }

        public ActionResult DATHANG(FormCollection collection)
        {
            Carts ddh = new Carts();
            Customer kh = (Customer)Session["Taikhoan"];
            List<Cart> gh = Laygiohang();
            ddh.id_custommer = kh.id;
            ddh.create_date = DateTime.Now;
            //var Ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["Ngaygiao"]);
            ddh.total = (decimal)Tongtien();
            ddh.statuss = false;
            ddh.payment = false;
            db.Carts.Add(ddh);
            foreach (var item in gh)
            {

                Cart_Details ctdh = new Cart_Details();
                ctdh.id_cart = ddh.id;
                ctdh.id_product = item.iIDsanpham;
                ctdh.quanity = item.iSoluong;
                ctdh.price = (decimal)item.dThanhtien;
                db.Cart_Details.Add(ctdh);              
            }
            db.SaveChanges();
            Session["Giohang"] = null;
            return RedirectToAction("Xacnhandonhang", "Cart");
        }

        public ActionResult Xacnhandonhang()
        {
            return View();
        }
        public ActionResult CartNull()
        {
            return View();
        }
    }
}