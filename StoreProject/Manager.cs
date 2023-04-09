namespace StoreProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Manager
    {
        [Key]
        public int manager_id { get; set; }

        [StringLength(50)]
        public string manager_name { get; set; }

        [StringLength(50)]
        public string manager_email { get; set; }
    }
}
