using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopMobile_core.Common;
using WebShopMobile_core.Models;

namespace WebShopMobile_core.DAO
{
    public class AdminDao
    {
        private ShopMobileDB db = null;

        public AdminDao()
        {
            db = new ShopMobileDB();
        }

        public int Insert(Employee entity)
        {
            entity.createdate = DateTime.Now;
            db.Employee.Add(entity);
            db.SaveChanges();
            return entity.id;
        }

        public IEnumerable<Employee> GetAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Employee> model = db.Employee;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.fullname.Contains(searchString)).OrderByDescending(x => x.createdate);
            }
            return model.OrderByDescending(x => x.createdate).ToPagedList(page, pageSize);
        }

        public bool Update(Employee admin)
        {
            try
            {
                var adm = db.Employee.Find(admin.id);
                adm.fullname = admin.fullname;
                adm.email = admin.email;
                if (!string.IsNullOrEmpty(admin.pass))
                {
                    adm.pass = admin.pass;
                }
                adm.address = admin.address;
                adm.phone = admin.phone;          
                adm.createdate = admin.createdate;         
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Employee GetbyId(string phone)
        {
            return db.Employee.SingleOrDefault(x => x.phone == phone);
        }

        public Employee ViewDetail(int id)
        {
            return db.Employee.Where(a => a.id == id).FirstOrDefault();
        }
        public List<string> getListPermision(string phone)
        {
            var admin = db.Employee.Single(a => a.phone == phone);
            var data = (from p in db.Permissions
                        join g in db.GroupAdmins on p.GroupAdminId equals g.Id
                        join r in db.Roles on p.RoleId equals r.Id
                        where g.Id == admin.GroupAdminId
                        select new
                        {
                            RoleId = p.RoleId,
                            GroupAdminId = p.GroupAdminId
                        }).AsEnumerable().Select(x => new Permission()
                        {
                            RoleId = x.RoleId,
                            GroupAdminId = x.GroupAdminId
                        });
            return data.Select(x => x.RoleId).ToList();
        }
        public int Login(string phone, string password, bool isLoginAdmin)
        {
            var result = db.Employee.SingleOrDefault(x => x.phone == phone);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (isLoginAdmin == true)
                {
                    if (result.GroupAdminId.ToString() == CommonGroup.ADMIN_GROUP || result.GroupAdminId.ToString() == CommonGroup.EMPLOYEES_GROUP)
                    {
                        if (result.Status == false)
                        {
                            return -1;
                        }
                        else
                        {
                            if (result.pass == password)
                            {
                                return 1;
                            }
                            else
                            {
                                return -2;
                            }
                        }
                    }
                    else
                    {
                        return -3;
                    }
                }
                else
                {
                    if (result.Status == false)
                    {
                        return -1;
                    }
                    else
                    {
                        if (result.pass == password)
                        {
                            return 1;
                        }
                        else
                        {
                            return -2;
                        }
                    }
                }

            }
        }

        public bool Delete(int id)
        {
            try
            {
                var admin = db.Employee.Find(id);
                db.Employee.Remove(admin);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
