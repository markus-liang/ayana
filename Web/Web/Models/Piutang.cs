using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class Piutang
    {
        [Display(Name = "ID Customer")]
        [ForeignKey("Customer")]
        public int CustomerID { get; set; }

        [Required]
        public int Total { get; set; }

        [Required]
        public int PenjualanID { get; set; }

        [Required]
        public DateTime JatuhTempo{ get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Penjualan Penjualan { get; set; }
    }
}