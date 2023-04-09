namespace StoreProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Supplier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Supplier()
        {
            StockIns = new HashSet<StockIn>();
        }

        [Key]
        public int supplier_id { get; set; }

        [Required]
        [StringLength(50)]
        public string supplier_name { get; set; }

        [StringLength(15)]
        public string supplier_phone { get; set; }

        [StringLength(20)]
        public string supplier_fax { get; set; }

        [Required]
        [StringLength(15)]
        public string supplier_mobile { get; set; }

        [StringLength(50)]
        public string supplier_email { get; set; }

        [StringLength(100)]
        public string supplier_website { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StockIn> StockIns { get; set; }
    }
}
