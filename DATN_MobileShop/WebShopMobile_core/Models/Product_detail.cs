namespace WebShopMobile_core.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product_detail
    {
        public int id { get; set; }

        public int? id_color { get; set; }

        public int? id_product { get; set; }
       
        public double? Screen_size { get; set; }

 
        public double? battery_capacity { get; set; }

 
        public double? Camera_rear { get; set; }


        public double? front_camera { get; set; }

        [StringLength(200)]
        public string operating_system { get; set; }

        public double? Ram { get; set; }


        public double? Rom { get; set; }

        public int? Sim { get; set; }

        [Column(TypeName = "xml")]
        public string More_Image { get; set; }

        [StringLength(2000)]
        public string Information { get; set; }


        public double? Size { get; set; }

        [StringLength(300)]
        public string material { get; set; }


        public int? release_time { get; set; }

        [StringLength(200)]
        public string Design { get; set; }

        [StringLength(200)]
        public string Chip { get; set; }


        public double? CPU_speed { get; set; }

        [Column(TypeName = "money")]
        public decimal? purchase_price { get; set; }

        [Column(TypeName = "money")]
        public decimal? price_endow { get; set; }

        [StringLength(200)]
        public string locations { get; set; }

        public DateTime? create_date { get; set; }

        [StringLength(200)]
        public string create_by { get; set; }

        [StringLength(4000)]
        public string Descriptions { get; set; }

        public int? viewcount { get; set; }

        public int? tophot { get; set; }

        public int? quantily { get; set; }

        public int? quantily_stock { get; set; }

        public virtual color color { get; set; }

        public virtual Product Product { get; set; }
    }
}
