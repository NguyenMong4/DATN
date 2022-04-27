using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopMobile_core.Models;

namespace WebShopMobile_core.DAO
{
    public class RoleDao
    {
        private ShopMobileDB db = null;
        public int totalRow = 0;

        public RoleDao()
        {
            db = new ShopMobileDB();
        }
        public string Insert(Role entity)
        {
            db.Roles.Add(entity);
            entity.CreateDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;
            db.SaveChanges();
            return entity.Id;
        }

        public IEnumerable<Role> GetAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Role> model = db.Roles;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString)).OrderByDescending(x => x.CreateDate);
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }

        public bool Update(Role role)
        {
            try
            {
                var entity = db.Roles.Find(role.Id);
                entity.Name = role.Name;
                entity.UpdatedDate = DateTime.Now;
                entity.Status = role.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Role GetbyId(string name)
        {
            return db.Roles.SingleOrDefault(x => x.Name == name);
        }

        public Role ViewDetail(string id)
        {
            return db.Roles.Where(a => a.Id == id).FirstOrDefault();
        }



        public bool Delete(string id)
        {
            try
            {
                var role = db.Roles.Find(id);
                db.Roles.Remove(role);
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
