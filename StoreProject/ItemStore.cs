namespace StoreProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ItemStore")]
    public partial class ItemStore
    {
        public int id { get; set; }

        public int? item_id { get; set; }

        public int? store_id { get; set; }

        public virtual Item Item { get; set; }

        public virtual Store Store { get; set; }
    }
}
