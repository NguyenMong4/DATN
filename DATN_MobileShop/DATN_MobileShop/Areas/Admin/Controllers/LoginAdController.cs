using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopMobile_core.Common;
using WebShopMobile_core.DAO;

namespace DATN_MobileShop.Areas.Admin.Controllers
{
    public class LoginAdController : Controller
    {
        // GET: Admin/LoginAd
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new AdminDao();
                var result = dao.Login(model.phone, Encrytor.CreateMD5(model.pass),true);
                if (result == 1)
                {
                    var user = dao.GetbyId(model.phone);
                    var userSession = new AdminLogin();
                    userSession.AdminName = user.phone;
                    userSession.AdminId = user.id;
                    userSession.GroupAdminId = user.GroupAdminId;
                    var listPermission = dao.getListPermision(model.phone);
                    Session.Add(CommonAdmin.SESSION_PERMISSION, listPermission);

                    Session.Add(CommonAdmin.ADMIN_SESSION, userSession);
                    return RedirectToAction("Dashboard", "HomeAd");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "The account does not exist.");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Account is locked");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Incorrect password");
                }
                else if (result == -3)
                {
                    ModelState.AddModelError("", "Accounts without permissions");
                }
                else
                {
                    ModelState.AddModelError("", "Please check your account again");
                }
            }
            return View("Index");
        }

        public ActionResult Logout()
        {
            Session[WebShopMobile_core.Common.CommonAdmin.ADMIN_SESSION] = null;

            return RedirectToAction("Index", "LoginAd");
        }
    }
    
}