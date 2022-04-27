using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebShopMobile_core.Models;

namespace WebShopMobile_core.Common
{
    public class UserDao
    {
        ShopMobileDB db = null;
        public UserDao()
        {
            db = new ShopMobileDB();
        }
        public long Insert(Customer entity)
        {
            db.Customer.Add(entity);
            db.SaveChanges();
            return entity.id;
        }

        public IEnumerable<Customer> ListAllPaging(int page, int pageSize)
        {
            return db.Customer.OrderByDescending(x => x.id).ToPagedList(page, pageSize);
        }

        public Customer GetById(string phone)
        {
            return db.Customer.SingleOrDefault(x => x.phone == phone);
        }
        public Employee GetById_Employee(string phone)
        {
            return db.Employee.SingleOrDefault(x => x.phone == phone);
        }
        public int Login(string sdt, string matKhau)
        {
            var result = db.Customer.SingleOrDefault(x => x.phone == sdt);
            if (result == null)
            {
                return 0;
            }
            else
            {               
                if (result.pass == matKhau)
                    return 2;
                else
                    return -2;
            }
        } 
        public int LoginAd(string sdt, string matKhau)
        {
            var result = db.Employee.SingleOrDefault(x => x.phone == sdt);
            if (result == null)
            {
                return 0;
            }
            else
            {               
                if (result.pass == matKhau)
                    return 2;
                else
                    return -2;
            }
        }

        public bool checkPhone(string sdt)
        {
            return db.Customer.Count(x => x.phone == sdt) > 0;//trả về true( =0 trả về false)
        }
        public bool checkEmail(string email)
        {
            return db.Customer.Count(x => x.email == email) > 0;
            
        }
        public int checkPassWord (string pass)
        {
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");
            int count = 0;           
            if (pass.Length < 6)
            {
                return 1;
            }
            else if (regexItem.IsMatch(pass))
            {               
                return 2;
            }   
            else
            {
                string space;
                for (int i = 0; i < pass.Length; i++)
                {
                    space = pass.Substring(i, 1);
                    if (space == " ")
                        count++;
                }
                if (count > 0)
                    return 3;
            }    
            return 0;
        }
        public int checkusername(string username)
        {
            var regExp = new Regex("/[`!@#$%^&*()_+\\-=\\[\\]{};':\"\\|,.<>\\/?~]/;");
            if (regExp.IsMatch(username))
                return 1;
            return 0;
        }
    }
}

