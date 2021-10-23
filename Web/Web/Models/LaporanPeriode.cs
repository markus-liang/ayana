using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class LaporanPeriode
    {
        [Display(Name = "Mulai")]
        [Required]
        public DateTime Mulai { get; set; }

        [Display(Name = "Selesai")]
        [Required]
        public DateTime Selesai { get; set; }

        [Display(Name = "Kas Awal")]
        [Required]
        public int KasAwal { get; set; }

        [Display(Name = "Penjualan")]
        [Required]
        public int TotalPenjualan { get; set; }

        [Display(Name = "Penjualan Batal")]
        [Required]
        public int PenjualanBatal { get; set; }

        [Display(Name = "Pembelian")]
        [Required]
        public int TotalPembelian { get; set; }

        [Display(Name = "Pembelian Batal")]
        [Required]
        public int PembelianBatal { get; set; }

        [Display(Name = "Penerimaan")]
        [Required]
        public int Penerimaan { get; set; }

        [Display(Name = "Pengeluaran")]
        [Required]
        public int Pengeluaran { get; set; }

        [Display(Name = "Kas Akhir")]
        [Required]
        public int KasAkhir { get; set; }

        [Display(Name = "ID Kas Awal")]
        [Required]
        public int KasAwalID { get; set; }

        [Display(Name = "ID Kas Akhir")]
        [Required]
        public int KasAkhirID { get; set; }
    }
}