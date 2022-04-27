namespace WebShopMobile_core.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {

        public int id { get; set; }

        [StringLength(500)]
        public string fullname { get; set; }

        [StringLength(200)]
        public string pass { get; set; }

        [StringLength(500)]
        public string address { get; set; }
        [StringLength(10)]
        public string phone { get; set; }

        [StringLength(2000)]
        public string email { get; set; }

        public DateTime? createdate { get; set; }

        public bool Status { get; set; }

        public DateTime? birth_day { get; set; }

        public DateTime? update_time { get; set; }

        [StringLength(50)]
        public string GroupAdminId { get; set; }    
        public virtual GroupAdmin GroupAdmin { get; set; }

    }
}
