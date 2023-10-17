namespace AEF_Project
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Transaction")]
    public partial class Transaction
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string Store_name { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Item_id { get; set; }

        [StringLength(50)]
        public string Store_Filled { get; set; }

        [StringLength(50)]
        public string Item_name { get; set; }

        public int? Quantitiy { get; set; }

        [Required]
        [StringLength(50)]
        public string Supplier_name { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Pro_date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Exp_date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Supplier_tel { get; set; }

        [StringLength(50)]
        public string Unit { get; set; }

        public int? Total_quantity { get; set; }

        public int? Last_quantity { get; set; }

        public int? Total_quantity1 { get; set; }

        public int? Last_quantity1 { get; set; }

        public virtual Item Item { get; set; }

        public virtual Store Store { get; set; }

        public virtual supplier supplier { get; set; }
    }
}
