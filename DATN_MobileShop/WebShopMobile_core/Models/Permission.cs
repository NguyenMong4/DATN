using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopMobile_core.Models
{
    [Table("Permission")]
    public partial class Permission
    {
        [Key]
        [StringLength(50)]
        [Column(Order = 1)]
        [Required(ErrorMessage = "The group id must not be vacated.")]
        public string GroupAdminId { get; set; }

        [Key]
        [StringLength(50)]
        [Column(Order = 2)]
        [Required(ErrorMessage = "The role id must not be vacated.")]
        public string RoleId { get; set; }

        public virtual GroupAdmin GroupAdmin { get; set; }

        public virtual Role Role { get; set; }

    }
}
