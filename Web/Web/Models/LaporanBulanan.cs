using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class LaporanBulanan
    {
        [Display(Name = "Bulan")]
        [Required]
        public DateTime Bulan { get; set; }

        [Display(Name = "Penjualan")]
        [Required]
        public int Penjualan { get; set; }

        [Display(Name = "Penjualan Batal")]
        [Required]
        public int PenjualanBatal { get; set; }

        [Display(Name = "Pembelian")]
        [Required]  
        public int Pembelian { get; set; }

        [Display(Name = "Pembelian Batal")]
        [Required]
        public int PembelianBatal { get; set; }

        [Display(Name = "Penerimaan")]
        [Required]
        public int Penerimaan { get; set; }

        [Display(Name = "Pengeluaran")]
        [Required]
        public int Pengeluaran { get; set; }
    }
}