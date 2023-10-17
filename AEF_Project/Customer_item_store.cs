namespace AEF_Project
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer_item_store
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string store_name { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_item { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int tel_customer { get; set; }

        public int? tquantity { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int perm_num { get; set; }

        [Column(TypeName = "date")]
        public DateTime? cperm_date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? exp_date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? pro_date { get; set; }

        [StringLength(50)]
        public string Item_unit { get; set; }

        [StringLength(50)]
        public string item_name { get; set; }

        [StringLength(50)]
        public string customer_name { get; set; }

        public int? Total_quantity { get; set; }

        public int? Last_quantity { get; set; }

        public virtual customer customer { get; set; }

        public virtual Item Item { get; set; }

        public virtual Store Store { get; set; }
    }
}
