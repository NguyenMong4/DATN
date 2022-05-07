using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebShopMobile_core.Common;
using WebShopMobile_core.Models;

namespace DATN_MobileShop.Controllers
{
    public class CustomersController : Controller
    {
        ShopMobileDB db = new ShopMobileDB();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection collection, Customer user)
        {
            var dao = new UserDao();
            int count = 0;
            var hoten = collection["Tennguoidung"];
            var matkhau = collection["Matkhau"];
            var nhaplaimk = collection["Nhaplaimatkhau"];
            var email = collection["Email"];
            var sdt = collection["Sodienthoai"];
            var addr = collection["Diachi"];
            int result = dao.checkPassWord(matkhau);
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["tennguoidungnull"] = "Tên người dùng không được để trống";
            }
            else if (dao.checkusername(hoten) == 1)
            {
                ViewData["tennguoidungnull"] = "Tên người dùng không được chứa kí tự đặc biệt";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["matkhaunull"] = "Mật khẩu không được để trống";
            }
            else if (String.IsNullOrEmpty(nhaplaimk))
            {
                ViewData["nhaplaimatkhaunull"] = "Nhập lại mật khẩu không được để trống";
            }
            else if (String.IsNullOrEmpty(addr))
            {
                ViewData["diachinull"] = "Địa chỉ không được để trống";
            }
            else if (String.IsNullOrEmpty(sdt))
            {
                ViewData["sodienthoainull"] = "Số điện thoại không được để trống";
            }
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["emailnull"] = "Email không được để trống";
            }
            else if (dao.checkEmail(email))
            {
                ViewData["emailnull"] = "Email đã được đăng kí";
            }
            else if (dao.checkPhone(user.phone))
            {
                ViewBag.thongbao = "Số điện thoại đã tồn tại";
            }
            else if (result == 1)
            {
                ViewData["matkhaunull"] = "Mật khẩu phải từ 6 kí tự";
            }
            else if (result == 3)
            {
                ViewData["matkhaunull"] = "Mật khẩu không được chứa khoảng trắng";
            }
            else if (result == 2)
                ViewData["matkhaunull"] = "Mật khẩu phải bao gồm kí tự dặc biệt";
            else if (matkhau != nhaplaimk)
            {
                ViewData["nhaplaimatkhaunull"] = "Mật khẩu và nhập lại mật khẩu phải trùng nhau";
            }
            else
            {
                var encryptedMd5Pas = Encrytor.CreateMD5(matkhau);
                user.fullname = hoten;
                user.pass = encryptedMd5Pas;
                user.address = addr;
                user.phone = sdt;
                user.email = email;
                user.createdate = DateTime.Now;
                db.Customer.Add(user);
                db.SaveChanges();
                count = 1;
                if (count == 1)
                {
                    //ViewBag.thongbao = "Đăng kí thành công";
                    return RedirectToAction("Login", "Customers");
                }
                else
                    ViewBag.thongbao = "Đăng kí thất bại";
            }

            return this.Register();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model, FormCollection collection)
        {
            var dao = new UserDao();
            var phone = collection["phone"];
            var pass = collection["pass"];
            if (String.IsNullOrEmpty(phone))
            {
                ViewData["sodienthoainull"] = "Số điện thoại không được để trống";
            }
            else if (String.IsNullOrEmpty(pass))
            {
                ViewData["matkhaunull"] = "Mật khẩu không được để trống";
            }
            else
            {
                var result = dao.Login(model.phone, Encrytor.CreateMD5(model.pass));
                if (result == 2)
                {
                    var user = dao.GetById(model.phone);
                    var userSession = new UserLogin();
                    userSession.phone = user.phone;
                    userSession.IDuser = user.id;
                    Session["Taikhoan"] = user;
                    Session["username"] = user.fullname;
                    Session["matkhau"] = user.pass;
                    Session["diachi"] = user.address;
                    Session["email"] = user.email;
                    Session["sdt"] = user.phone;
                    Session.Add(CommonContants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {

                    ViewBag.thongbao = "Tài khoản không tồn tại";
                }
                else
                {
                    ViewBag.thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }

            return View("Login");
        }
        public ActionResult Details()
        {
            if (Session["Taikhoan"] != null)
            {
                Customer customer = Session["Taikhoan"] as Customer;
                return View(customer);
            }
            return Redirect("/");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,fullname,pass,address,phone,email")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                Session["Taikhoan"] = customer;
                return RedirectToAction("Index");
            }
            return View(customer);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Customers");
        }
    }
}
