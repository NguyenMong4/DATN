using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopMobile_core.Models;

namespace WebShopMobile_core.Common
{
    public class HomeShow
    {      
        public List<Product> topnew { get; set; }
        public List<Advantisment> list_advertisment { get; set; }
        public List<Product> upcoming_products { get; set; }
        public Advantisment av2 { get; set; }
        public Advantisment av3 { get; set; }

    }
}
