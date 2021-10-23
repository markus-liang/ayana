using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class ReturPembelian
    {
        [Key]
        [Required]
        [Display(Name = "ID Retur")]
        public int ReturPembelianID { get; set; }

        [Required]
        [Display(Name = "Tanggal")]
        public DateTime Tanggal { get; set; }

        [Required]
        [Display(Name = "Nilai Total")]
        public int Total { get; set; }

        [Display(Name = "User")]
        public string UserName { get; set; }

        [Display(Name = "ID Supplier")]
        [ForeignKey("Supplier")]
        public int? SupplierID { get; set; }

        [Display(Name = "Tanggal Pembayaran")]
        [DataType(DataType.DateTime)]
        public DateTime? TanggalBayar { get; set; }

        [Display(Name = "Tipe Pembayaran")]
        [ForeignKey("TipePembayaran")]
        public int? TipePembayaranID { get; set; }

        [Display(Name = "Keterangan")]
        public string Keterangan { get; set; }

        public int StatusTransaksi { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual TipePembayaran TipePembayaran { get; set; }
        public virtual ICollection<ReturPembelianDetail> ReturPembelianDetails { get; set; }
    }

    public class ReturPembelianDetail
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("ReturPembelian")]
        [Display(Name = "ID Retur")]
        public int ReturPembelianID { get; set; }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("Pembelian")]
        [Display(Name = "Faktur Pembelian")]
        public int PembelianID { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Barang")]
        [Display(Name = "ID Barang")]
        public int BarangID { get; set; }

        [Required]
        [Display(Name = "Jumlah")]
        public int Jumlah { get; set; }

        [Required]
        [Display(Name = "Harga")]
        public int Harga { get; set; }

        [Required]
        public bool isPaid{ get; set; }

        public virtual ReturPembelian ReturPembelian { get; set; }
        public virtual Pembelian Pembelian { get; set; }
        public virtual Barang Barang { get; set; }
    }
}