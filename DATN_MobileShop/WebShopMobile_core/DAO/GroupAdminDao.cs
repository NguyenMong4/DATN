using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopMobile_core.Models;

namespace WebShopMobile_core.DAO
{
    public class GroupAdminDao
    {
        private ShopMobileDB db = null;   
        public int totalRow = 0;

        public GroupAdminDao()
        {
            db = new ShopMobileDB();
        }
        public string Insert(GroupAdmin entity)
        {
            db.GroupAdmins.Add(entity);
            entity.CreateDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;
            db.SaveChanges();
            return entity.Id;
        }
        public List<GroupAdmin> GetAll()
        {
            return db.GroupAdmins.Where(x => x.Status == true).ToList();
        }
        public IEnumerable<GroupAdmin> GetAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<GroupAdmin> model = db.GroupAdmins;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString)).OrderByDescending(x => x.CreateDate);
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }

        public bool Update(GroupAdmin groupAdmin)
        {
            try
            {
                var entity = db.GroupAdmins.Find(groupAdmin.Id);
                entity.Name = groupAdmin.Name;
                entity.UpdatedDate = DateTime.Now;
                entity.Status = groupAdmin.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public GroupAdmin GetbyId(string name)
        {
            return db.GroupAdmins.SingleOrDefault(x => x.Name == name);
        }

        public GroupAdmin ViewDetail(string id)
        {
            return db.GroupAdmins.Where(a => a.Id == id).FirstOrDefault();
        }



        public bool Delete(string id)
        {
            try
            {
                var groupAdmin = db.GroupAdmins.Find(id);
                db.GroupAdmins.Remove(groupAdmin);
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
