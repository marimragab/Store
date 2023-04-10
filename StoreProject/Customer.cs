namespace StoreProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            StockOuts = new HashSet<StockOut>();
        }

        [Key]
        public int customer_id { get; set; }

        [Required]
        [StringLength(50)]
        public string customer_name { get; set; }

        [StringLength(20)]
        public string customer_phone { get; set; }

        [StringLength(20)]
        public string customer_fax { get; set; }

        [Required]
        [StringLength(20)]
        public string customer_mobile { get; set; }

        [StringLength(50)]
        public string customer_email { get; set; }

        [StringLength(100)]
        public string customer_website { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StockOut> StockOuts { get; set; }
    }
}
