using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class Pembelian
    {
        [Key]
        [Required]
        [Display(Name = "ID Faktur")]
        public int PembelianID { get; set; }

        [Required]
        [Display(Name = "Tanggal")]
        public DateTime Tanggal { get; set; }

        [Required]
        [Display(Name = "ID Supplier")]
        [ForeignKey("Supplier")]
        public int SupplierID { get; set; }

        [Display(Name = "Jatuh Tempo")]
        public DateTime JatuhTempo { get; set; }

        [Display(Name = "Tanggal Pembayaran")]
        [DataType(DataType.DateTime)]
        public DateTime? TanggalBayar { get; set; }

        [ForeignKey("TipePembayaran")]
        [Display(Name = "Tipe Pembayaran")]
        public int? TipePembayaranID { get; set; }

        [Required]
        [Display(Name = "Harga Total")]
        public int Total { get; set; }

        [Display(Name = "User")]
        public string UserName { get; set; }

        [Display(Name = "Keterangan")]
        public string Keterangan { get; set; }

        public int StatusTransaksi { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<PembelianDetail> PembelianDetails { get; set; }
        public virtual TipePembayaran TipePembayaran { get; set; }
    }

    public class PembelianDetail
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Pembelian")]
        [Display(Name = "ID Faktur")]
        public int PembelianID { get; set; }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("Barang")]
        [Display(Name = "ID Barang")]
        public int BarangID { get; set; }

        [Required]
        [Display(Name = "Jumlah")]
        public int Jumlah { get; set; }

        [Required]
        [Display(Name = "Harga")]
        public int Harga { get; set; }

        public virtual Pembelian Pembelian { get; set; }
        public virtual Barang Barang { get; set; }
    }
}