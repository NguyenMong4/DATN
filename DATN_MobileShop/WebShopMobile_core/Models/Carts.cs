namespace WebShopMobile_core.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Carts
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Carts()
        {
            Cart_Details = new HashSet<Cart_Details>();
        }

        public int id { get; set; }

        public int? id_voucher { get; set; }

        public int? id_custommer { get; set; }

        public DateTime? create_date { get; set; }

        public DateTime? update_at { get; set; }

        public bool? statuss { get; set; }

        [Column(TypeName = "money")]
        public decimal? total { get; set; }

        public bool? payment { get; set; }
        [StringLength(200)]
        public string address { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart_Details> Cart_Details { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Voucher Voucher { get; set; }

        public virtual Customer Customer1 { get; set; }
    }
}
