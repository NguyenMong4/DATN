namespace WebShopMobile_core.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Advantisment = new HashSet<Advantisment>();
            Cart_Details = new HashSet<Cart_Details>();
            Product_detail = new HashSet<Product_detail>();
            Wishlist = new HashSet<Wishlist>();
        }

        public int id { get; set; }

        [StringLength(200)]
        public string productName { get; set; }

        public DateTime? createdate { get; set; }

        public DateTime? update_at { get; set; }

        public double? endow { get; set; }

        public bool? stutus { get; set; }

        public string photo { get; set; }

        [Column(TypeName = "money")]
        public decimal? price { get; set; }

        public int? id_category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Advantisment> Advantisment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart_Details> Cart_Details { get; set; }

        public virtual Product_category Product_category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product_detail> Product_detail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Wishlist> Wishlist { get; set; }
    }
}
