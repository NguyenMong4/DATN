namespace WebShopMobile_core.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Advantisment")]
    public partial class Advantisment
    {
        public int id { get; set; }

        public int? id_product { get; set; }

        [Column(TypeName = "text")]
        public string photo { get; set; }

        public bool? statuss { get; set; }

        [StringLength(2000)]
        public string texts { get; set; }

        public int? posision { get; set; }

        public DateTime? create_date { get; set; }

        public DateTime? update_date { get; set; }

        public virtual Product Product { get; set; }
    }
}
