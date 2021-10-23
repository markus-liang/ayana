using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class Hutang
    {
        [Display(Name = "ID Supplier")]
        [ForeignKey("Supplier")]
        public int SupplierID { get; set; }

        [Required]
        public int Total { get; set; }

        [Required]
        public int PembelianID { get; set; }

        [Required]
        public DateTime JatuhTempo{ get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual Pembelian Pembelian { get; set; }
    }
}