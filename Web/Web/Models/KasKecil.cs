using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class KasKecil
    {
        [Key]
        [Required]
        [Display(Name = "ID Kas Kecil")]
        public int KasKecilID { get; set; }

        [Display(Name = "Jenis Kas")]
        [ForeignKey("JenisKasKecil")]
        public int JenisKasKecilID { get; set; }

        [Required]
        [Display(Name = "Tanggal")]
        public DateTime Tanggal { get; set; }

        [Display(Name = "Total")]
        public int Total { get; set; }

        [Display(Name = "Keterangan")]
        public string Keterangan { get; set; }

        [Display(Name = "Status")]
        [DefaultValue(true)]
        public bool Status { get; set; }

        public virtual JenisKasKecil JenisKasKecil { get; set; }

        public KasKecil()
        {
            Status = true;
        }
    }
}