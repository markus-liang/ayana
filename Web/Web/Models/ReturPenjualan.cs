using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class ReturPenjualan
    {
        [Key]
        [Required]
        [Display(Name = "ID Retur")]
        public int ReturPenjualanID { get; set; }

        [Required]
        [Display(Name = "Tanggal")]
        [DataType(DataType.DateTime)]
        public DateTime Tanggal { get; set; }

        [Required]
        [Display(Name = "Nilai Total")]
        public int Total { get; set; }

        [Display(Name = "User")]
        public string UserName { get; set; }

        [Display(Name = "ID Customer")]
        [ForeignKey("Customer")]
        public int? CustomerID { get; set; }

        [Display(Name = "Tanggal Pembayaran")]
        [DataType(DataType.DateTime)]
        public DateTime? TanggalBayar { get; set; }

        [Display(Name = "Tipe Pembayaran")]
        [ForeignKey("TipePembayaran")]
        public int? TipePembayaranID { get; set; }

        [Display(Name = "Keterangan")]
        public string Keterangan { get; set; }

        public int StatusTransaksi { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual TipePembayaran TipePembayaran { get; set; }
        public virtual ICollection<ReturPenjualanDetail> ReturPenjualanDetails { get; set; }
    }

    public class ReturPenjualanDetail
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("ReturPenjualan")]
        [Display(Name = "ID Retur")]
        public int ReturPenjualanID { get; set; }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("Penjualan")]
        [Display(Name = "Faktur Penjualan")]
        public int PenjualanID { get; set; }

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
        public bool isPaid { get; set; }

        public virtual ReturPenjualan ReturPenjualan { get; set; }
        public virtual Penjualan Penjualan { get; set; }
        public virtual Barang Barang { get; set; }
    }
}