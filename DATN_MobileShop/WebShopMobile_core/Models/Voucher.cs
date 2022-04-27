namespace WebShopMobile_core.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Voucher")]
    public partial class Voucher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Voucher()
        {
            Carts = new HashSet<Carts>();
        }

        public int id { get; set; }

        [StringLength(200)]
        public string code { get; set; }

        public DateTime? create_date { get; set; }

        public DateTime? update_time { get; set; }

        public bool? statuss { get; set; }

        public DateTime? enddate { get; set; }

        public double? endow { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Carts> Carts { get; set; }
    }
}
