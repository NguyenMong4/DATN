using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopMobile_core.Common;
using WebShopMobile_core.DAO;
using WebShopMobile_core.Models;

namespace DATN_MobileShop.Areas.Admin.Controllers
{
    public class RoleController : BaseController
    {
        // GET: Admin/Role
        [HasPermission(RoleId = "VIEW")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 7)
        {
            var dao = new RoleDao();
            var model = dao.GetAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        [HasPermission(RoleId = "CREATE")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                var dao = new RoleDao();
                string id = dao.Insert(role);
                if (id != null)
                {
                    SetAlert("Create Slider success", "success");
                    return RedirectToAction("Index", "Role");
                }
                else
                {
                    ModelState.AddModelError("", "error");
                }
                return View("Index");
            }

            return View(role);
        }
        [HttpGet]
        [HasPermission(RoleId = "EDIT")]
        public ActionResult Edit(string id)
        {
            var slider = new RoleDao().ViewDetail(id);
            return View(slider);
        }
        [HttpPost]
        public ActionResult Edit(Role role)
        {

            if (ModelState.IsValid)
            {
                var dao = new RoleDao();

                var result = dao.Update(role);
                if (result)
                {
                    SetAlert("Update Slider success", "success");
                    return RedirectToAction("Index", "Role");
                }
                else
                {
                    ModelState.AddModelError("", "error");
                }
                return View("Index");
            }
            return View(role);

        }
        [HttpDelete]
        [HasPermission(RoleId = "DELETE")]
        public ActionResult Delete(string id)
        {
            var dao = new RoleDao();
            var result = dao.Delete(id);
            if (result)
            {
                SetAlert("Delete Slider success", "success");
                return RedirectToAction("Index", "Role");
            }
            else
            {
                ModelState.AddModelError("", "error");
            }
            return RedirectToAction("Index");
        }
    }
}