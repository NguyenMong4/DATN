namespace WebShopMobile_core.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Blog")]
    public partial class Blog
    {
        public int id { get; set; }

        [Column(TypeName = "text")]
        public string title { get; set; }

        [StringLength(2000)]
        public string photo { get; set; }

        [Column(TypeName = "text")]
        public string contents { get; set; }

        public bool? statuss { get; set; }

        public DateTime? create_date { get; set; }

        public DateTime? update_date { get; set; }
    }
}
