using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class Mutasi
    {
        //[Key]
        //[ForeignKey("Barang")]
        [Display(Name = "ID Barang")]
        [Required]
        public int BarangID { get; set; }

        [Required]
        public int StokAwal { get; set; }

        [Required]
        public int Masuk{ get; set; }

        [Required]
        public int Keluar { get; set; }

        [Required]
        public int BatalMasuk { get; set; }

        [Required]
        public int BatalKeluar { get; set; }

        [Required]
        public int StokAkhir { get; set; }

        [Required]
        public int Adjustment { get; set; }

        public virtual Barang Barang { get; set; }
    }
}