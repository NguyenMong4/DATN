using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebShopMobile_core.Common;

namespace DATN_MobileShop.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Admin/Base
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var session = (AdminLogin)Session[CommonAdmin.ADMIN_SESSION];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "LoginAd", action = "Index", Areas = "Admin" })
                    );
            }
            base.OnActionExecuted(filterContext);
        }
        protected void SetAlert(String message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
        protected void Alert(String message, string type)
        {
            TempData["Message"] = message;
            if (type == "error")
            {
                TempData["AlertType"] = "alert-error";
            }

        }
    }
}