using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Lokasi
    {
        [Key]
        [Required]
        [Display(Name = "ID Lokasi")]
        public int LokasiID { get; set; }

        [Required]
        [Display(Name = "Nama Lokasi")]
        public string Nama { get; set; }

        [Display(Name = "Keterangan")]
        public string Keterangan { get; set; }

        public virtual ICollection<Barang> Barangs { get; set; }
    }
}