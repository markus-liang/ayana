using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class Barang
    {
        [Key]
        [Required]
        [Display(Name = "ID Barang")]
        public int BarangID { get; set; }

        [Display(Name = "Jenis Barang")]
        [ForeignKey("JenisBarang")]
        public int JenisBarangID{ get; set; }

        [Display(Name = "ID Lokasi")]
        [ForeignKey("Lokasi")]
        public int LokasiID { get; set; }

        [Required]
        [Display(Name = "Nama Barang")]
        public string Nama { get; set; }

        [Required]
        [Display(Name = "Jumlah")]
        public int Qty { get; set; }

        [Required]
        [Display(Name = "Harga Beli")]
        public int HargaBeli { get; set; }

        [Display(Name = "Harga Jual Grosir")]
        public int HargaGrosir { get; set; }

        [Display(Name = "Harga Eceran Minimum")]
        public int HargaMinimum { get; set; }

        [Required]
        [Display(Name = "Harga Eceran Tertulis")]
        public int HargaPriceList { get; set; }

        [Display(Name = "Harga Rata-rata")]
        public float HargaRataan { get; set; }

        [Display(Name = "Keterangan")]
        public string Keterangan { get; set; }

        [Display(Name = "Non-Aktif")]
        public bool IsNonAktif { get; set; }

        public virtual JenisBarang JenisBarang { get; set; }
        public virtual Lokasi Lokasi { get; set; }
        public virtual ICollection<PembelianDetail> PembelianDetails { get; set; }
        public virtual ICollection<PenjualanDetail> PenjualanDetails { get; set; }
    }
}