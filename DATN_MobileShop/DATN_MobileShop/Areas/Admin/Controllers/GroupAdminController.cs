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
    public class GroupAdminController : BaseController
    {
        // GET: Admin/GroupAdmin
        [HasPermission(RoleId = "VIEW")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 7)
        {
            var dao = new GroupAdminDao();
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
        public ActionResult Create(GroupAdmin groupAdmin)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dao = new GroupAdminDao();
                    string id = dao.Insert(groupAdmin);
                    if (id != null)
                    {
                        SetAlert("Create Slider success", "success");
                        return RedirectToAction("Index", "GroupAdmin");
                    }
                    else
                    {
                        ModelState.AddModelError("", "error");
                    }
                    return View("Index");
                }
                catch (Exception ex)
                {

                    ex.Message.Contains(ViewBag.error = "id already exists");
                }
            }

            return View(groupAdmin);
        }
        [HttpGet]
        [HasPermission(RoleId = "EDIT")]
        public ActionResult Edit(string id)
        {
            var slider = new GroupAdminDao().ViewDetail(id);
            return View(slider);
        }
        [HttpPost]
        public ActionResult Edit(GroupAdmin GroupAdmin)
        {

            if (ModelState.IsValid)
            {

                var dao = new GroupAdminDao();

                var result = dao.Update(GroupAdmin);
                if (result)
                {
                    SetAlert("Update Slider success", "success");
                    return RedirectToAction("Index", "GroupAdmin");
                }
                else
                {
                    ModelState.AddModelError("", "error");
                }
                return View("Index");

            }
            return View(GroupAdmin);

        }
        [HttpDelete]
        [HasPermission(RoleId = "DELETE")]
        public ActionResult Delete(string id)
        {
            var dao = new GroupAdminDao();
            var result = dao.Delete(id);
            if (result)
            {
                SetAlert("Delete Slider success", "success");
                return RedirectToAction("Index", "GroupAdmin");
            }
            else
            {
                SetAlert("Unable to delete Group Admin with data", "error");
            }
            return RedirectToAction("Index");
        }
    }
}