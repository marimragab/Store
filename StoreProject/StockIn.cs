namespace StoreProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StockIn")]
    public partial class StockIn
    {
        [Key]
        public int stockin_id { get; set; }

        public int? store_id { get; set; }

        public int? item_id { get; set; }

        public int? supplier_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? stockin_date { get; set; }

        public int? quantity { get; set; }

        [Column(TypeName = "date")]
        public DateTime? production_date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? expiry_date { get; set; }

        public virtual Item Item { get; set; }

        public virtual Store Store { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
