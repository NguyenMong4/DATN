namespace WebShopMobile_core.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cart_Details
    {
        public int id { get; set; }

        public int? id_cart { get; set; }

        public int? id_product { get; set; }

        public int? quanity { get; set; }

        [Column(TypeName = "money")]
        public decimal? price { get; set; }

        public virtual Carts Carts { get; set; }

        public virtual Product Product { get; set; }
    }
}
