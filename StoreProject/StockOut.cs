namespace StoreProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StockOut")]
    public partial class StockOut
    {
        public int? store_id { get; set; }

        [Key]
        public int stockout_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? stockout_date { get; set; }

        public int? item_id { get; set; }

        public int? quantity { get; set; }

        public int? customer_id { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Item Item { get; set; }

        public virtual Store Store { get; set; }
    }
}
