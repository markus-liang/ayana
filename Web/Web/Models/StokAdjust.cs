using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class StokAdjust
    {
        [Key]
        [Required]
        public int StokAdjustID { get; set; }

        [Required]
        [Display(Name = "Tanggal")]
        public DateTime Tanggal { get; set; }

        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "Keterangan")]
        public string Keterangan { get; set; }

        public virtual ICollection<StokAdjustDetail> StokAdjustDetails { get; set; }
    }

    public class StokAdjustDetail
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("StokAdjust")]
        public int StokAdjustID { get; set; }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("Barang")]
        [Display(Name = "ID Barang")]
        public int BarangID { get; set; }

        [Required]
        [Display(Name = "Jumlah Dalam Data")]
        public int JumlahDalamData { get; set; }

        [Required]
        [Display(Name = "Jumlah Real")]
        public int JumlahReal { get; set; }

        public virtual StokAdjust StokAdjust { get; set; }
        public virtual Barang Barang { get; set; }
    }
}