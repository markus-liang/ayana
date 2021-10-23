using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class TipePembayaran
    {
        [Key]
        [Required]
        [Display(Name = "ID Tipe Pembayaran")]
        public int TipePembayaranID { get; set; }

        [Required]
        [Display(Name = "Nama")]
        public string Nama { get; set; }

        [Display(Name = "Keterangan")]
        public string Keterangan { get; set; }

        public virtual ICollection<Pembelian> Pembelians { get; set; }
        public virtual ICollection<Penjualan> Penjualans { get; set; }
    }
}