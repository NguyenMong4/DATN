using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebShopMobile_core.Common;
using WebShopMobile_core.DAO;
using WebShopMobile_core.Models;

namespace DATN_MobileShop.Areas.Admin.Controllers
{
    public class AdminsController : BaseController
    {
        ShopMobileDB db = new ShopMobileDB();
        // GET: Admin/Admins
        [HasPermission(RoleId = "VIEW")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 7)
        {
            var dao = new AdminDao();
            var model = dao.GetAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        [HasPermission(RoleId = "CREATE")]
        public ActionResult Create()
        {
            ViewBag.GroupAdminId = new SelectList(db.GroupAdmins, "Id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "id,fullname,pass,address,phone,email,createdate,Status,birth_day,update_time,GroupAdminId")] Employee employee)
        {           
            if (ModelState.IsValid)
            {
                var encryptedMd5Pas = Encrytor.CreateMD5(employee.pass);
                employee.pass = encryptedMd5Pas;
                employee.createdate = DateTime.Now;
                db.Employee.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupAdminId = new SelectList(db.GroupAdmins, "Id", "Name", employee.GroupAdminId);
            return View(employee);
        }
        [HttpGet]
        [HasPermission(RoleId = "EDIT")]
        public ActionResult Edit(int id)
        {
            SetviewBag();
            var adm = new AdminDao().ViewDetail(id);
            return View(adm);
        }
        [HttpPost]
        public ActionResult Edit(Employee admin)
        {

            if (ModelState.IsValid)
            {
                var dao = new AdminDao();
                if (!string.IsNullOrEmpty(admin.pass))
                {
                    var encryptedMd5Pas = Encrytor.CreateMD5(admin.pass);
                    admin.pass = encryptedMd5Pas;
                    admin.update_time = DateTime.Now;
                }

                var result = dao.Update(admin);
                if (result)
                {
                    SetAlert("Update Admin success", "success");
                    return RedirectToAction("Index", "Admins");
                }
                else
                {
                    ModelState.AddModelError("", "error");
                }
                return View("Index");
            }
            SetviewBag();
            return View(admin);

        }
   
        [HasPermission(RoleId = "DELETE")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Admin/Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employee.Find(id);
            db.Employee.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public void SetviewBag(string SelectedId = null)
        {
            var groupAdminDao = new GroupAdminDao();

            ViewBag.GroupAdminId = new SelectList(groupAdminDao.GetAll(), "Id", "Name", SelectedId);
        }
    }
}