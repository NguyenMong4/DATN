using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopMobile_core.Common
{
   public class LoginModel
    {
        [Required(ErrorMessage = "Mời nhập số điện thoại")]
        public string phone { get; set; }

        [Required(ErrorMessage = "Mời nhập mật khẩu")]
        public string pass { get; set; }
        public bool RememberMe { get; set; }
    }
}
