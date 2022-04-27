using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopMobile_core.Models;

namespace WebShopMobile_core.Common
{
    public class Cart
    {
        ShopMobileDB db = new ShopMobileDB();
        [Key]
        public int iIDsanpham { set; get; }
        public string sTensanpham { set; get; }
        public string sHinhanhsanpham { set; get; }
        public string diachi { get; set; }
        public Double dDongia { set; get; }
        public int iSoluong { set; get; }
        public Double dThanhtien
        {
            get { return iSoluong * dDongia; }
        }
        public Cart(int IDsanpham)
        {
            iIDsanpham = IDsanpham;
            Product sp = db.Product.Single(n => n.id == iIDsanpham);
            sTensanpham = sp.productName;
            sHinhanhsanpham = sp.photo;
            dDongia = Double.Parse(sp.price.ToString());
            iSoluong = 1;
        }
    }
}
