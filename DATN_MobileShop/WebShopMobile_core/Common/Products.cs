using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopMobile_core.Models;

namespace WebShopMobile_core.Common
{

    public class Products
    {
        public Product product { get; set; }
        public Product_detail product_Detail { get; set; }
        public Cart_Details cart_Details { get; set; }
        public Carts carts { get; set; }


    }
}
